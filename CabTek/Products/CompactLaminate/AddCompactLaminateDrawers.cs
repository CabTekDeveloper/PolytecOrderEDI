
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Windows.Forms.LinkLabel;

//using BorgEdi.Interfaces;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddCompactLaminateDrawers
    {
        private static List<VinylPart> lstDrawerBank = [];
        private static VinylPart CurrentProduct = new();

        public static void Add(List<VinylPart> LstDrawerBank)
        {
            lstDrawerBank = LstDrawerBank;

            if (lstDrawerBank.Count == 1)
            {
                AddCompactLaminateDoor.Add(lstDrawerBank[0]); //Single drawer front are added as a Thermo door
            }
            else
            {
                CreateProduct();
            }

        }


        public static void CreateProduct()
        {
            CurrentProduct = lstDrawerBank[0];

            var DrawerBank = new CompactDrawers()
            {
                Quantity = CurrentProduct.Quantity,
                Height = (decimal)CurrentProduct.Height,
                Width = (decimal)CurrentProduct.Width,
                Thickness = CurrentProduct.Thickness.ToString(),
                Profile = CurrentProduct.FaceProfile,
                Colour = CurrentProduct.Colour,
                Finish = CurrentProduct.Finish,
            };

           

            var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(CurrentProduct.EdgeLocation, CurrentProduct.HandleSystem, CurrentProduct.EdgeMould);

            DrawerBank.EdgeProfileTop = edgeProfiles["topEdge"];
            DrawerBank.EdgeProfileBottom = edgeProfiles["bottomEdge"];
            DrawerBank.EdgeProfileLeft = edgeProfiles["leftEdge"];
            DrawerBank.EdgeProfileRight = edgeProfiles["rightEdge"];

            //AddDrillings  drawer fronts           
            foreach (var drawerFront in lstDrawerBank)
            {
                CurrentProduct = drawerFront;

                var DrawerPiece = new CompactDrawersPiece()
                {
                    Height = (decimal)CurrentProduct.DfHeight,
                    Width = (decimal)CurrentProduct.Width,
                    EdgeLocation = CurrentProduct.EdgeLocation,
                    AdditionalInstructions = CurrentProduct.AdditionalInstructions,

                    LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {CurrentProduct.EzeNo}" },
                };
                
                //var edgeProfiles = HelperMethods.WorkoutCompactLaminateEdgeProfile(Part.edgeLocation, Part.handleSystem, Part.edgeMould);

                //DrawerPiece.EdgeProfileTop = edgeProfiles["topEdge"];
                //DrawerPiece.EdgeProfileBottom = edgeProfiles["bottomEdge"];
                //DrawerPiece.EdgeProfileLeft = edgeProfiles["leftEdge"];
                //DrawerPiece.EdgeProfileRight = edgeProfiles["rightEdge"];

                DrawerPiece = AddDrillings(DrawerPiece);
                DrawerBank.Pieces.Add(DrawerPiece);
            }

            PolytecConfiguredOrder.Order.AddProduct(DrawerBank);
        }


        //ADD DRILLINGS TO DRAWER FRONT
        public static CompactDrawersPiece AddDrillings(CompactDrawersPiece Product)
        {
            double width = CurrentProduct.Width;
            PARTNAME partName = CurrentProduct.PartName;
            double LINS = CurrentProduct.RINS;      // The LINS of the Back view is the RINS of the Front view
            double RINS = CurrentProduct.LINS;      // The RINS of the Back view is the LINS of the Front view 
            int DTYP1 = CurrentProduct.DTYP1;
            int DTYP2 = CurrentProduct.DTYP2;
            double INUP1 = CurrentProduct.INUP1;
            double INUP2 = CurrentProduct.INUP2;
            double holeRadius = (CurrentProduct.HDIA / 2);

            //AddDrillings Drilling
            AddLeftAndRightVerticalHoles(DTYP1, INUP1);
            AddLeftAndRightVerticalHoles(DTYP2, INUP2);
            AddSingleSpotHole(addToSide: partName.ToString());

            return Product;

            //Method to add hole drillings
            void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
            {
                if (DTYP == 0 || INUP == 0) return;

                var drillingInfo = (CurrentProduct.Product == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, partName.ToString()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

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


    }
}
