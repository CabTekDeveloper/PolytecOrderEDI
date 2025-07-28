
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class CustomDrillingOnGenericPiece
    {
        private static GenericPiece? ConfiguredPiece { get; set; }
        private static VinylPart? vinylPart { get; set; } = null;
        private static CabinetPart? cabPart { get; set; } = null;
        private static BuildParameter_DrawerFront DrawerFrontParams { get; set; } = new();
        private static PRODUCT ProductName { get; set; } = PRODUCT.None;
        private static PARTNAME PartName { get; set; } = PARTNAME.None;
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }
        private static double LINS { get; set; }
        private static double RINS { get; set; }
        private static int DTYP1 { get; set; }
        private static int DTYP2 { get; set; }
        private static double INUP1 { get; set; }
        private static double INUP2 { get; set; }
        private static double HDIA { get; set; }
        private static double HoleDepth { get; set; }

        private static void SetDrillingProperties()
        {
            ProductName = vinylPart != null ? vinylPart.Product     : cabPart != null ? cabPart.Product     : PRODUCT.None;
            PartName    = vinylPart != null ? vinylPart.PartName    : cabPart != null ? cabPart.PartName    : PARTNAME.None ;
            Height      = vinylPart != null ? vinylPart.Height      : cabPart != null ? cabPart.Height      : 0;
            Width       = vinylPart != null ? vinylPart.Width       : cabPart != null ? cabPart.Width       : 0;
            Thickness   = vinylPart != null ? vinylPart.Thickness   : cabPart != null ? cabPart.Thickness   : 0;

            if (cabPart != null) DrawerFrontParams = new(cabPart);

            LINS        = vinylPart != null ? vinylPart.RINS        : DrawerFrontParams != null ? DrawerFrontParams.RINS    : 0;     // The LINS of the Back view is the RINS of the Front view
            RINS        = vinylPart != null ? vinylPart.LINS        : DrawerFrontParams != null ? DrawerFrontParams.LINS    : 0;      // The RINS of the Back view is the LINS of the Front view 
            DTYP1       = vinylPart != null ? vinylPart.DTYP1       : DrawerFrontParams != null ? DrawerFrontParams.DTYP1   : 0;
            DTYP2       = vinylPart != null ? vinylPart.DTYP2       : DrawerFrontParams != null ? DrawerFrontParams.DTYP2   : 0;
            INUP1       = vinylPart != null ? vinylPart.INUP1       : DrawerFrontParams != null ? DrawerFrontParams.INUP1   : 0;
            INUP2       = vinylPart != null ? vinylPart.INUP2       : DrawerFrontParams != null ? DrawerFrontParams.INUP2   : 0;
            HDIA        = vinylPart != null ? vinylPart.HDIA        : DrawerFrontParams != null ? DrawerFrontParams.HDIA    : 0;

            HoleDepth   = vinylPart != null ? vinylPart.HoleDepth   : 0 ;
        }

        public static void AddDrillings( GenericPiece configuredPiece, VinylPart? vinyl_part = null, CabinetPart? cabinet_part = null)
        {
            if (vinyl_part == null && cabinet_part == null) return;

            vinylPart = vinyl_part;
            cabPart = cabinet_part;

            ConfiguredPiece = configuredPiece;
            SetDrillingProperties();

            // AddDrillings drillings
            AddLeftAndRightVerticalHoles(DTYP1, INUP1);
            AddLeftAndRightVerticalHoles(DTYP2, INUP2);
            AddSingleSpotHole(addToSide: PartName.ToString().ToLower());   
        }

        //Method to add vertical holes on left and right on a panel
        public static void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
        {
            if (ConfiguredPiece != null)
            {
                if (DTYP > 0 && INUP > 0)
                {
                    var holePattern = (ProductName == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, PartName.ToString().ToLower()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

                    if (holePattern.HasDrillingInfo)
                    {
                        double holeDepth = (HoleDepth > 0) ? HoleDepth : holePattern.HoleDepth;
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
                double offset = (addToSide == "left") ? SpotHole.Inset : (addToSide == "right") ? Width - SpotHole.Inset : Width / 2;
                ConfiguredPiece.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.Inup, offset, (SpotHole.Radius), SpotHole.Depth);
            }
        }


    }
}
