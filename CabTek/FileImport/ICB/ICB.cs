
//using Microsoft.VisualBasic;
//using System;
//using System.Reflection;

namespace PolytecOrderEDI
{
    static class ICB
    {
        public static List<Cabinet> Cabinets { get; set; } = [];
        public static List<ICBPart> LstICBPart {  get; set; } = [];
        public static string FileName { get; private set; } = string.Empty; //Store file name with extension
        public static string JobNumber { get; set; } = string.Empty;
        public static string ClientOrderNumber { get; set; } = string.Empty;
        public static string FilePath { get; set; } = string.Empty;

        public static void Reset()
        {
            Cabinets.Clear();
            LstICBPart.Clear();
            FileName = string.Empty;
            JobNumber = string.Empty;
            ClientOrderNumber = string.Empty;
            FilePath = string.Empty;
        }   

        public static bool Import()
        {
            try
            {
                if ( !FileManager.Import( $"{FileAndDirectory.KitFilesFolder}\\{GlobalVariable.CurrentUserName}\\Jobs" , FileFilter.ICB) )
                {
                    FileManager.FileImportMessage = "File not imported";
                    return false;
                }
                
                FilePath = FileManager.FilePath;
                FileName = FileManager.FileName;
                GlobalVariable.PoNumber = $"{FileManager.FileName_NoExt}-"; //Save PO Number globally
                GlobalVariable.FileName = FileManager.FileName ;

                if (!ReadFile())
                    return false;

                LstICBPart = ICB_FilterParts.Filter(LstICBPart);

                if (LstICBPart.Count == 0)
                {
                    FileManager.FileImportMessage = "No data imported!\nEither the quantities are 0 or the parts in the ICB cannot be ordered via the EDI App yet.";
                    return false;
                }
              
                if (!ICB_Validation.Validate(LstICBPart)) 
                    return false;
                
                return BuildCabinetData();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private static bool ReadFile()
        {
            try
            {
                string[] arrData = File.ReadAllLines(FilePath);
                arrData = [.. arrData.Skip(1)];    //Remove Header
                arrData = [.. arrData.Where(val => val.Trim().Length > 0)]; //Remove empty rows

                if (arrData.Length == 0)
                {
                    FileManager.FileImportMessage = "There is no data to build order!";
                    return false;
                }
               
                foreach (var line in arrData)
                {
                    var part = new ICBPart(line.Split("|"));
                    LstICBPart.Add(part);
                }

                return true;
                
            }
            catch (Exception ex)
            {   
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = "Error in reading and cleaning ICB file";
                return false;
            }
        }


        // *How the Cabinet data is built:
        // 1. Parts (here part is the individual line of record in the ICB file) belonging to the same cabinet will be grouped together and stored in a List - 'tempList'.
        // 2. The Cabinet class will create a new Cabinet object from tempList
        // 3. Then add the new Cabinet object to the List, Cabinets.
        // 4. Finally clear the tempList to store the next cabinet parts.
        // 5. Continue steps 1 though 4 till all data is read.
        // The Cabinet objects stored in the Cabinets List will be used to build the Products for Polytec Configured Order.
        private static bool BuildCabinetData()
        {
            try
            {
                JobNumber = LstICBPart[0].JobNumber;
                ClientOrderNumber = LstICBPart[0].ClientOrderNumber;

                List<ICBPart> tempCabinet = []; //Store parts belonging to same cabinet.


                foreach (var part in LstICBPart)
                {
                    // If the current part belongs to new cabinet, add cabinet parts in tempCabinet to Cabinet list and clear tempCabinet.
                    bool isNewCabinet = tempCabinet.Count > 0 && part.CabinetName != tempCabinet[^1].CabinetName;
                    if (isNewCabinet)
                    {
                        Cabinets.Add(new Cabinet(tempCabinet));     // Build cabinet and add to Cabinets List
                        tempCabinet = [];                           // Reset tempCabinet for new cabinet parts.
                    }

                    // Add current part to tempCabinet.
                    if (part.CNCCODE == "BSDFBANK" && part.Parameter.Contains("DN="))
                    {
                        var dfs = SplitDrawerBank(part);
                        foreach (var df in dfs) 
                            tempCabinet.Add(df);
                    }
                    else
                    {
                        tempCabinet.Add(part);
                    }
                }

                //Add the last cabinet parts to Cabinets list.
                if (tempCabinet.Count > 0)
                    Cabinets.Add(new Cabinet(tempCabinet));

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = "Error in building cabinet data.";
                return false;
            }

        }


        private static List<ICBPart> SplitDrawerBank(ICBPart part)
        {
            List<ICBPart> drawerBank = [];
            var dict_param = HelperMethods.SplitParameter(part.Parameter);
            var handleParamStr = RebuildHandleParameterString(part.Parameter);

            int numDrawers = dict_param.TryGetValue("DN", out double paramValue) ? (int)paramValue : 0;

            for(int i = 1; i <=numDrawers; i++)
            {
                var height  = dict_param.TryGetValue($"H{i}", out paramValue) ? paramValue : 0;
                var dtyp    = dict_param.TryGetValue($"D{i}", out paramValue) ? paramValue : 0;
                var inup    = dict_param.TryGetValue($"I{i}", out paramValue) ? paramValue : 0;
                var lins    = dict_param.TryGetValue($"L{i}", out paramValue) ? paramValue : 0;
                var rins    = dict_param.TryGetValue($"PR{i}", out paramValue) ? paramValue : 0;
                var ldia    = dict_param.TryGetValue($"LD{i}", out paramValue) ? paramValue : 0;
                var rdia    = dict_param.TryGetValue($"RD{i}", out paramValue) ? paramValue : 0;

                var newParam = $"DTYP={dtyp}_INUP={inup}_LINS={lins}_RINS={rins}_LDIA={ldia}_RDIA={rdia}_{handleParamStr}".TrimEnd('_');

                //Create a new part and modify its property
                var newPart = ICBPart.Clone(part);
                newPart.Dimx = height;
                newPart.Parameter = newParam;
                newPart.PartDescription = $"({i})_Drawer_Front";

                drawerBank.Add(newPart);
            }
            return drawerBank;  
        }

        private static string RebuildHandleParameterString(string parameter)
        {
            string[] handleParamKeys = ["HDLT", "HDLO", "HDLS", "HDLD", "HDLX", "HDLY", "HDDP"];
            string handleParam = "";
            var dict_param = HelperMethods.SplitParameter(parameter);

            foreach (var p in  dict_param)
            {
                if (handleParamKeys.Contains(p.Key))
                    handleParam += $"{p.Key}={p.Value}_";
            }
            return handleParam.TrimEnd('_'); 
        }


    }//End of class
}//End of namespace


