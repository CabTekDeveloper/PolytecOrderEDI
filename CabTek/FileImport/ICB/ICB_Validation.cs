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

                //Check if all Part Numbers and Ids are unique.
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
                        if (errorMsg != "")
                        {
                            ErrorMessage += $"PART NUMBER: {part.PartNumber}\n{errorMsg}\n";
                        }
                    }
                }

                //Display error messages
                if (ErrorMessage.Length > 0)
                {
                    MessageBox.Show(ErrorMessage, "Fix the errors and import again.");
                    FileManager.FileImportMessage = $"Fix the errors and import again.";
                    return false;
                }
                else { return true; }

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
                var partNo = part.PartNumber;
                if (!tempSet.Add(partNo))
                {
                    if (!duplicatePartNos.Contains(partNo)) duplicatePartNos.Add(partNo);
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
                var partId = part.UniquePartId;
                if (!tempSet.Add(partId))
                {
                    if (!duplicatePartIds.Contains(partId)) duplicatePartIds.Add(partId);
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
            
            string errorMsg = string.Empty;
            if (part.Dimz == 0)
            {
                errorMsg += $"Thickness cannot left empty or set to 0.\n";

            }
            else
            {
                //Until Polytec adds new models for different thciknesses of Decoratitive products, any thickness other than 18mm will be categorized as decorative16mm.
                //And if the thickness is not 16mm no 18mm, we will validate if the product has been set to Door

                var thicness = (int) part.Dimz;
                if (thicness != 16 && thicness != 18)
                {
                    if (!part.PartDescription.Contains("Door", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMsg += $"For thickness other than 16mm or 18mm, set PartDescription to 'Panel' or 'Door' in the ICB File.\n";
                    }
                }
            }

                return errorMsg;

        }

        //PARAMETER VALIDATION
        private static string Parameter_Validation(ICBPart part)
        {
            string errorMsg = string.Empty;
            if (part.Parameter.Length > 0)
            {
                if (part.Parameter.Any(x => Char.IsWhiteSpace(x)))
                {
                    errorMsg += $"Parameter field cannot have white spaces.\n";
                }

                else
                {
                    var dict_param = HelperMethods.SplitParameter(part.Parameter);
                    if (dict_param.Count == 0)
                    {
                        errorMsg += $"Check the Parameters.\n";
                    }
                    else
                    {
                        foreach (var item in dict_param)
                        {
                            if (CustomRegex.LetterAndDigit().IsMatch(item.Key) == false)
                            {
                                errorMsg += $"Parameter field can only contain Letters and Numbers only.\n";
                                break;
                            }
                        }
                    }
                }


            }
            return errorMsg;
        }

        //MATERIAL VALIDATION
        private static string Material_Validation(ICBPart part)
        {
            string errorMsg = string.Empty;

            //if(part.Material.Contains("é"))
            //{
            //    MessageBox.Show("Contains é");

            //}

            if (HelperMethods.IsMaterialHMRparticleBoard(part.Material))
            {
                errorMsg = string.Empty;
            }
            else
            {
                if (TablePolytecBoardColors.CheckRecordExists(part.Material) == false)
                {
                    errorMsg += $"The Material Color '{part.Material}' is either wrong or not present in the EDI database.\n";
                }
            }
            return errorMsg;
        }

        //EDGE DESCRIPTION VALIDATION
        private static string EdgeDescription_Validation(ICBPart part)
        {
            string errorMsg = string.Empty;
            string concatEdgeDescription = part.TopEdgeDescription + part.BottomEdgeDescription + part.LeftEdgeDescription + part.RightEdgeDescription;

            if (concatEdgeDescription.Contains("contrasting", StringComparison.OrdinalIgnoreCase))
            {
                errorMsg += $"Replace edge description 'Contrasting' with a valid MaterialCode or 'Matching'.\n";
            }
            else
            {
                var edgeDescription = HelperMethods.GetEdgeColor(part).ToLower();
                if (edgeDescription != "matching")
                {
                    if (HelperMethods.IsMaterialHMRparticleBoard(part.Material) == false)
                    {
                        if(edgeDescription == "white" || edgeDescription == "black" ) errorMsg += $"Replace edge description with a Valid MaterialCode or 'Matching'.\n";
                    }
                }
            }
            return errorMsg;
        }


    }
}
