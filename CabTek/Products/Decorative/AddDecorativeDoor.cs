

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeDoor
    {
        private static CabinetPart Part { get; set; } = new();

        public static void Add(CabinetPart part)
        {
            Part = part;

            if (part.ProductType == PRODUCTTYPE.Decorative16mm) Create16mmDoor();
            else Create18mmDoor();
        }

        private static void Create16mmDoor( )
        {
            var ConfiguredProduct = new Decorative16mmDoor()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.Color,
                Finish = Part.Finish,
            };

            //if Thickness is not 16, specify the Thickness.
            if (Part.Thickness != 16)
            {
                ConfiguredProduct.Thickness = Part.Thickness.ToString();
            }

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            DecorativeProductCustomDrilling.Add(Part, ConfiguredProduct);       //AddDrillings Drilling
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);   //AddDrillings to Configured Order
        }


        private static void Create18mmDoor()
        {

            var ConfiguredProduct = new Decorative18mmDoor()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.Color,
                Finish = Part.Finish,
                CoatedSides = (string.Equals(Part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2 
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            DecorativeProductCustomDrilling.Add(Part, ConfiguredProduct);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }
    }


}


