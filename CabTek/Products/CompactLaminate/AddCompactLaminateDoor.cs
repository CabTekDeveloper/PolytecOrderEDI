
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Windows.Forms.LinkLabel;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddCompactLaminateDoor
    {
        private static VinylPart CurrentProduct = new();

        public static void Add(VinylPart objCurrentProduct)
        {
            CurrentProduct = objCurrentProduct;

            if (CurrentProduct.PartName == PARTNAME.Pair)
            {
                CurrentProduct.Quantity = (CurrentProduct.Quantity / 2);

                //AddDrillings left door
                CurrentProduct.PartName = PARTNAME.Left;
                CreateProduct();

                //AddDrillings right door
                CurrentProduct.PartName = PARTNAME.Right;
                CreateProduct();
            }
            else
            {
                CreateProduct();
            }

        }


        private static void CreateProduct()
        {
            
            var Product = new CompactDoor()
            {
                Quantity = CurrentProduct.Quantity,
                Height = (decimal)CurrentProduct.Height,
                Width = (decimal)CurrentProduct.Width,
                Thickness = CurrentProduct.Thickness.ToString(),
                EdgeLocation = HelperMethods.ReplaceHbyTBLR(CurrentProduct.EdgeLocation),
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {CurrentProduct.EzeNo}" },
                AdditionalInstructions = CurrentProduct.AdditionalInstructions,
                Colour = CurrentProduct.Colour,
                Finish = CurrentProduct.Finish,
                //GrooveType = "None",
            };

            var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(CurrentProduct.EdgeLocation, CurrentProduct.HandleSystem, CurrentProduct.EdgeMould);

            Product.EdgeProfileTop = edgeProfiles["topEdge"];
            Product.EdgeProfileBottom = edgeProfiles["bottomEdge"];
            Product.EdgeProfileLeft = edgeProfiles["leftEdge"];
            Product.EdgeProfileRight = edgeProfiles["rightEdge"];

            Product = AddDrillings(Product);
            PolytecConfiguredOrder.Order.AddProduct(Product);
        }


        private static CompactDoor AddDrillings(CompactDoor Product)
        {
            PARTNAME partName = CurrentProduct.PartName;
            double height = CurrentProduct.Height;
            double width = CurrentProduct.Width;
            double hole1FromBot = CurrentProduct.Hole1FromBot;
            double hole2FromTop = CurrentProduct.Hole2FromTop;
            double hole3FromTop = CurrentProduct.Hole3FromTop;
            double hole4FromTop = CurrentProduct.Hole4FromTop;
            double hole5FromTop = CurrentProduct.Hole5FromTop;
            double hole6FromTop = CurrentProduct.Hole6FromTop;

            int numHoles = CurrentProduct.NumHoles;
            double hingeBlockInset = CurrentProduct.HingeBlockInset;

            double LINS = CurrentProduct.RINS;      // The LINS of the Back view is the RINS of the Front view
            double RINS = CurrentProduct.LINS;      // The RINS of the Back view is the LINS of the Front view 
            int DTYP1 = CurrentProduct.DTYP1;
            int DTYP2 = CurrentProduct.DTYP2;
            double INUP1 = CurrentProduct.INUP1;
            double INUP2 = CurrentProduct.INUP2;
            double holeRadius = (CurrentProduct.HDIA / 2);

            //Single drawer fronts are added as a door
            if (CurrentProduct.Product == PRODUCT.DrawerFront)
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

                hDepth = (hDepth > 0) ? hDepth : CompactDoorHingeBlockHole.depth;
                hRadius = (hRadius > 0) ? hRadius : CompactDoorHingeBlockHole.radius;
                hgGap = (hgGap > 0) ? hgGap : CompactDoorHingeBlockHole.gap;

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

                var drillingInfo = (CurrentProduct.Product == PRODUCT.DrawerFront ) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, partName.ToString()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

                if (drillingInfo.HasDrillingInfo)
                {
                    //double HoleDepth = drillingInfo.HoleDepth;
                    double holeDepth = CompactDrawerHoleDepth.holeDepth;

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
                    offset = SpotHole.inset;
                }
                else if (string.Equals(addToSide, "right", StringComparison.OrdinalIgnoreCase))
                {
                    offset = width - SpotHole.inset;
                }
                else
                {
                    offset = width / 2;
                }
               
                Product.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.inup, offset, (SpotHole.radius), SpotHole.depth);
            }

        }
        //End of 'AddDrillings' method
    }


}
