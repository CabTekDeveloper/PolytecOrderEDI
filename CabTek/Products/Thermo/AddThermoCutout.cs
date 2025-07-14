
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoCutout
    {
        private static VinylPart Part = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            if (Part.HasCutout2) Create2Cutout();
            else CreateCutout();
        }


        private static void CreateCutout()
        {
            var ConfiguredProduct = new ThermoCutout()
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
                Profile =Part.FaceProfile,

                A = (decimal)Part.CutoutTopBorder,
                D = (decimal)Part.CutoutBottomBorder,
                B = (decimal)Part.CutoutLeftBorder,
                C = (decimal)Part.CutoutRightBorder,
            };
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

        private static void Create2Cutout()
        {
            var ConfiguredProduct = new Thermo2Cutout()
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
                D = (decimal)Part.CutoutBottomBorder,
                B = (decimal)Part.CutoutLeftBorder,
                C = (decimal)Part.CutoutRightBorder,
                E = (decimal)Part.CutoutLeftBorder2,
                F = (decimal)Part.CutoutRightBorder2,
                G = (decimal)Part.CutoutBottomBorder2,
                H = (decimal)Part.CutoutInternalHeight1
            };
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
