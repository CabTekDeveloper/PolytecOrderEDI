
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeRollerFrame
    {
        private static CabinetPart Part { get; set; } = new();
        private static BuildParameter_Cutout CutoutParams { get; set; } = new();

        public static void Add(CabinetPart part)
        {
            Part            = part;
            CutoutParams    = new BuildParameter_Cutout(Part);

            if (CutoutParams.STTP == 0)
            {
                AddDecorativeDoor.Add(Part);
            }
            else
            {
                if (Part.ProductType == PRODUCTTYPE.Decorative16mm) Create16mmRollerFrame();
                else Create18mmGlassFrame();
            }
        }

        private static void Create16mmRollerFrame()
        {
            
            var ConfiguredProduct = new Decorative16mmRollerFrame()
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


        private static void Create18mmGlassFrame()
        {
            var ConfiguredProduct = new Decorative18mmRollerFrame()
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

    }
}
