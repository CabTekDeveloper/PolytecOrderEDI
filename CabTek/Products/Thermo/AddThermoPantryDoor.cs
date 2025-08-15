

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoPantryDoor
    {
        private static VinylPart Part = new();

        public static void Add(VinylPart part)
        {
            Part = part;

            if (Part.PartName == PARTNAME.Pair) //If it's Pair Door, split the line into left and right doors.
            {
                Part.Quantity /= 2;
                Part.PartName = PARTNAME.Left;  //Add left door
                CreateProduct();
                Part.PartName = PARTNAME.Right; //Add right door
                CreateProduct();

                // Change properties to the initial values
                Part.PartName = PARTNAME.Pair;
                Part.Quantity *= 2;
            }
            else CreateProduct();

        }

        private static void CreateProduct()
        {
            var ConfiguredProduct = new ThermoPantryDoor()
            {
                Quantity =  Part.Quantity,
                Height = (decimal) Part.Height,
                Width = (decimal) Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                PressedSides = Part.PressedSides,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                EdgeMould = Part.EdgeMould,
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,
                KickHeight = Part.KickHeight,
                MidRailHeight = Part.MidRailHeight,
                DoubleMidRail = Part.DoubleMidRail,
                PanelCount = Part.PanelCount,
            };

            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, vinyl_part: Part);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }

}



