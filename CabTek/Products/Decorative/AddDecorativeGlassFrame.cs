
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeGlassFrame
    {
        public static void Add(CabinetPart part)
        {
            if (part.ProductType == PRODUCTTYPE.Decorative16mm) 
                Create16mmGlassFrame(part);
            else 
                Create18mmGlassFrame(part);
        }

        private static void Create16mmGlassFrame(CabinetPart part)
        {
            var configuredProduct = new Decorative16mmGlassFrame()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.ContrastingEdgeColour,
                Finish = part.Finish,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            CustomDrilling.AddDrillings(configuredProduct: configuredProduct, cabinet_part: part);
            PolytecConfiguredOrder.Order.AddProduct(configuredProduct);
        }


        private static void Create18mmGlassFrame(CabinetPart part)
        {
            var configuredProduct = new Decorative18mmGlassFrame()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.ContrastingEdgeColour,
                Finish = part.Finish,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
                CoatedSides = (string.Equals(part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            CustomDrilling.AddDrillings(configuredProduct: configuredProduct, cabinet_part: part);
            PolytecConfiguredOrder.Order.AddProduct(configuredProduct);
        }

    }
}
