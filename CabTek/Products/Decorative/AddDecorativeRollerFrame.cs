using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeRollerFrame
    {
        public static void Add(CabinetPart part)
        {
           var cutoutParams    = new BuildParameter_Cutout(part);

            if (cutoutParams.STTP == 0)
                AddDecorativeDoor.Add(part);
            else if (part.ProductType == PRODUCTTYPE.Decorative16mm) 
                Create16mmRollerFrame(part,cutoutParams);
            else
                Create18mmRollerFrame(part,cutoutParams);
            
        }

        private static void Create16mmRollerFrame(CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative16mmRollerFrame()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.ContrastingEdgeColour,
                Finish = part.Finish,

                A = (decimal)cutoutParams.CutoutTopBorder,
                B = (decimal)cutoutParams.CutoutLeftBorder,
                C = (decimal)cutoutParams.CutoutRightBorder,
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(configuredProduct);
        }


        private static void Create18mmRollerFrame(CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative18mmRollerFrame()
            {
                Quantity = part.Quantity,
                Height = (decimal)part.Height,
                Width = (decimal)part.Width,
                EdgeLocation = part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{part.CabinetNumber}-P{part.PartNumber}" },
                AdditionalInstructions = part.AdditionalInstructions,
                Colour = part.ContrastingEdgeColour,
                Finish = part.Finish,
                CoatedSides = (string.Equals(part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2,

                A = (decimal)cutoutParams.CutoutTopBorder,
                B = (decimal)cutoutParams.CutoutLeftBorder,
                C = (decimal)cutoutParams.CutoutRightBorder,
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(configuredProduct);
        }

    }
}
