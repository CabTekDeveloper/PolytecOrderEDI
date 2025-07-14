
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoPanel
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }

        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoPanel()
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
            };

            if (Part.Return1Edge != "" || Part.Return2Edge != "")
            {
                ConfiguredProduct.Returns = [];
                if (Part.Return1Edge != "")
                {
                    ConfiguredProduct.Returns.Add(new Return() { Edge = HelperMethods.GetReturnPanelEdge(Part.Return1Edge), Thickness = Part.Return1Thickness, Width = Part.Return1Width });
                }
                if (Part.Return2Edge != "")
                {
                    ConfiguredProduct.Returns.Add(new Return() { Edge = HelperMethods.GetReturnPanelEdge(Part.Return2Edge), Thickness = Part.Return2Thickness, Width = Part.Return2Width });
                }
            }
            
            ThermoProductCustomDrilling.AddDrillings(Part, ConfiguredProduct);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
