
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoGlassFrame
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            if (Part.PartName == PARTNAME.Pair) //AddDrillings pair product as Left and Right product
            {
                Part.Quantity = Part.Quantity/2;

                Part.PartName = PARTNAME.Left; 
                CreateProduct();

                Part.PartName = PARTNAME.Right; 
                CreateProduct();

                // Change properties to the initial values
                Part.PartName = PARTNAME.Pair;
                Part.Quantity *= 2;
            }
            else CreateProduct();
        }


        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoGlassFrame()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                PressedSides = Part.PressedSides,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                GlassFrameStyle = Part.StyleProfile,

                AdditionalInstructions = Part.AdditionalInstructions,
                EdgeMould = Part.EdgeMould,
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,
            };

            //CustomDrillingOnProduct.AddDrillings(Part, ConfiguredProduct);  //AddDrillings drilling
            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, vinyl_part: Part);  //AddDrillings drilling
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }
       
    }
}
