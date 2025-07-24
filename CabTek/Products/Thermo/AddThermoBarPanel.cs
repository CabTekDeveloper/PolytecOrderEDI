
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoBarPanel
    {
        private static VinylPart Part = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }

        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoBarPanel()
            {
                Quantity =  Part.Quantity,
                Height = (decimal) Part.Height,
                Width = (decimal) Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                PressedSides = Part.PressedSides,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                EdgeMould = Part.EdgeMould,
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,

                KickHeight = Part.KickHeight,
                DoubleMidRail = Part.DoubleMidRail,
                EvenlySizedProfiles = Part.EvenlySizedProfiles, 
            };

            //AddDrillings panels/profiles to Bar Panel
            double[] barPanelSizes =
            [
                Part.Profile1Size, Part.Profile2Size, Part.Profile3Size, Part.Profile4Size,
                Part.Profile5Size, Part.Profile6Size, Part.Profile7Size, Part.Profile8Size
            ];

            for ( int i = 0; i < Part.NumberOfPanels; i++ )
            {

                if (Part.EvenlySizedProfiles)
                {
                    ConfiguredProduct.AddPanel();
                }
                else
                {
                    ConfiguredProduct.AddPanel().Width = (decimal)barPanelSizes[i];
                }
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);

        }

    }

}


