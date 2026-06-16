
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddDecorativeDrawers
    {
        private const int DrawerGap = 3;
        private const string DrawerBankEdge = "11111";

        public static void Add(List<CabinetPart> lstDrawerBank)
        {
            var part = lstDrawerBank[0];
            if (lstDrawerBank.Count == 1)
                AddDecorativeDoor.Add(part); //Single drawer front is added as a door
            else if (part.ProductType == PRODUCTTYPE.Decorative16mm) 
                Create16mmDrawerBank(lstDrawerBank);
            else 
                Create18mmDrawerBank(lstDrawerBank);
        }
        

        //Create 16mm Drawer Bank
        private static void Create16mmDrawerBank( List<CabinetPart> lstDrawerBank)
        {
            var part = lstDrawerBank[0];
            var configuredDrawerBank = new Decorative16mmDrawers()
            {
                Quantity = part.Quantity,
                Height = (decimal) lstDrawerBank.Sum(df => df.Height) + ((lstDrawerBank.Count - 1) * DrawerGap),  
                Width = (decimal) part.Width,
                EdgeLocation = DrawerBankEdge,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
                Colour = part.Color,
                Finish = part.Finish,
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredDrawerBank.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredDrawerBank.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }

            //AddDrillings  drawer fronts           
            foreach (var drawerFront in lstDrawerBank)
            {
                var drawerPiece = new Decorative16mmDrawersPiece()
                {
                    Height = (decimal)drawerFront.Height,
                    Width = (decimal)drawerFront.Width,
                    EdgeLocation = drawerFront.EdgeLocation,
                    AdditionalInstructions = drawerFront.AdditionalInstructions,
                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{drawerFront.CabinetNumber}-P{drawerFront.PartNumber}" },
                };

                CustomDrilling.AddDrillings(configuredPiece: drawerPiece , cabinet_part: drawerFront);
                configuredDrawerBank.Pieces.Add(drawerPiece);
            }
            PolytecConfiguredOrder.Order.AddProduct(configuredDrawerBank);
        }

        //Create 18mm Drawer Bank
        private static void Create18mmDrawerBank(List<CabinetPart> lstDrawerBank)
        {
            var part = lstDrawerBank[0];
            var configuredDrawerBank = new Decorative18mmDrawers()
            {
                Quantity = part.Quantity,
                Height = (decimal)lstDrawerBank.Sum(df => df.Height) + ((lstDrawerBank.Count - 1) * DrawerGap),
                Width = (decimal)part.Width,
                EdgeLocation = DrawerBankEdge,
                HandleSystem = (part.HandleSystem == "") ? null : part.HandleSystem,
                Colour = part.Color,
                Finish = part.Finish,
                CoatedSides = (string.Equals(part.Side, "SS", StringComparison.OrdinalIgnoreCase)) ? 1 : 2
            };

            if (part.ContrastingEdgeColour != "" && part.ContrastingEdgeFinish != "")
            {
                configuredDrawerBank.ContrastEdgeColour = part.ContrastingEdgeColour;
                configuredDrawerBank.ContrastEdgeFinish = part.ContrastingEdgeFinish;
            }
           
            foreach (var drawerFront in lstDrawerBank)
            {
                var drawerPiece = new Decorative18mmDrawersPiece()
                {
                    Height = (decimal)drawerFront.Height,
                    Width = (decimal)drawerFront.Width,
                    EdgeLocation = drawerFront.EdgeLocation,
                    AdditionalInstructions = drawerFront.AdditionalInstructions,
                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"C{drawerFront.CabinetNumber}-P{drawerFront.PartNumber}" },
                };

                CustomDrilling.AddDrillings(configuredPiece: drawerPiece , cabinet_part: drawerFront);
                configuredDrawerBank.Pieces.Add(drawerPiece );
            }
            PolytecConfiguredOrder.Order.AddProduct(configuredDrawerBank);
        }
    }
}
