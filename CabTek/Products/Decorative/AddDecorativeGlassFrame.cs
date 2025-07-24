
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeGlassFrame
    {
        private static CabinetPart Part { get; set; } = new();

        public static void Add(CabinetPart part)
        {
            Part = part;
            if (Part.ProductType == PRODUCTTYPE.Decorative16mm) Create16mmGlassFrame();
            else Create18mmGlassFrame();
        }

        private static void Create16mmGlassFrame( )
        {
            var ConfiguredProduct = new Decorative16mmGlassFrame()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            //DecorativeProductCustomDrilling.Add(Part, ConfiguredProduct);
            //CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, vinyl_part: Part);
            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, cabinet_part: Part);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }


        private static void Create18mmGlassFrame()
        {
            var ConfiguredProduct = new Decorative18mmGlassFrame()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                EdgeLocation = Part.EdgeLocation,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.ContrastingEdgeColour,
                Finish = Part.Finish,
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
                CoatedSides = (string.Equals(Part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredProduct.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredProduct.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            //DecorativeProductCustomDrilling.Add(Part, ConfiguredProduct);
            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, cabinet_part: Part);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
