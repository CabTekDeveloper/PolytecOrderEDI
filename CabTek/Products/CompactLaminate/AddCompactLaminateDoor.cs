using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddCompactLaminateDoor
    {
        private static VinylPart Part = new();

        public static void Add(VinylPart objCurrentProduct)
        {
            Part = objCurrentProduct;

            if (Part.PartName == PARTNAME.Pair)
            {
                Part.Quantity = Part.Quantity / 2;

                Part.PartName = PARTNAME.Left;
                CreateProduct();

                Part.PartName = PARTNAME.Right;
                CreateProduct();
            }
            else
            {
                CreateProduct();
            }
        }

        private static void CreateProduct()
        {
            
            var ConfiguredProduct = new CompactDoor()
            {
                Quantity = Part.Quantity,
                Height = (decimal)Part.Height,
                Width = (decimal)Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = HelperMethods.ReplaceHbyTBLR(Part.EdgeLocation),
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.Colour,
                Finish = Part.Finish,
                //GrooveType = "None",
            };

            var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(Part.EdgeLocation, Part.HandleSystem, Part.EdgeMould);

            ConfiguredProduct.EdgeProfileTop      = edgeProfiles["topEdge"];
            ConfiguredProduct.EdgeProfileBottom   = edgeProfiles["bottomEdge"];
            ConfiguredProduct.EdgeProfileLeft     = edgeProfiles["leftEdge"];
            ConfiguredProduct.EdgeProfileRight    = edgeProfiles["rightEdge"];

            //ConfiguredProduct = AddDrillings(ConfiguredProduct);
            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, vinyl_part: Part);
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }


        private static CompactDoor AddDrillings(CompactDoor Product)
        {
            PARTNAME partName = Part.PartName;
            double height = Part.Height;
            double width = Part.Width;
            double hole1FromBot = Part.Hole1FromBot;
            double hole2FromTop = Part.Hole2FromTop;
            double hole3FromTop = Part.Hole3FromTop;
            double hole4FromTop = Part.Hole4FromTop;
            double hole5FromTop = Part.Hole5FromTop;
            double hole6FromTop = Part.Hole6FromTop;

            int numHoles = Part.NumHoles;
            double hingeBlockInset = Part.HingeBlockInset;

            double LINS = Part.RINS;      // The LINS of the Back view is the RINS of the Front view
            double RINS = Part.LINS;      // The RINS of the Back view is the LINS of the Front view 
            int DTYP1 = Part.DTYP1;
            int DTYP2 = Part.DTYP2;
            double INUP1 = Part.INUP1;
            double INUP2 = Part.INUP2;
            double holeRadius = (Part.HDIA / 2);

            //Single drawer fronts are added as a door
            if (Part.Product == PRODUCT.DrawerFront)
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                AddSingleSpotHole(addToSide: partName.ToString());

                return Product;
            }

            switch (partName)
            {
                case PARTNAME.Left:
                    AddHingeBlocks("right", hingeBlockInset);
                    break;
                
                case PARTNAME.Right:
                    AddHingeBlocks("left", hingeBlockInset);
                    break;

                default:
                    break;
            }

            return Product; 

            ////Method to add Hinge Block
            ////On a left or right doors, this will add vertical hinge block holes
            ////On a top or bottom doors, this will add horizontal hinge block holes
            void AddHingeBlocks(string addToSide, double offset, double hDepth = 0, double hRadius = 0, double hgGap = 0)
            {
                if (numHoles == 0 || offset == 0) return;

                hDepth = (hDepth > 0) ? hDepth : CompactDoorHingeBlockHole.Depth;
                hRadius = (hRadius > 0) ? hRadius : CompactDoorHingeBlockHole.Radius;
                hgGap = (hgGap > 0) ? hgGap : CompactDoorHingeBlockHole.Gap;

                if (addToSide == "left" || addToSide == "right")
                {
                    double leftOffset = (addToSide == "left") ? offset : (addToSide == "right") ? (width - offset) : 0;

                    if (hole1FromBot > 0) { DrillVerticalHingeblockHoles(hole1FromBot); }
                    if (hole2FromTop > 0) { DrillVerticalHingeblockHoles((height - hole2FromTop)); }
                    if (hole3FromTop > 0) { DrillVerticalHingeblockHoles((height - hole3FromTop)); }
                    if (hole4FromTop > 0) { DrillVerticalHingeblockHoles((height - hole4FromTop)); }
                    if (hole5FromTop > 0) { DrillVerticalHingeblockHoles((height - hole5FromTop)); }
                    if (hole6FromTop > 0) { DrillVerticalHingeblockHoles((height - hole6FromTop)); }

                    void DrillVerticalHingeblockHoles(double hingeHolePositionFromBottom)
                    {
                        Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hingeHolePositionFromBottom - (hgGap / 2), leftOffset, hRadius, hDepth);
                        Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hingeHolePositionFromBottom + (hgGap / 2), leftOffset, hRadius, hDepth);
                    }
                }

                else if (addToSide == "top" || addToSide == "bottom")
                {
                    double bottomOffset = (addToSide == "top") ? (height - offset) : (addToSide == "bottom") ? offset : 0;

                    if (hole1FromBot > 0) { DrillHorizontalHingeblockHoles(width - hole1FromBot); }
                    if (hole2FromTop > 0) { DrillHorizontalHingeblockHoles((hole2FromTop)); }
                    if (hole3FromTop > 0) { DrillHorizontalHingeblockHoles((hole3FromTop)); }
                    if (hole4FromTop > 0) { DrillHorizontalHingeblockHoles((hole4FromTop)); }
                    if (hole5FromTop > 0) { DrillHorizontalHingeblockHoles((hole5FromTop)); }
                    if (hole6FromTop > 0) { DrillHorizontalHingeblockHoles((hole6FromTop)); }

                    void DrillHorizontalHingeblockHoles(double hingeHolePositionFromLeft)
                    {
                        Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, hingeHolePositionFromLeft - (hgGap / 2), hRadius, hDepth);
                        Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, hingeHolePositionFromLeft + (hgGap / 2), hRadius, hDepth);
                    }
                }

            }

            //Method to add hole drillings
            void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
            {
                if (DTYP == 0 || INUP == 0) return;

                var drillingInfo = (Part.Product == PRODUCT.DrawerFront ) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, partName.ToString()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

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
        //End of 'AddDrillings' method
    }


}
