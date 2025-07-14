
using System.Text;
using System.Globalization;
using BorgEdi;
using BorgEdi.Enums;
using BorgEdi.Helpers;
using BorgEdi.ResponseModels;

namespace PolytecOrderEDI
{
    static class PolytecConfiguredOrder
    {
        private static ConfiguredOrder _order = new();
        public static ConfiguredOrder Order
        {
            get { return _order; }
            set { _order = value; }
        }


        //Reset Order
        public static void Reset()
        {
            _order = new ConfiguredOrder();
        }


        //Remove all products from the Configured order
        public static void RemoveProducts() => _order.Lines.Clear();


        //Create Order
        public static bool CreateOrder(string PoNumber, string requestedDate)
        {
            try
            {
                DateTime RequestedDate = (requestedDate == "") ? DateTime.Now.Date : DateTime.ParseExact(requestedDate, GlobalVariable.DateFormat, CultureInfo.InvariantCulture);

                _order = new ConfiguredOrder
                {
                    ShipToGln = CabtekAccountDetails.GLN,
                    PoNumber = PoNumber,
                    RequestedDate = RequestedDate,
                    Attachments = [],
                };
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        

        //Build and AddDrillings Products to Configured Order
        public static bool BuildAndAddProducts()
        {
            RemoveProducts();

            var jobType = GlobalVariable.JobType;

            if(jobType == JOBTYPE.Melamine)
            {
                 if(BuildAndAddProducts_ICB.Build() == false) return false;
            }
            else
            {
                if (BuildAndAddProducts_Vinyl.Build() == false) return false ;
            }

            OutputOrderToXMLfile();
            return true;

        }

        public static void UpdatePoNumber(string newPoNumber)
        {
            _order.PoNumber = newPoNumber;
            OutputOrderToXMLfile();
        }

        //Update Requested date
        public static void UpdateRequestedDate(string requestedDate)
        {
            _order.RequestedDate = DateTime.ParseExact(requestedDate, GlobalVariable.DateFormat, CultureInfo.InvariantCulture);
            OutputOrderToXMLfile() ;
        }


        //Write Order details to XML file
        public static void OutputOrderToXMLfile()
        {
            var jobType = GlobalVariable.JobType;
            var folderPath = (jobType == JOBTYPE.Melamine) ? FileAndDirectory.MelamineOrdersFolder : (jobType == JOBTYPE.Vinyl) ? FileAndDirectory.VinylOrdersFolder : "";

            if (Directory.Exists(folderPath))
            {
                var filePath = $"{folderPath}\\{_order.PoNumber}.xml";
                File.WriteAllText(filePath, _order.GetEdiMessage());
            }
        }


        public static bool SendOrder(bool isTesting)
        {
            DialogResult res;
            try
            {
                var customerConfiguration = new CustomerConfiguration
                {
                    Environment = isTesting ? EdiEnvironment.Test : EdiEnvironment.Live,
                    AuthCode =  isTesting ? SftpTestCredentials.AuthCode : SftpLiveCredentials.AuthCode,
                    Customer = CabtekAccountDetails.CustomerAccountCode,
                };

                string userName = isTesting ? SftpTestCredentials.UserName : SftpLiveCredentials.UserName;
                string password = isTesting ? SftpTestCredentials.Password : SftpLiveCredentials.Password;


                //// Wangchuk added 30-06-2025
                //// When app is in development skip sending order to polytec but return true so the rest of the code in the calling method is excetued!
                //MessageBox.Show("App is in development. Skipped sending order to Polytec.");
                //return true;
                ////End App in developemnt

                //Send to Test Environment
                if (isTesting)
                {
                    

                    res = MessageBox.Show($"Send order to the Test Environment?", "Sending to Test Environment", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        res = MessageBox.Show($"Skip validation?", "Skip Validation", MessageBoxButtons.YesNo);
                        customerConfiguration.SkipValidation = (res == DialogResult.Yes) ;

                        Cursor.Current = Cursors.WaitCursor;    //AddDrillings wait cursor
                        _order.Send(customerConfiguration, userName, password, "/in-configured"); // Comment this line when the app is in development
                        Cursor.Current = Cursors.Default;


                        return true;
                    }
                }
                //Send to Polytec Live Environment
                else
                {
                    res = MessageBox.Show("Send order to Polytec?", "Confirm Order", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {  
                        string msg = "Ony skip validation if you are not able to send orders to Polytec!\n\n";
                        msg += "Skip validation?";
                        res = MessageBox.Show(msg, "Skip Validation", MessageBoxButtons.YesNo);
                        customerConfiguration.SkipValidation = (res == DialogResult.Yes);

                        Cursor.Current = Cursors.WaitCursor;    //Change cursor to wait cursor
                        _order.Send(customerConfiguration, userName, password, "/in-configured");
                        Cursor.Current = Cursors.Default;   //Change cursor to default cursor

                        return true;
                    }
                    
                }

                return false;
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static void DownloadOrderResponeAndOutputToXML(bool isTesting)
        {
            try
            {
                var customerConfiguration = new CustomerConfiguration
                {
                    Environment = isTesting ? EdiEnvironment.Test : EdiEnvironment.Live,
                    AuthCode = isTesting ? SftpTestCredentials.AuthCode : SftpLiveCredentials.AuthCode,
                    Customer = CabtekAccountDetails.CustomerAccountCode,
                };

                string userName = isTesting ? SftpTestCredentials.UserName : SftpLiveCredentials.UserName;
                string password = isTesting ? SftpTestCredentials.Password : SftpLiveCredentials.Password;

                var lstSftpFile = SftpHelper.GetFilesInPath(customerConfiguration, userName, password, "/out");

                if (lstSftpFile != null && lstSftpFile.Count > 0)
                {
                    string sftpFilePath = lstSftpFile[0].Path;
                    string sftpFileData = Encoding.ASCII.GetString(lstSftpFile[0].Data, 0, lstSftpFile[0].Data.Length);

                    string poNo = GetPoNumberFromOrderResponse(sftpFileData);
                    string fileName = $"{FileAndDirectory.VinylOrdersFolder}\\{poNo}_orderResponse.xml";
                    File.WriteAllText(fileName, sftpFileData);

                    //Mark order response as read. Otherwise we will be downloading old responses.
                    SftpHelper.MarkAsRead( new OrderResponse()
                    {
                        FileName = sftpFilePath,
                        CachedConfig = customerConfiguration,
                        CachedUsername = userName,
                        CachedPassword = password,
                    });

                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private static string GetPoNumberFromOrderResponse(string orderResponse)
        {
            string poNumber = string.Empty;

            if (orderResponse.Contains("<originalOrder>", StringComparison.OrdinalIgnoreCase))
            {
                var tempVal = orderResponse.Split("<originalOrder>")[1].Split("</originalOrder>")[0].ToString().Trim();

                if (tempVal.Contains("<entityIdentification>", StringComparison.OrdinalIgnoreCase))
                {
                    poNumber = tempVal.Split("<entityIdentification>")[1].Split("</entityIdentification>")[0].ToString().Trim();
                }
            }

            return poNumber;
        }


    }

}
