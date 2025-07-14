//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

namespace PolytecOrderEDI
{
    static class ICB_FilterParts
    {
        private static string[] FilterBy_PartDescription { get; } = [   "Left_End", "Right_End", "Top", "Bottom", "Back", "Left_Back", "Right_Back", "Shelf", "Cnr_Drw", "Connector", "Recessed_Rail", "Mitred", "Duct_Side", "Duct_Face", "RH_Front_Panel" ];
        private static string[] FilterBy_CNCCODE { get; } = ["USFILL_L", "USFILL_R", "VCUTSEL"];
        private static string[] FilterBy_Parameter { get; } = ["PEDI=1"]; //If PEDI==1, we will keep the part

        public static List<ICBPart> Filter(List<ICBPart> icbParts)
        {
            var filteredList = icbParts.ToList();
            try
            {
                foreach(var part in filteredList.ToList())
                {
                    if (part.Quantity == 0)  filteredList.Remove(part);
                    else if (FilterBy_Parameter.Any(filterStr => !part.Parameter.Contains(filterStr, StringComparison.OrdinalIgnoreCase)))              filteredList.Remove(part);
                    //else if (FilterBy_PartDescription.Any(filterStr => part.PartDescription.Contains(filterStr, StringComparison.OrdinalIgnoreCase)))   filteredList.Remove(part);
                    //else if (FilterBy_CNCCODE.Any(filterStr => part.CNCCODE.Contains(filterStr, StringComparison.OrdinalIgnoreCase)))                   filteredList.Remove(part);
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = $"Error in filtering ICB file";
                return filteredList;
            }

        }

 
    }
}
