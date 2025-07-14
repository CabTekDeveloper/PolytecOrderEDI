
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
                EvenlySizedProfiles = true, 
            };

            //AddDrillings panels/profiles to Bar Panel
            int profileCount = Int32.Parse(Part.StyleProfile.Replace("P", ""));
            for( int i = 0; i < profileCount; i++ )
            {
                ConfiguredProduct.AddPanel();
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);

        }

    }

}


