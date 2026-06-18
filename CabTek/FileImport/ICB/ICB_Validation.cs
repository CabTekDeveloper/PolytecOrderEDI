//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

namespace PolytecOrderEDI
{
    static class ICB_Validation
    {
        private static string ErrorMessage { get; set; } = string.Empty;
        private static bool ExitValidation { get; set; } = false;

        private static void Reset()
        {
            ErrorMessage = string.Empty;
            ExitValidation = false;
        }

        public static bool Validate(List<ICBPart> LstICBPart)
        {
            try
            {
                Reset(); //Reset the properties if it was set when when validating previous jobs.

                //Check if all vinylPart Numbers and Ids are unique.
                UniquePartNumber_Validation(LstICBPart);
                UniquePartId_Validation(LstICBPart);

                //Validate each part
                if (!ExitValidation)
                {
                    foreach (var part in LstICBPart)
                    {
                        string errorMsg = string.Empty;

                        errorMsg += Thickness_Validation(part);
                        errorMsg += Material_Validation(part);
                        errorMsg += EdgeDescription_Validation(part);
                        errorMsg += Parameter_Validation(part);

                        //Concat error messages
                        if (errorMsg.Length > 0)
                            ErrorMessage += $"PART NUMBER: {part.PartNumber}\n{errorMsg}\n";
                        
                    }
                }

                //Display error messages
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Fix the errors and import again.");
                    FileManager.FileImportMessage = $"Fix the errors and import again.";
                    return false;
                }

                return true; 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = $"Error in validating ICB job";
                return false;
            }
        }

       
        //UNIQUE PART NUMBER VALIDATION
        private static void UniquePartNumber_Validation(List<ICBPart> LstICBPart)
        {
            List<int> duplicatePartNos = [];
            HashSet<int> tempSet = [];

            foreach (var part in LstICBPart)
            {
                if (!tempSet.Add(part.PartNumber))
                {
                    if (!duplicatePartNos.Contains(part.PartNumber)) 
                        duplicatePartNos.Add(part.PartNumber);
                }
            }

            if (duplicatePartNos.Count > 0)
            {
                ErrorMessage += $"Cannot have duplicate Part Number:\n{string.Join(", ", duplicatePartNos)}\n\n";
                ExitValidation = true;
            }

        }


        //UNIQUE PART ID VALIDATION
        private static void UniquePartId_Validation(List<ICBPart> LstICBPart)
        {
            List<int> duplicatePartIds = [];
            HashSet<int> tempSet = [];

            foreach (var part in LstICBPart)
            {
                if (!tempSet.Add(part.UniquePartId))
                {
                    if (!duplicatePartIds.Contains(part.UniquePartId)) 
                        duplicatePartIds.Add(part.UniquePartId);
                }
            }

            if (duplicatePartIds.Count > 0)
            {
                ErrorMessage += $"Cannot have duplicate Part ID:\n{string.Join(", ", duplicatePartIds)}\n\n";
                ExitValidation = true;
            }
        }


        private static string Thickness_Validation(ICBPart part)
        {
            if (part.Dimz == 0)
                return "Thickness cannot be left empty or set to 0.\n";

            //Until Polytec adds new models for different thciknesses of Decoratitive products, any thickness other than 18mm will be categorized as decorative16mm.
            //And if the thickness is not 16mm no 18mm, we will validate if the product has been set to Door
            var thickness = (int) part.Dimz;
            if (thickness != 16 && thickness != 18 && !part.PartDescription.Contains("door", StringComparison.OrdinalIgnoreCase))
                return "For thicknesses other than 16mm or 18mm, set PartDescription to 'Panel' or 'Door' in the ICB File.\n";

             return string.Empty;
        }

        //PARAMETER VALIDATION
        private static string Parameter_Validation(ICBPart part)
        {
            if (part.Parameter.Length == 0)
                return string.Empty;

            if (part.Parameter.Any(x => char.IsWhiteSpace(x))) 
                return "Parameter field cannot have white spaces.\n";

            var dict_param = HelperMethods.SplitParameter(part.Parameter);

            if (dict_param.Count == 0)
                return "Check the Parameters.\n";

            foreach (var item in dict_param)
            {
                if (!CustomRegex.LetterAndDigit().IsMatch(item.Key))
                    return "Parameter field can only contain Letters and Numbers only.\n";
            }

            return string.Empty;
        }

        //MATERIAL VALIDATION
        private static string Material_Validation(ICBPart part)
        {
            string errorMsg = string.Empty;

            if (!HelperMethods.IsMaterialHMRparticleBoard(part.Material) && !TablePolytecBoardColors.CheckRecordExists(part.Material))
                errorMsg += $"The Material Color '{part.Material}' is either misspelled or not present in the EDI database.\n";
           
            return errorMsg;
        }

        //EDGE DESCRIPTION VALIDATION
        private static string EdgeDescription_Validation(ICBPart part)
        {
            string concatEdgeDescription = part.TopEdgeDescription + part.BottomEdgeDescription + part.LeftEdgeDescription + part.RightEdgeDescription;

            if (concatEdgeDescription.Contains("contrasting", StringComparison.OrdinalIgnoreCase))
                return "Replace edge description 'Contrasting' with a valid MaterialCode or 'Matching'.\n";

            var edgeDescription = HelperMethods.GetEdgeColor(part);

            if (string.Equals(edgeDescription, "matching", StringComparison.OrdinalIgnoreCase))
                return string.Empty ;

            if(!HelperMethods.IsMaterialHMRparticleBoard(part.Material) && 
                (string.Equals(edgeDescription, "white", StringComparison.OrdinalIgnoreCase) || 
                string.Equals(edgeDescription, "black", StringComparison.OrdinalIgnoreCase)))
                    return "Replace edge description with a Valid MaterialCode or 'Matching'.\n";

            return string.Empty;
        }

    }
}
