

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeCutout
    {
        private static CabinetPart Part { get; set; } = new();
        private static BuildParameter_Cutout CutoutParams { get; set; } = new();

        public static void Add(CabinetPart part)
        {
            Part = part;
            CutoutParams = new BuildParameter_Cutout(part);

            if (CutoutParams.STTP == 0)
            {
                AddDecorativeDoor.Add(part);
            }
            else
            {
                if (part.ProductType == PRODUCTTYPE.Decorative16mm) Create16mmCutout();
                else Create18mmCutout();
            }
        }

        private static void Create16mmCutout()
        {
            var ConfiguredProduct = new Decorative16mmCutout()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,

                A = (decimal)CutoutParams.CutoutTopBorder,
                D = (decimal)CutoutParams.CutoutBottomBorder,
                B = (decimal)CutoutParams.CutoutLeftBorder,
                C = (decimal)CutoutParams.CutoutRightBorder,
            };


            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }


        private static void Create18mmCutout()
        {
            var ConfiguredProduct = new Decorative18mmCutout()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,
                CoatedSides = (string.Equals(Part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2,

                A = (decimal)CutoutParams.CutoutTopBorder,
                D = (decimal)CutoutParams.CutoutBottomBorder,
                B = (decimal)CutoutParams.CutoutLeftBorder,
                C = (decimal)CutoutParams.CutoutRightBorder,
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }


        private static void Create16mm2Cutouts()
        {
            var ConfiguredProduct = new Decorative16mm2Cutout()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.CabinetNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,

                A = (decimal)CutoutParams.CutoutTopBorder,
                D = (decimal)CutoutParams.CutoutBottomBorder,
                B = (decimal)CutoutParams.CutoutLeftBorder,
                C = (decimal)CutoutParams.CutoutRightBorder,
                //E = (decimal)CutoutParams.cutoutLeftBorder2,
                //F = (decimal)CutoutParams.cutoutRightBorder2,
                //G = (decimal)CutoutParams.cutoutBottomBorder2,
                //H = (decimal)CutoutParams.cutoutInternalHeight1
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);

        }

        

        private static void Create18mm2Cutouts()
        {
            var ConfiguredProduct = new Decorative18mm2Cutout()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.CabinetNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,

                A = (decimal)CutoutParams.CutoutTopBorder,
                D = (decimal)CutoutParams.CutoutBottomBorder,
                B = (decimal)CutoutParams.CutoutLeftBorder,
                C = (decimal)CutoutParams.CutoutRightBorder,
                //E = (decimal)CutoutParams.cutoutLeftBorder2,
                //F = (decimal)CutoutParams.cutoutRightBorder2,
                //G = (decimal)CutoutParams.cutoutBottomBorder2,
                //H = (decimal)CutoutParams.cutoutInternalHeight1
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);

        }

    }
}
