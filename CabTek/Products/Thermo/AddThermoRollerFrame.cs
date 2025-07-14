
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoRollerFrame
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }   

        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoRollerFrame()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                PressedSides = Part.PressedSides,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                EdgeMould = Part.EdgeMould,
                Colour = Part.Colour,
                Finish = Part.Finish,
                Profile = Part.FaceProfile,

                A = (decimal)Part.CutoutTopBorder,
                B = (decimal)Part.CutoutLeftBorder,
                C = (decimal)Part.CutoutRightBorder,
            };

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
