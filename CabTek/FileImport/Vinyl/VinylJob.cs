//using BorgEdi;
//using BorgEdi.Interfaces;
//using BorgEdi.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Diagnostics;
//using System.Drawing;
//using System.Globalization;
//using System.Linq;
//using System.Security.Policy;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using static System.Windows.Forms.LinkLabel;

namespace PolytecOrderEDI
{
    static class VinylJob
    {
        
        public static List<VinylPart> LstProducts {  get; set; } = [];
        public static void Reset() => LstProducts = [];

        public static bool Import()
        {
            try
            {
                var directoryPath = FileAndDirectory.VinylOrdersFolder;
                var fileFilter = FileFilter.CSV;

                if (FileManager.Import(directoryPath, fileFilter))
                {
                    if (!FileManager.FilePath.Contains(directoryPath))
                    {
                        FileManager.FileImportMessage = "You have imported a wrong file!";
                        return false;
                    }
                    else
                    {
                        return ReadAndClean() && ValidateVinylParts.Validate(LstProducts);
                    }
                }
                else
                {
                    FileManager.FileImportMessage = "File not imported";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private static bool ReadAndClean()
        {
            try
            {
                List<string>  lstFormattedData = [];
                string[] arrData = File.ReadAllLines(FileManager.FilePath);

                if (arrData.Length == 0)
                {
                    FileManager.FileImportMessage = "There is no data to build order!";
                    return false;
                }
                else
                {
                    //FORMAT IMPORTED DATA
                    for (int i = 0; i < arrData.Length; i++)
                    {
                        string[] splitLine = arrData[i].Split(',');

                        if (!(splitLine.Skip(1).All(element => string.IsNullOrEmpty(element.Trim())))) { lstFormattedData.Add(arrData[i]); }
                    }

                    //WRITE FORMATTED DATA BACK TO FILE
                    if (arrData.Length > lstFormattedData.Count)
                    {
                        File.WriteAllLines(FileManager.FilePath, lstFormattedData);
                    }

                    //REMOVE HEADER 
                    lstFormattedData.RemoveAt(0);

                    //EXIT IMPORTATION IF DATA IS EMPTY
                    if (lstFormattedData.Count == 0)
                    {
                        FileManager.FileImportMessage = "There is no data to build order!";
                        return false;
                    }

                    //CRTEATE LIST OF PRODUCT OBJECTS
                    for (int i = 0; i < lstFormattedData.Count; i++)
                    {
                        var objProduct = new VinylPart(lstFormattedData[i].Split(","));
                        LstProducts.Add(objProduct);
                    }

                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = $"Error in reading and cleaning Vinyl job";
                return false;
            }
        }

  

    } 

} 
