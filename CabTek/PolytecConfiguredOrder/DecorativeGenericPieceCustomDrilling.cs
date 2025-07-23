
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class DecorativeGenericPieceCustomDrilling
    {
        private static GenericPiece? ConfiguredPiece { get; set; }
        private static CabinetPart Part { get; set; } = new();
        //Part Properties
        private static PARTNAME PartName { get; set; }
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }

        //DrawerFront properties since sigle DF are added as door
        private static double LINS { get; set; }
        private static double RINS { get; set; }
        private static int DTYP1 { get; set; }
        private static int DTYP2 { get; set; }
        private static double INUP1 { get; set; }
        private static double INUP2 { get; set; }
        private static double HDIA { get; set; }


        private static void SetDrillingProperties()
        {
            PartName = Part.PartName;
            Height = Part.Height;
            Width = Part.Width;
            Thickness = Part.Thickness;

            //DrawerFront Drilling Parameters
            var DrawerFrontParams = new BuildParameter_DrawerFront(Part);
            LINS = DrawerFrontParams.RINS;      // The LINS of the Back view is the RINS of the Front view
            RINS = DrawerFrontParams.LINS;      // The RINS of the Back view is the LINS of the Front view 
            DTYP1 = DrawerFrontParams.DTYP1;
            DTYP2 = DrawerFrontParams.DTYP2;
            INUP1 = DrawerFrontParams.INUP1;
            INUP2 = DrawerFrontParams.INUP2;
            HDIA = DrawerFrontParams.HDIA;
        }

        public static void Add(GenericPiece configuredPiece, CabinetPart part)
        {
            ConfiguredPiece = configuredPiece;
            Part = part;
            SetDrillingProperties();

            //AddDrillings
            AddLeftAndRightVerticalHoles(DTYP1, INUP1);
            AddLeftAndRightVerticalHoles(DTYP2, INUP2);
            AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
            AddHandleOnFront();
        }

        //AddDrillings Handle
        private static void AddHandleOnFront(string addToSide = "")
        {
            if (ConfiguredPiece != null)
            {
                var HandleParams = new BuildParameter_Handle(Part);     //Handle Drilling Paramters
                if (HandleParams.HasHandle)
                {
                    var hDepth = Thickness;
                    var hGap = HandleParams.HoleGap;
                    var hRadius = HandleParams.HoleDiameter / 2;
                    var sideInset = HandleParams.SideInset;
                    bool isVerticalHandle = HandleParams.IsHandleVertical;

                    var hole1TopInset = HandleParams.Hole1TopInset;
                    var hole1LeftInset = (addToSide == "left" || addToSide == "") ? (sideInset) : (Width - sideInset);

                    var hole2TopInset = HandleParams.Hole2TopInset;
                    double hole2LeftInset;
                    if (isVerticalHandle) hole2LeftInset = (addToSide == "left" || addToSide == "") ? (sideInset) : (Width - sideInset);
                    else hole2LeftInset = (addToSide == "left" || addToSide == "") ? (hole1LeftInset + hGap) : (hole1LeftInset - hGap);

                    //AddDrillings Holes from top left
                    if (hole1TopInset > 0 && hole1LeftInset > 0) { ConfiguredPiece.Features.AddHoleFromTopLeft(ApplyTarget.Front, hole1TopInset, hole1LeftInset, hRadius, hDepth); }  // Handle Hole1
                    if (hole2TopInset > 0 && hole2LeftInset > 0) { ConfiguredPiece.Features.AddHoleFromTopLeft(ApplyTarget.Front, hole2TopInset, hole2LeftInset, hRadius, hDepth); }  // Handle Hole2
                }
            }
        }

        //Method to add vertical holes 
        public static void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
        {
            if (ConfiguredPiece != null)
            {
                if (DTYP > 0 && INUP > 0)
                {
                    var holePattern = (Part.Product == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, PartName.ToString().ToLower()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

                    if (holePattern.HasDrillingInfo)
                    {
                        double holeDepth = holePattern.HoleDepth;
                        var holeRadius = HDIA / 2;

                        //AddDrillings leftside drilling 
                        double hole1Height = INUP + holePattern.LeftDefaultINUP;
                        double hole2Height = INUP + holePattern.LeftDefaultINUP + holePattern.Gap1;
                        double hole3Height = INUP + holePattern.LeftDefaultINUP + holePattern.Gap1 + holePattern.Gap2;
                        double hole4Height = INUP + holePattern.LeftDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3;
                        double hole5Height = INUP + holePattern.LeftDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3 + holePattern.Gap4;
                        double hole6Height = INUP + holePattern.LeftDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3 + holePattern.Gap4 + holePattern.Gap5;

                        if (LINS > 0)
                        {
                            double leftOffset = LINS;

                            if (holePattern.NumHolesLeft > 0) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Left Hole1
                            if (holePattern.NumHolesLeft > 1) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Left Hole2
                            if (holePattern.NumHolesLeft > 2) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Left Hole3
                            if (holePattern.NumHolesLeft > 3) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Left Hole4
                            if (holePattern.NumHolesLeft > 4) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Left Hole5
                            if (holePattern.NumHolesLeft > 5) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Left Hole6
                        }

                        //AddDrillings rightside drilling 
                        hole1Height = INUP + holePattern.RightDefaultINUP;
                        hole2Height = INUP + holePattern.RightDefaultINUP + holePattern.Gap1;
                        hole3Height = INUP + holePattern.RightDefaultINUP + holePattern.Gap1 + holePattern.Gap2;
                        hole4Height = INUP + holePattern.RightDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3;
                        hole5Height = INUP + holePattern.RightDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3 + holePattern.Gap4;
                        hole6Height = INUP + holePattern.RightDefaultINUP + holePattern.Gap1 + holePattern.Gap2 + holePattern.Gap3 + holePattern.Gap4 + holePattern.Gap5;

                        if (RINS > 0)
                        {
                            double leftOffset = Width - RINS;

                            if (holePattern.NumHolesRight > 0) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Right Hole1
                            if (holePattern.NumHolesRight > 1) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Right Hole2
                            if (holePattern.NumHolesRight > 2) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Right Hole3
                            if (holePattern.NumHolesRight > 3) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Right Hole4
                            if (holePattern.NumHolesRight > 4) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Right Hole5
                            if (holePattern.NumHolesRight > 5) { ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Right Hole6
                        }
                    }
                }
            }
        }


        //AddDrillings spot holes
        public static void AddSingleSpotHole(string addToSide = "")
        {
            if (ConfiguredPiece != null)
            {
                double offset = (addToSide == "left") ? SpotHole.inset : (addToSide == "right") ? Width - SpotHole.inset : Width / 2;
                ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.inup, offset, (SpotHole.radius), SpotHole.depth);
            }
        }


    }
}
