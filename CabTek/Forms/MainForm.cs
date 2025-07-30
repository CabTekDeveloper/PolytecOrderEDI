//using BorgEdi;
//using BorgEdi.Enums;
//using BorgEdi.Models;
//using Microsoft.Win32;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Diagnostics;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//using System.ComponentModel;
//using System.Reflection;
//using Microsoft.VisualBasic.Logging;
//using System.IO;
//using BorgEdi.ResponseModels;

namespace PolytecOrderEDI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        //FORM LOAD
        private void MainForm_Load(object sender, EventArgs e)
        {
            GlobalVariable.CurrentUserName = RegistryInfo.GetUserName();
            if (String.IsNullOrEmpty(GlobalVariable.CurrentUserName))
            {
                MessageBox.Show($"Set user name in the following Registry Path:\n\n{FileAndDirectory.KeyPath_PruchaseOrderForm_Information}", "Missing user name in registry", MessageBoxButtons.OK);
                Application.Exit();
            }
            else
            {
                ChangeJobType();
                ResetMainForm();
                TableEdiAppConnectionLog.AddCurrentUserConnectDateTime();
                LblCurrentUserName.Text = GlobalVariable.CurrentUserName;
            }
        }

        // FORM CLOSING
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TableEdiAppConnectionLog.UpdateCurrentUserDisconnectDateTime();
            FileManager.DeleteXMLifNotOrdered(LblPoNumber.Text);
        }


        //BUTTON: CHANGE JOB TYPE
        private void BtnChangeJobType_Click(object sender, EventArgs e)
        {
            FileManager.DeleteXMLifNotOrdered(LblPoNumber.Text);
            this.Hide();
            ChangeJobType();
            ResetMainForm();
            this.Show();
        }


        //IMPORT FILES: CSV, ICB
        private void BtnImportFile_Click(object sender, EventArgs e)
        {
            try
            {

                FileManager.DeleteXMLifNotOrdered(LblPoNumber.Text);

                ResetMainForm();

                JOBTYPE jobType = GlobalVariable.JobType;
                bool isFileImported = (jobType == JOBTYPE.Vinyl) ? VinylJob.Import() : ICB.Import();
                var poNumber = GlobalVariable.PoNumber;
                var requestedDate = GlobalVariable.RequestedDate;
                var fileImportMessage = FileManager.FileImportMessage;

                LblPoNumber.Text = poNumber;
                LblRequestedDate.Text = requestedDate;

                if (isFileImported)
                {
                    if (TableEdiOrderLog.IsOrderSentToPolytec(poNumber))
                    {
                        EdiOrderLog orderLog = TableEdiOrderLog.GetOrderLog(poNumber);
          
                        LblSendFileMsg.Text = $"This job has been ordered already.\n\n{orderLog.ToString()}";
                        LblSendFileMsg.ForeColor = Color.Red;
                        BtnViewOrderXML.Visible = true;
                    }
                    else
                    {
                        if (PolytecConfiguredOrder.CreateOrder(GlobalVariable.PoNumber, GlobalVariable.RequestedDate))
                        {
                            if (PolytecConfiguredOrder.BuildAndAddProducts())
                            {
                                LblFileImportMsg.Text = "Order built successfully.";
                                BtnViewOrderXML.Visible = true;
                                BtnSendOrder.Visible = true;
                                BtnEditPoNumber.Visible = true;
                                BtnViewImportedCabinetParts.Visible = (GlobalVariable.JobType == JOBTYPE.Melamine) && true;
                                BtnPickRequestedDate.Visible = (GlobalVariable.JobType == JOBTYPE.Melamine) && true;
                                BtnOpenAddAttachmentForm.Visible = true;


                            }
                            else
                            {
                                LblFileImportMsg.Text = $"Failed to build Add Products to Configured Order.";
                                PolytecConfiguredOrder.Reset();
                            }
                        }
                        else
                        {
                            LblFileImportMsg.Text = $"Failed to create Configured Order.";
                            PolytecConfiguredOrder.Reset();
                        }
                    }
                }
                else
                {
                    LblFileImportMsg.Text = fileImportMessage;
                    LblFileImportMsg.ForeColor = Color.Red;
                    BtnViewOrderXML.Visible = false;
                    BtnSendOrder.Visible = false;
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        // VIEW XML ORDER
        private void BtnViewOrderXML_Click(object sender, EventArgs e)
        {
            OpenFile_OrderXML();
        }


        //SEND ORDER
        private async void BtnSendOrder_Click(object sender, EventArgs e)
        {
            try
            {
                bool isTesting = GlobalVariable.IsTestMode;

                //Exit if requested date is not provided
                if (LblRequestedDate.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Requested date not picked!", "Missing requested date.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //Make sure Notepad++ is closed when sending a melamine job to the EDI Live enviroment of Polytec
                if (!isTesting && GlobalVariable.JobType == JOBTYPE.Melamine && FileManager.IsApplicationOpen("notepad++"))
                {
                    var msg = "Please close Notepad++ and try sending the job again!\n\n" +
                             $"If the ICB File '{GlobalVariable.FileName}' is open in any other applicatoins, close them all.\n\n" +
                             $"This is to make sure any changes made to the ICB file by this EDI App is applied.";
                    MessageBox.Show(msg, "Close Notepad++");
                    return;
                }

                BtnSendOrder.Visible = false;

                if (TableEdiOrderLog.IsOrderSentToPolytec(PolytecConfiguredOrder.Order.PoNumber))
                {
                    EdiOrderLog orderLog = TableEdiOrderLog.GetOrderLog(GlobalVariable.PoNumber);
                    LblSendFileMsg.Text = $"Cannot send this order because it has been ordered before.\n" +
                                          $"{orderLog.ToString()}\n"+
                                          $"Note: You can rename the order to send as a new order.";
                    LblSendFileMsg.ForeColor = Color.Red;
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                   
                    if (PolytecConfiguredOrder.SendOrder(isTesting))
                    {
                        // Update sent stats label
                        LblSendFileMsg.Text = $"Success... Order sent to Polytec {(isTesting ? "Test" : "Live")} Environemnt.\n";

                        // AddDrillings order log to db if the order is sent to the live environment/polytec
                        if (!isTesting) 
                        {
                            TableEdiOrderLog.InsertNewOrderLog(PolytecConfiguredOrder.Order.PoNumber, GlobalVariable.CurrentUserName, "polytec");
                            LblSendFileMsg.Text += $"Success...  Order log added to database.\n";
                        }

                        // If the job is melamine job and not a test job, set Quanity to 0 in the ICB file.
                        if (GlobalVariable.JobType == JOBTYPE.Melamine && isTesting == false)
                        {
                            FileManager.SetICBPartQtyToZero();
                            LblSendFileMsg.Text += $"Success... Part qty set to 0 in the ICB File {ICB.FileName}.\n";
                        }

                        Cursor.Current = Cursors.WaitCursor;

                        //If the job is Vinyl, update contents with order details
                        if (GlobalVariable.JobType == JOBTYPE.Vinyl)
                        {
                            LblSendFileMsg.Text += "\nPlease wait... Generating Google Sheets for Polytec order.\n";
                            var orderDetailsForGoogleSheet = new OrderDetailsForGoogleApi();
                            string newSpreadSheetId = await GoogleSheets.CopyAndUpdatePolytecOrderSheet(orderDetailsForGoogleSheet);

                            if (!string.IsNullOrEmpty(newSpreadSheetId))
                            {
                                LblSendFileMsg.Text += "Success... Polytect order google sheet generated.\n";

                                // Change the permission so that it can be viewed by anyone with link
                                await GoogleDrive.ChangeFileAccessToAnyoneWithLink(newSpreadSheetId);

                                MessageBox.Show("Do you want to open Google sheet to print the order?", "Open Google sheets");

                                if (!GoogleSheets.OpenSpreadSheetInBrowser(newSpreadSheetId))
                                {
                                    MessageBox.Show("Failed to open Google sheet order report!", "Error");
                                    LblSendFileMsg.Text += "\nError... Failed to open Google sheet order report!\n" +
                                                            "\t\tYou can print order report from the Excel Door order form.";
                                }
                            }
                            else
                            {
                                LblSendFileMsg.Text += "\nError... Failed to generate google sheet order report for printing.\n";
                            }
                        }
                        Cursor.Current = Cursors.Default;


                    }
                    else
                    {
                        LblSendFileMsg.Text = "Error: Failed to send order.\n";
                        LblSendFileMsg.ForeColor = Color.Red;
                    }  
                }
            }

            catch (Exception ex) 
            {
                Cursor.Current = Cursors.Default;
                LblSendFileMsg.Text += $"Error:{ex.Message}.\n";
                MessageBox.Show(ex.Message);
            }

        }

        // Button: Info
        private void BtnInfo_Click(object sender, EventArgs e)
        {
            string msgTitle = "v1.0";
            string msg = string.Empty;
            string activeUsers = TableEdiAppConnectionLog.GetActiveUsers();

            msg += "This version of EDI can be used for ordering the following products:\n" +
                            "- Thermo Products \n" +
                            "- Cut and Rout Products \n" +
                            "- Melamine job from ICB \n";

            msg += $"\n\nOnline:\n{activeUsers}";

            var recentLogs = TableEdiOrderLog.GetRecentOrderLogs();
            if (recentLogs != "") msg += $"\n\nRecent order sent to Polytec:\n{recentLogs}";

            MessageBox.Show(msg, msgTitle, MessageBoxButtons.OK);
        }


        //Button: Open form to add attachments
        private void BtnOpenAddAttachmentForm_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var frmAddAttachments = new FrmAddAttachments();
            frmAddAttachments.ShowDialog();
            this.Visible = true;

            if (AttachmentManager.AttachmentsAddedToConfiguredOrder)
            {
                BtnOpenAddAttachmentForm.Visible = false;
                int attachmentCount = AttachmentManager.Attachments.Count;
                if (attachmentCount > 0) lblAttachmentName1.Text = AttachmentManager.Attachments[0].FileName;
                if (attachmentCount > 1) lblAttachmentName2.Text = AttachmentManager.Attachments[1].FileName;
                if (attachmentCount > 2) lblAttachmentName3.Text = AttachmentManager.Attachments[2].FileName;
            }
            else
            {
                AttachmentManager.Reset();
            }

        }


        // Button: Edit PoNumber
        private void BtnEditPoNumber_Click(object sender, EventArgs e)
        {
            var poNumber = GlobalVariable.PoNumber;
            var promptStr = "Edit PO Number";
            string newPoNumber;
            do
            {
                newPoNumber = Microsoft.VisualBasic.Interaction.InputBox(promptStr, "Purchase Order Number", poNumber).Trim().Replace(" ", "");
                if (newPoNumber == "")
                {
                    promptStr = "PO Number cannot be empty!";
                }
                else
                {
                    if (TableEdiOrderLog.IsOrderSentToPolytec(newPoNumber))
                    {
                        EdiOrderLog orderLog = TableEdiOrderLog.GetOrderLog(newPoNumber);
                        promptStr = $"A job has been ordered already with PO number \"{newPoNumber}\"\n\n{orderLog.ToString()}\n\nEnter a new PO number!";
                        newPoNumber = string.Empty; // Clear the newPoNumber so we can keep the while loop running to get an appropriate po number.
                    }
                }  
            } while (newPoNumber == "");


            if (!string.Equals(poNumber, newPoNumber, StringComparison.OrdinalIgnoreCase))
            {
                LblPoNumber.Text = newPoNumber;
                FileManager.DeleteXMLifNotOrdered(poNumber);
                PolytecConfiguredOrder.UpdatePoNumber(newPoNumber);
                GlobalVariable.PoNumber = newPoNumber;

            }
        }


        //Button: Pick Requested Date
        private void BtnPickRequestedDate_Click(object sender, EventArgs e)
        {
            if (ICB.Cabinets.Count == 0) return;
            this.Visible = false;
            var frmDatePicker = new FrmDatePicker(title: "Pick requested date", isRequestedDate: true);
            frmDatePicker.ShowDialog();
            LblRequestedDate.Text = GlobalVariable.RequestedDate;
            this.Visible = true;

            PolytecConfiguredOrder.UpdateRequestedDate(GlobalVariable.RequestedDate);
        }

        //Button: Edit Additional Instructions
        private void BtnViewImportedCabinetParts_Click(object sender, EventArgs e)
        {
            if (ICB.Cabinets.Count == 0) return;

            this.Hide();
            Form frmAdditionalInstruction = new FrmImportedCabinetParts();
            frmAdditionalInstruction.ShowDialog();
            this.Show();
        }

        //Button: Open Form "FrmPolytecColors"
        private void BtnOpenFrmPolytecColors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new FrmPolytecColors();
            form.ShowDialog();
            this.Show();
        }


        //OPEN FILE: ORDER XML
        private static void OpenFile_OrderXML()
        {
            try
            {
                var jobType = GlobalVariable.JobType;
                var folderPath = (jobType == JOBTYPE.Melamine) ? FileAndDirectory.MelamineOrdersFolder : (jobType == JOBTYPE.Vinyl) ? FileAndDirectory.VinylOrdersFolder : "";
                var poNumber = GlobalVariable.PoNumber;
                var fileToOpen = $"{folderPath}\\{poNumber}.xml";

                if (!FileManager.OpenFile(fileToOpen)) MessageBox.Show($"The Order XML file does not exists for this job.");

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        //CHANGE JOB TYPE
        private void ChangeJobType()
        {
            var frmSelectJobType = new FrmSelectJobType();
            frmSelectJobType.ShowDialog();

            var jobType = GlobalVariable.JobType;
            LblJobType.Text = jobType.ToString();
            LblJobType.BackColor = ColorManager.GetJobTypeBackColor(jobType);
            LblJobType.ForeColor = ColorManager.GetJobTypeForeColor(jobType);
        }


        //RESET MAIN FORM
        private void ResetMainForm()
        {
            LblRequestedDate.Text = "";
            LblFileImportMsg.Text = "";
            LblFileImportMsg.ForeColor = Color.Blue;

            lblAttachmentName1.Text = "";
            lblAttachmentName2.Text = "";
            lblAttachmentName3.Text = "";

            LblSendFileMsg.Text = "";
            LblSendFileMsg.ForeColor = Color.Blue;
            LblPoNumber.Text = "";
            BtnViewOrderXML.Visible = false;
            BtnSendOrder.Visible = false;

            BtnEditPoNumber.Visible = false;
            BtnPickRequestedDate.Visible = false;
            BtnOpenAddAttachmentForm.Visible = false;
            BtnViewImportedCabinetParts.Visible = false;

            PolytecConfiguredOrder.Reset();
            AttachmentManager.Reset();
            VinylJob.Reset();
            ICB.Reset();
            GlobalVariable.Reset();
            GlobalVariable.IsTestMode = ChkTesting.Checked;
            BtnOpenFrmPolytecColors.Visible = GlobalVariable.JobType == JOBTYPE.Melamine;
        }

        private void ChkTesting_CheckedChanged(object sender, EventArgs e)
        {
            GlobalVariable.IsTestMode = ChkTesting.Checked;
        }
    }
}


