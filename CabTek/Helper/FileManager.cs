//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Diagnostics;
using System.Reflection;
using System.Text.Json;

namespace PolytecOrderEDI
{
    static class FileManager
    {
        public static string FilePath { get; set; } = string.Empty;
        public static string FileName { get; set; } = string.Empty;
        public static string FileName_NoExt { get; set; } = string.Empty;
        public static string FileImportMessage { get; set; } = string.Empty;

        public static void Reset()
        {
            FilePath = string.Empty;
            FileName = string.Empty;
            FileName_NoExt = string.Empty;
            FileImportMessage = string.Empty;
        }

        public static bool Import(string initialDirectory, string filter = "")
        {
            try
            {
                var opf = new OpenFileDialog()
                {
                    InitialDirectory = initialDirectory,
                    Filter = filter,
                };

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    FilePath = opf.FileName;
                    FileName = FilePath.Split("\\")[^1];
                    FileName_NoExt = FileName.Split(".")[0];
                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public static bool OpenFile(string filePath = "")
        {
            try
            {

                if (File.Exists(filePath))
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            UseShellExecute = true,
                            FileName = filePath,
                        }
                    };
                    process.Start();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }


        public static void DeleteXMLifNotOrdered(string poNumber)
        {
            poNumber = poNumber.Trim();
            var jobType = GlobalVariable.JobType;
            if (poNumber != "" && !TableEdiOrderLog.IsOrderSentToPolytec(poNumber) )
            {
                var folderPath = (jobType == JOBTYPE.Melamine) ? FileAndDirectory.MelamineOrdersFolder : (jobType == JOBTYPE.Vinyl) ? FileAndDirectory.VinylOrdersFolder : "";
                FileManager.DeleteFile($"{folderPath}\\{poNumber}.xml");
            }
        }


        public static bool IsApplicationOpen(string applicationName)
        {
            Process[] processes = Process.GetProcessesByName(applicationName);
            if (processes.Length == 0) return false;
            else return true;
        }


        public static void SetICBPartQtyToZero()
        {
            var filePath = ICB.FilePath;
            List<int> orderedPartNumbers = ICB.LstICBPart.Select(part => part.PartNumber).ToList();
            List<ICBPart?> ICBParts = [];

            //Create a list of ICBParts from the orignal ICB File. Set the Quantity if the part has been ordered (Sent to Polytec).
            string[] arrData = File.ReadAllLines(filePath);
            string ICB_Header = CustomRegex.WhiteSpaces().Replace(arrData[0], "");
            arrData = arrData.Skip(1).ToArray();    //Remove Header

            foreach (var line in arrData)
            {
                if (line.Trim().Length > 0)
                {
                    var part = new ICBPart(line.Split("|"));
                    if (orderedPartNumbers.Contains(part.PartNumber))
                    {
                        part.Quantity = 0;
                    }
                    ICBParts.Add(part);
                }
                else
                {
                    ICBParts.Add(null);
                }
            }

            //Concat all ICB parts
            string concatedString = $"{ICB_Header}\n";

            foreach (var part in ICBParts)
            {
                if (part == null)
                {
                    concatedString += $"\n";
                }
                else
                {
                    var line = string.Empty;
                    foreach (PropertyInfo prop in part.GetType().GetProperties())
                    {
                        line += prop.GetValue(part) + "|";
                    }
                    concatedString += $"{line}EOL\n";
                }
            }

            //Finally, write the altered data to ICB
            File.WriteAllText(filePath, concatedString);
        }



        /// Added on 18-06-2025 by Wangchuk
        //public static bool WriteContentToFile(string filePath, string content)
        //{
        //    try
        //    {
        //        File.WriteAllText(filePath, content);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error writing to file: {ex.Message}");
        //        return false;
        //    }

        //}
    }
}
