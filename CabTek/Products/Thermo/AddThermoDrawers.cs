
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoDrawers
    {
        private static List<VinylPart> LstDrawerBank { get; set; } = [];
        private static VinylPart Part { get; set; } = new();

        public static void Add(List<VinylPart> lstDrawerBank)
        {
            LstDrawerBank   = lstDrawerBank;
            Part            = lstDrawerBank[0];

            //Single drawer front is added as a door  
            if (LstDrawerBank.Count == 1) AddThermoDoor.Add(LstDrawerBank[0]); 
            else CreateProduct();
        }

        private static void CreateProduct()
        {
            var ConfiguredDrawerBank = new ThermoDrawers()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                PressedSides = Part.PressedSides,
                DrawerStyle = Part.StyleProfile,
                HandleSystem = Part.HandleSystem,
                EdgeMould = Part.EdgeMould,
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,
                //AdditionalInstructions= vinylPart.additionalInstructions,
            };


            //AddDrillings  drawer fronts           
            foreach(var drawerFront in LstDrawerBank)
            {
                Part = drawerFront;
                var DrawerPiece = new ThermoDrawersPiece()
                {
                    Height = (decimal)Part.DfHeight,
                    Width = (decimal) Part.Width,
                    EdgeLocation = drawerFront.EdgeLocation,
                    AdditionalInstructions = Part.AdditionalInstructions,
                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                };
                CustomDrillingOnGenericPiece.AddDrillings(configuredPiece: DrawerPiece, vinyl_part: Part);
                ConfiguredDrawerBank.Pieces.Add(DrawerPiece);
            }
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredDrawerBank);
        }
    }
}
