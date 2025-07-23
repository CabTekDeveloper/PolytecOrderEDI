
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeDrawers
    {
        private static List<CabinetPart> LstDrawerBank { get; set; } = [];
        private static CabinetPart Part { get; set; } = new();

        public static void Add(List<CabinetPart> lstDrawerBank)
        {
            LstDrawerBank   = lstDrawerBank;
            Part            = lstDrawerBank[0];

            if (LstDrawerBank.Count == 1)
            {
                AddDecorativeDoor.Add(Part); //Single drawer front is added as a door
            }
            else
            {
                if (Part.ProductType == PRODUCTTYPE.Decorative16mm) Create16mmDrawerBank();
                else Create18mmDrawerBank();
            }
        }
        

        //Create 16mm Drawer Bank
        private static void Create16mmDrawerBank()
        {
            var ConfiguredDrawerBank = new Decorative16mmDrawers()
            {
                Quantity = Part.Quantity,
                Height = (decimal) LstDrawerBank.Sum(df => df.Height) + ((LstDrawerBank.Count - 1) * 3),  
                Width = (decimal) Part.Width,
                EdgeLocation = "11111",
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
                Colour = Part.Color,
                Finish = Part.Finish,
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredDrawerBank.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredDrawerBank.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }

            //AddDrillings  drawer fronts           
            foreach (var drawerFront in LstDrawerBank)
            {
                Part = drawerFront;

                var DrawerPiece = new Decorative16mmDrawersPiece()
                {
                    Height = (decimal)drawerFront.Height,
                    Width = (decimal) Part.Width,
                    EdgeLocation = Part.EdgeLocation,
                    AdditionalInstructions = Part.AdditionalInstructions,
                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                };
                //DecorativeGenericPieceCustomDrilling.Add(DrawerPiece, Part);
                CustomDrillingOnGenericPiece.AddDrillings(configuredPiece: DrawerPiece, cabinet_part:Part);
                ConfiguredDrawerBank.Pieces.Add(DrawerPiece);
            }
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredDrawerBank);
        }


        //Create 18mm Drawer Bank
        private static void Create18mmDrawerBank()
        {
            var ConfiguredDrawerBank = new Decorative18mmDrawers()
            {
                Quantity = Part.Quantity,
                Height = (decimal)LstDrawerBank.Sum(df => df.Height) + ((LstDrawerBank.Count - 1) * 3),
                Width = (decimal)Part.Width,
                EdgeLocation = "11111",
                HandleSystem = (Part.HandleSystem == "") ? null : Part.HandleSystem,
                Colour = Part.Color,
                Finish = Part.Finish,
                CoatedSides = (string.Equals(Part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2
            };

            if (Part.ContrastingEdgeColour != "" && Part.ContrastingEdgeFinish != "")
            {
                ConfiguredDrawerBank.ContrastEdgeColour = Part.ContrastingEdgeColour;
                ConfiguredDrawerBank.ContrastEdgeFinish = Part.ContrastingEdgeFinish;
            }
           
            foreach (var drawerFront in LstDrawerBank)
            {
                Part = drawerFront;
                var DrawerPiece = new Decorative18mmDrawersPiece()
                {
                    Height = (decimal)drawerFront.Height,
                    Width = (decimal)Part.Width,
                    EdgeLocation = Part.EdgeLocation,
                    AdditionalInstructions = Part.AdditionalInstructions,
                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{Part.CabinetNumber}-P{Part.PartNumber}" },
                };

                //DecorativeGenericPieceCustomDrilling.Add(DrawerPiece, Part);
                CustomDrillingOnGenericPiece.AddDrillings(configuredPiece: DrawerPiece, cabinet_part: Part);
                ConfiguredDrawerBank.Pieces.Add(DrawerPiece);
            }
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredDrawerBank);
        }

    }
}
