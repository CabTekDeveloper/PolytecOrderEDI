// File added on 02-07-2025 by Wangchuk
// Last modified by Wangchuk on 11-07-2025

using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace PolytecOrderEDI
{
    static class GoogleSheets
    {
        private static string PolytecOrder_BaseTemplateId { get { return "151u8tqkGWq8eUlsITS_LmaWbUg74LHD9HzEkr7UaYOI"; } }
        private static SheetsService? Service { get; } = GoogleApi.SheetsService;
        private static List<Request> RequestList { get; set; } = [];
        public static string NewSpreadSheetId { get; private set; } = string.Empty;
        public static string ErrorMessage { get; private set; } = string.Empty;



        private static void ResetProperties()
        {
            RequestList.Clear();
            NewSpreadSheetId = string.Empty;
            ErrorMessage = string.Empty;
        }

        public static async Task<string> CopyAndUpdatePolytecOrderSheet(OrderDetailsForGoogleApi orderDetails)
        {
            try
            {
                ResetProperties();

                if (Service == null) throw new Exception("Google Sheets Service is null.");

                NewSpreadSheetId = await GoogleDrive.CopyFile(PolytecOrder_BaseTemplateId);

                await RenameSheet(NewSpreadSheetId, orderDetails.PoNumber);

                await UpdateSingleCellValue(NewSpreadSheetId, "C1", orderDetails.PoNumber);
                await UpdateSingleCellValue(NewSpreadSheetId, "C2", orderDetails.OrderedBy);
                await UpdateSingleCellValue(NewSpreadSheetId, "C3", orderDetails.RequestedDate);

                var cellRange = $"A6:P{orderDetails.VinylProducts.Count + 5}";
                var body = BuildValueRangeFromVinylProducts(orderDetails.VinylProducts);
                var updateRequest = Service.Spreadsheets.Values.Update(body, NewSpreadSheetId, cellRange);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                await updateRequest.ExecuteAsync();

                return NewSpreadSheetId;
  
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error updating Polytec order google sheet.\n\nError details:\n{ex.Message}");
                return string.Empty;
            }
        }

       

        


        // Method to update single cell value. The single cell could be a merged cell
        private static async Task UpdateSingleCellValue(string googleSheetId, string cellRange, string newValue)
        {
            try
            {
                if (Service == null) throw new Exception("Google Sheets Service is null.");

                Cursor.Current = Cursors.WaitCursor;
                var value = new List<IList<object>> { new List<object> { newValue } };
                var body = new ValueRange() { Values = value };
                var updateRequest = Service.Spreadsheets.Values.Update(body, googleSheetId, cellRange);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                await updateRequest.ExecuteAsync();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating google sheet cell!\n\nError details:\n{ex.Message}");
            }
        }

        // Method to rename google sheet
        private static async Task<bool> RenameSheet(string filedToRenameId, string newName)
        {
            try
            {
                if (Service == null) throw new Exception("Google Sheets Service is null.");

                RequestList = [ new Request() {
                        UpdateSpreadsheetProperties = new UpdateSpreadsheetPropertiesRequest  {
                            Properties = new SpreadsheetProperties { Title = newName},
                            Fields = "title"
                        }
                    }
                ];

                var batchUpdateRequest = new BatchUpdateSpreadsheetRequest { Requests = RequestList };
                var response = await Service.Spreadsheets.BatchUpdate(batchUpdateRequest, filedToRenameId).ExecuteAsync();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error renaming google drive file!\n\nError details:\n{ex.Message}");
                return false;
            }
        }
        
        public static bool OpenSpreadSheetInBrowser(string spreadsheetId)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://docs.google.com/spreadsheets/d/{spreadsheetId}/edit",
                    UseShellExecute = true
                });
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error opening google sheet in browser!\n\nError details:\n{ex.Message}");
                return false;
            }
        }

        //Method to build ValuRange object from the list of Vinyl Products
        private static ValueRange BuildValueRangeFromVinylProducts(List<VinylPart> vinylProducts)
        {

            var values = new List<IList<object>>();
            foreach (var product in vinylProducts)
            {
                var col_A_Val = product.LineNo;
                var col_B_Val = HelperMethods.TitleCaseString(product.ProductType.ToString());
                var col_C_Val = HelperMethods.TitleCaseString(product.Product.ToString());
                var col_D_val = product.Quantity;
                var col_E_val = product.Height > 0 ? (object)product.Height : "";
                var col_F_val = product.Width;
                var col_G_val = product.DfHeight > 0 ? (object)product.DfHeight : "";
                var col_H_val = product.Thickness;
                var col_I_val = (product.PartName != PARTNAME.None) ? product.PartName.ToString().Replace("_", " ") : "";
                var col_J_val = product.EdgeLocation;
                var col_K_val = string.Equals("None", product.HandleSystem) ? string.Empty : product.HandleSystem;
                var col_L_val = product.Colour;
                var col_M_val = product.Finish;
                var col_N_val = product.EdgeMould;
                var col_O_val = product.FaceProfile;
                var col_P_val = product.EzeNo;

                values.Add([col_A_Val, col_B_Val, col_C_Val, col_D_val, col_E_val, col_F_val, col_G_val, col_H_val, col_I_val, col_J_val, col_K_val, col_L_val, col_M_val, col_N_val, col_O_val, col_P_val]);
            }
            var valueRange = new ValueRange() { Values = values };

            return valueRange;
        }

    }
}







