

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeDoor
    {
        public static void Add(CabinetPart part)
        {
            if (part.ProductType == PRODUCTTYPE.Decorative16mm) 
                Create16mmDoor(part);
            else 
                Create18mmDoor(part);
        }

        private static void Create16mmDoor(CabinetPart part)
        {
            var configuredProduct = new Decorative16mmDoor()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.Color,
                Finish = part.Finish,
            };

            //if Thickness is not 16, specify the Thickness.
            if (part.Thickness != 16)
            {
                configuredProduct.Thickness = part.Thickness.ToString();
            }

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            CustomDrilling.AddDrillings(configuredProduct: configuredProduct, cabinet_part: part);
            PolytecConfiguredOrder.Order.AddProduct(configuredProduct); 
        }


        private static void Create18mmDoor(CabinetPart part)
        {
            var configuredProduct = new Decorative18mmDoor()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.Color,
                Finish = part.Finish,
                CoatedSides = (string.Equals(part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2 
            };

            //if Thickness is not 18, specify the Thickness.
            if (part.Thickness != 18)
            {
                configuredProduct.Thickness = part.Thickness.ToString();
            }

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


