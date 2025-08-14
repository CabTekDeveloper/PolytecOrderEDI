using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddCompactLaminateDrawers
    {
        private static List<VinylPart> LstDrawerBank = [];
        private static VinylPart Part = new();

        public static void Add(List<VinylPart> lstDrawerBank)
        {
            LstDrawerBank = lstDrawerBank;

            if (LstDrawerBank.Count == 1)
            {
                AddCompactLaminateDoor.Add(LstDrawerBank[0]); //Single drawer front are added as a Thermo door
            }
            else
            {
                CreateProduct();
            }

        }


        public static void CreateProduct()
        {
            Part = LstDrawerBank[0];

            var ConfiguredDrawerBank = new CompactDrawers()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                Thickness = Part.Thickness.ToString(),
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,
            };

      

            var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(Part.EdgeLocation, Part.HandleSystem, Part.EdgeMould);

            ConfiguredDrawerBank.EdgeProfileTop = edgeProfiles["topEdge"];
            ConfiguredDrawerBank.EdgeProfileBottom = edgeProfiles["bottomEdge"];
            ConfiguredDrawerBank.EdgeProfileLeft = edgeProfiles["leftEdge"];
            ConfiguredDrawerBank.EdgeProfileRight = edgeProfiles["rightEdge"];

            //AddDrillings  drawer fronts           
            foreach (var drawerFront in LstDrawerBank)
            {
                Part = drawerFront;

                var DrawerPiece = new CompactDrawersPiece()
                {
                    Height = (decimal)Part.DfHeight,
                    Width = (decimal)Part.Width,
                    EdgeLocation = Part.EdgeLocation,
                    AdditionalInstructions = Part.AdditionalInstructions,

                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                };
                
                //var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(vinylPart.edgeLocation, vinylPart.handleSystem, vinylPart.edgeMould);

                //DrawerPiece.EdgeProfileTop = edgeProfiles["topEdge"];
                //DrawerPiece.EdgeProfileBottom = edgeProfiles["bottomEdge"];
                //DrawerPiece.EdgeProfileLeft = edgeProfiles["leftEdge"];
                //DrawerPiece.EdgeProfileRight = edgeProfiles["rightEdge"];

                DrawerPiece = AddDrillings(DrawerPiece);
                ConfiguredDrawerBank.Pieces.Add(DrawerPiece);
            }

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredDrawerBank);
        }


        //ADD DRILLINGS TO DRAWER FRONT
        public static CompactDrawersPiece AddDrillings(CompactDrawersPiece Product)
        {
            double width = Part.Width;
            PARTNAME partName = Part.PartName;
            double LINS = Part.RINS;      // The LINS of the Back view is the RINS of the Front view
            double RINS = Part.LINS;      // The RINS of the Back view is the LINS of the Front view 
            int DTYP1 = Part.DTYP1;
            int DTYP2 = Part.DTYP2;
            double INUP1 = Part.INUP1;
            double INUP2 = Part.INUP2;
            double holeRadius = (Part.HDIA / 2);

            //AddDrillings Drilling
            AddLeftAndRightVerticalHoles(DTYP1, INUP1);
            AddLeftAndRightVerticalHoles(DTYP2, INUP2);
            AddSingleSpotHole(addToSide: partName.ToString());

            return Product;

            //Method to add hole drillings
            void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
            {
                if (DTYP == 0 || INUP == 0) return;

                var drillingInfo = (Part.Product == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, partName.ToString()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

                if (drillingInfo.HasDrillingInfo)
                {
                    //double HoleDepth = drillingInfo.HoleDepth;
                    double holeDepth = CompactDrawerHoleDepth.HoleDepth;

                    //AddDrillings leftside drilling 
                    double hole1Height = INUP + drillingInfo.LeftDefaultINUP;
                    double hole2Height = INUP + drillingInfo.LeftDefaultINUP + drillingInfo.Gap1;
                    double hole3Height = INUP + drillingInfo.LeftDefaultINUP + drillingInfo.Gap1 + drillingInfo.Gap2;
                    double hole4Height = INUP + drillingInfo.LeftDefaultINUP + drillingInfo.Gap1 + drillingInfo.Gap2 + drillingInfo.Gap3;

                    if (LINS > 0)
                    {
                        double leftOffset = LINS;

                        if (drillingInfo.NumHolesLeft > 0) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Left Hole1
                        if (drillingInfo.NumHolesLeft > 1) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Left Hole2
                        if (drillingInfo.NumHolesLeft > 2) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Left Hole3
                        if (drillingInfo.NumHolesLeft > 3) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Left Hole4
                    }

                    //AddDrillings rightside drilling 
                    hole1Height = INUP + drillingInfo.RightDefaultINUP;
                    hole2Height = INUP + drillingInfo.RightDefaultINUP + drillingInfo.Gap1;
                    hole3Height = INUP + drillingInfo.RightDefaultINUP + drillingInfo.Gap1 + drillingInfo.Gap2;
                    hole4Height = INUP + drillingInfo.RightDefaultINUP + drillingInfo.Gap1 + drillingInfo.Gap2 + drillingInfo.Gap3;

                    if (RINS > 0)
                    {
                        double leftOffset = width - RINS;

                        if (drillingInfo.NumHolesRight > 0) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Right Hole1
                        if (drillingInfo.NumHolesRight > 1) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Right Hole2
                        if (drillingInfo.NumHolesRight > 2) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Right Hole3
                        if (drillingInfo.NumHolesRight > 3) { Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Right Hole4
                    }
                }
            }


            //AddDrillings spot holes
            void AddSingleSpotHole(string addToSide = "")
            {
                double offset;
                if (string.Equals(addToSide, "left", StringComparison.OrdinalIgnoreCase))
                {
                    offset = SpotHole.Inset;
                }
                else if (string.Equals(addToSide, "right", StringComparison.OrdinalIgnoreCase))
                {
                    offset = width - SpotHole.Inset;
                }
                else
                {
                    offset = width / 2;
                }

                Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.Inup, offset, (SpotHole.Radius), SpotHole.Depth);
            }
        }


    }
}
