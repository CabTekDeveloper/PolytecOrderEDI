

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeCutout
    {
        public static void Add(CabinetPart part)
        {
            var cutoutParams = new BuildParameter_Cutout(part);

            if (cutoutParams.STTP == 0)
                AddDecorativeDoor.Add(part);
            else if (part.ProductType == PRODUCTTYPE.Decorative16mm) 
                Create16mmCutout(part, cutoutParams);
            else 
                Create18mmCutout(part, cutoutParams);
        }

        private static void Create16mmCutout( CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative16mmCutout()
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
                D = (decimal)cutoutParams.CutoutBottomBorder,
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


        private static void Create18mmCutout(CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative18mmCutout()
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
                D = (decimal)cutoutParams.CutoutBottomBorder,
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


        private static void Create16mm2Cutouts(CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative16mm2Cutout()
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
                D = (decimal)cutoutParams.CutoutBottomBorder,
                B = (decimal)cutoutParams.CutoutLeftBorder,
                C = (decimal)cutoutParams.CutoutRightBorder,
                //E = (decimal)cutoutParams.cutoutLeftBorder2,
                //F = (decimal)cutoutParams.cutoutRightBorder2,
                //G = (decimal)cutoutParams.cutoutBottomBorder2,
                //H = (decimal)cutoutParams.cutoutInternalHeight1
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredProduct.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredProduct.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(configuredProduct);

        }

        

        private static void Create18mm2Cutouts(CabinetPart part, BuildParameter_Cutout cutoutParams)
        {
            var configuredProduct = new Decorative18mm2Cutout()
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
                D = (decimal)cutoutParams.CutoutBottomBorder,
                B = (decimal)cutoutParams.CutoutLeftBorder,
                C = (decimal)cutoutParams.CutoutRightBorder,
                //E = (decimal)cutoutParams.cutoutLeftBorder2,
                //F = (decimal)cutoutParams.cutoutRightBorder2,
                //G = (decimal)cutoutParams.cutoutBottomBorder2,
                //H = (decimal)cutoutParams.cutoutInternalHeight1
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
