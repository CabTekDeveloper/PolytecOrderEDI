using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXCEL = Microsoft.Office.Interop.Excel;


namespace PolytecOrderEDI
{
    static class PolytecBoardColours
    {
        public static List<PolyColor> LstPolyBoardColors { get;  } = [];
        public static int NewColoursAddedCount { get; set; } = 0;
        public static string NewBoardColorsAddedMsg { get; set; } = "";

        private static int ExcelHeader_rowIndex { get { return 1; } }
        private static string ColName_materialCode { get { return "Material Code"; } }
        private static string ColName_materialDescription { get { return "Material_Description"; } }
        private static string ColName_grain { get { return "Grain"; } }

   
        private static void Reset()
        {
            LstPolyBoardColors.Clear();
            NewBoardColorsAddedMsg = string.Empty;
            NewColoursAddedCount = 0;
        }

        public static bool Import()
        {
            try
            {
                Reset();

                // Exit if file not imported
                if (FileManager.Import(FileAndDirectory.Desktop, FileFilter.EXCEL) == false) return false;
                // Exit if file is not read
                if (ReadFile() == false) return false;
                // Exit if board colors are not updated in the Database
                if (UpdatePolytecBoardColoursTable() == false) return false;

                return true;
            }
            catch { return false; }

        }

        private static bool ReadFile()
        {
            try
            {
                string filePath = FileManager.FilePath;

                EXCEL.Application excel = new();
                EXCEL.Workbook wBook = excel.Workbooks.Open(filePath);
                EXCEL.Worksheet wSheet = wBook.Worksheets[1];

                int totalRows = wSheet.UsedRange.Rows.Count;
                int totalColumns = wSheet.UsedRange.Columns.Count;

                int materialCode_colIndex = 0;
                int materialDescription_colIndex = 0;
                int grain_colIndex = 0;

                for (int colIndex = 1; colIndex <= totalColumns; colIndex++)
                {
                    string colName = wSheet.Cells[ExcelHeader_rowIndex, colIndex].Value.Trim() ?? string.Empty;
                    if (string.Equals(colName, ColName_materialCode, StringComparison.OrdinalIgnoreCase))               materialCode_colIndex = colIndex;
                    else if (string.Equals(colName, ColName_materialDescription, StringComparison.OrdinalIgnoreCase))   materialDescription_colIndex = colIndex;
                    else if (string.Equals(colName, ColName_grain, StringComparison.OrdinalIgnoreCase))                 grain_colIndex = colIndex;
                    else { /*Do nothing */ }
                }

                // Exit if the required columns don't exist in the Excel File
                if (materialCode_colIndex == 0 || materialDescription_colIndex == 0 || grain_colIndex == 0)
                {
                    FileManager.FileImportMessage = "Please make sure the following columns are spelled correctly in the Excel File!\n\n";
                    FileManager.FileImportMessage += (materialCode_colIndex == 0) ? $"Missing Column name: '{ColName_materialCode}' \n" : "";
                    FileManager.FileImportMessage += (materialDescription_colIndex == 0) ? $"Missing Column name: '{ColName_materialDescription}' \n" : "";
                    FileManager.FileImportMessage += (grain_colIndex == 0) ? $"Missing Column name: '{ColName_grain}'\n" : ""; 
                    return false;
                }


                for (int rowIndex = 2; rowIndex <= totalRows; rowIndex++)
                {
                    string materialCode         = wSheet.Cells[rowIndex, materialCode_colIndex].Value;
                    string materialDescription  = wSheet.Cells[rowIndex, materialDescription_colIndex].Value;
                    string grain                = wSheet.Cells[rowIndex, grain_colIndex].Value;

                    if (materialDescription.Contains("polytec", StringComparison.OrdinalIgnoreCase))
                    {
                        var colorInfo = BuildColorInfo(materialCode, materialDescription, grain);
                        LstPolyBoardColors.Add(colorInfo);
                    }
                }
                wBook.Close(false, filePath);
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = $"Error in reading Excel file";
                return false;
            }
        }


        private static PolyColor BuildColorInfo(string materialCode, string materialDescription, string grain)
        {
            string[] arrSides = { "DS", "SS", "D/S", "S/S" };
            string color = "";
            string finish = "";
            string side = "";

            List<string> tempLst = materialDescription.Split(" ").ToList();
            tempLst = tempLst.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            // Remove items upto polytec
            int polytecIndex = tempLst.FindIndex(x => x.Contains("polytec", StringComparison.OrdinalIgnoreCase));
            tempLst.RemoveRange(0, polytecIndex+1);

            // Get materail description 
            if(tempLst.Count > 0) materialDescription = string.Join(" ", tempLst);


            // Get side if it exists in the tempLst and then remove it from tempLst
            if(tempLst.Count > 0)
            {
                if (arrSides.Contains(tempLst[^1], StringComparer.OrdinalIgnoreCase))
                {
                    side = tempLst[^1].Trim().ToUpper();
                    if (string.Equals(side, "S/S", StringComparison.OrdinalIgnoreCase)) side = "SS";
                    else if (string.Equals(side, "D/S", StringComparison.OrdinalIgnoreCase)) side = "DS";
                    else { /* Do nothing */}
                    tempLst.RemoveAt(tempLst.Count - 1);
                }
            }

            // Get finish and then remove it from tempLst
            if (tempLst.Count > 0)
            {
                finish = HelperMethods.TitleCaseString(tempLst[^1].Trim());
                tempLst.RemoveAt(tempLst.Count-1 );
            }

            // Get color and clear templst
            if (tempLst.Count > 0) color = HelperMethods.TitleCaseString(string.Join(" ", tempLst).Trim());

            // Finally, clear tempLst
            tempLst.Clear();

            //MessageBox.Show($"{materialCode}\n{color}\n{finish}\n{side}\n{grain}\n{materialDescription}");

            return new PolyColor(materialCode, color, finish, side, grain, materialDescription);
        }


        private static bool UpdatePolytecBoardColoursTable()
        {
            try
            {

                for (int i = 0; i<LstPolyBoardColors.Count; i++)
                {   
                    var colorInfo = LstPolyBoardColors[i];
                    if (!TablePolytecBoardColors.CheckRecordExists(colorInfo.MaterialCode))
                    {
                        TablePolytecBoardColors.InsertRecord(colorInfo);
                        NewColoursAddedCount++;
                        NewBoardColorsAddedMsg += $"{NewColoursAddedCount} : {colorInfo.MaterialDescription}\n";

                    }
                }
                return true;
            }
            catch
            {
                FileManager.FileImportMessage = "Error in adding Polytec Board color to Database!";
                return false;
            }
        }
    }
}
