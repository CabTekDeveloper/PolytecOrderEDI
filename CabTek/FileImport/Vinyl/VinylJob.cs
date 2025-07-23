
namespace PolytecOrderEDI
{
    static class VinylJob
    {
        
        public static List<VinylPart> LstVinylParts {  get; set; } = [];

        public static void Reset() => LstVinylParts = [];

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
                        return ReadAndClean() && ValidateVinylParts.Validate(LstVinylParts);
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

                        if (!(splitLine.Skip(1).All(element => string.IsNullOrEmpty(element.Trim())))) 
                        { 
                            lstFormattedData.Add(arrData[i]); 
                        }
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
                    foreach (var item in lstFormattedData)
                    {
                        LstVinylParts.Add(new VinylPart(item.Split(",")));
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
