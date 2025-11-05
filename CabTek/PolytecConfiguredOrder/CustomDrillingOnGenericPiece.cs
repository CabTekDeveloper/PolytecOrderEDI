
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class CustomDrillingOnGenericPiece
    {
        private static GenericPiece? ConfiguredPiece { get; set; }
        private static VinylPart? vinylPart { get; set; } = null;
        private static CabinetPart? cabPart { get; set; } = null;

        private static BuildParameter_Door? DoorParams { get; set; } = null;
        private static BuildParameter_DrawerFront? DrawerFrontParams { get; set; } = null;
        private static BuildParameter_Handle? HandleParams { get; set; } = null;

        private static PRODUCTTYPE ProductType { get; set; } = PRODUCTTYPE.None;

        private static PRODUCT ProductName { get; set; } = PRODUCT.None;
        private static PARTNAME PartName { get; set; } = PARTNAME.None;
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }

        private static HINGETYPE HingeType { get; set; } = HINGETYPE.None;
        private static double HingeCupInset { get; set; }
        private static double HingeBlockInset { get; set; }
        private static double HingeBlockHDIA { get; set; }
        private static double HingeBlockHoleDepth { get; set; }

        private static double BifoldHingeCupInset { get; set; }
        private static double HTOD { get; set; }
        private static double Hole1FromBot { get; set; }
        private static double Hole2FromTop { get; set; }
        private static double Hole3FromTop { get; set; }
        private static double Hole4FromTop { get; set; }
        private static double Hole5FromTop { get; set; }
        private static double Hole6FromTop { get; set; }
        private static int NumHoles { get; set; }

        private static double LINS { get; set; }
        private static double RINS { get; set; }
        private static int DTYP1 { get; set; }
        private static int DTYP2 { get; set; }
        private static double INUP1 { get; set; }
        private static double INUP2 { get; set; }
        private static double DrawerHDIA { get; set; }
        private static double DrawerHoleDepth { get; set; }

        private static double CustomHole1LeftInset { get; set; }
        private static double CustomHole1TopInset { get; set; }
        private static double CustomHole1HDIA { get; set; }
        private static double CustomHole1Depth { get; set; }
        private static APPLYTARGET CustomHole1ApplyTarget { get; set; } = APPLYTARGET.None;
        private static bool HasCustomHole1Drilling { get; set; }


        private static void SetDrillingProperties()
        {
            ProductType = vinylPart != null ? vinylPart.ProductType : cabPart != null ? cabPart.ProductType : PRODUCTTYPE.None;
            ProductName = vinylPart != null ? vinylPart.Product     : cabPart != null ? cabPart.Product     : PRODUCT.None;
            PartName    = vinylPart != null ? vinylPart.PartName    : cabPart != null ? cabPart.PartName    : PARTNAME.None ;
            Height      = vinylPart != null ? vinylPart.Height      : cabPart != null ? cabPart.Height      : 0;
            Width       = vinylPart != null ? vinylPart.Width       : cabPart != null ? cabPart.Width       : 0;
            Thickness   = vinylPart != null ? vinylPart.Thickness   : cabPart != null ? cabPart.Thickness   : 0;

            //if (cabPart != null) DrawerFrontParams = new(cabPart);

            if (cabPart != null)
            {
                DoorParams = new(cabPart);
                DrawerFrontParams = new(cabPart);
                HandleParams = new(cabPart);
            }

            HingeType = vinylPart != null ? vinylPart.HingeType : DoorParams != null ? DoorParams.HingeType : HINGETYPE.None;
            HingeCupInset = vinylPart != null ? vinylPart.HingeCupInset : DoorParams != null ? DoorParams.HingeCupInset : 0;
            HingeBlockInset = vinylPart != null ? vinylPart.HingeBlockInset : DoorParams != null ? DoorParams.HingeBlockInset : 0;
            HingeBlockHDIA = vinylPart != null ? vinylPart.HingeBlockHDIA : 0;
            HingeBlockHoleDepth = vinylPart != null ? vinylPart.HingeBlockHoleDepth : 0;
            BifoldHingeCupInset = vinylPart != null ? vinylPart.BifoldHingeCupInset : DoorParams != null ? DoorParams.BifoldHingeCupInset : 0;
            HTOD = vinylPart != null ? vinylPart.HTOD : DoorParams != null ? DoorParams.HTOD : 0;

            Hole1FromBot = vinylPart != null ? vinylPart.Hole1FromBot : DoorParams != null ? DoorParams.Hole1FromBot : 0;
            Hole2FromTop = vinylPart != null ? vinylPart.Hole2FromTop : DoorParams != null ? DoorParams.Hole2FromTop : 0;
            Hole3FromTop = vinylPart != null ? vinylPart.Hole3FromTop : DoorParams != null ? DoorParams.Hole3FromTop : 0;
            Hole4FromTop = vinylPart != null ? vinylPart.Hole4FromTop : DoorParams != null ? DoorParams.Hole4FromTop : 0;
            Hole5FromTop = vinylPart != null ? vinylPart.Hole5FromTop : DoorParams != null ? DoorParams.Hole5FromTop : 0;
            Hole6FromTop = vinylPart != null ? vinylPart.Hole6FromTop : DoorParams != null ? DoorParams.Hole6FromTop : 0;
            NumHoles = vinylPart != null ? vinylPart.NumHoles : DoorParams != null ? DoorParams.NumHoles : 0;

            LINS        = vinylPart != null ? vinylPart.RINS        : DrawerFrontParams != null ? DrawerFrontParams.RINS    : 0;     // The LINS of the Back view is the RINS of the Front view
            RINS        = vinylPart != null ? vinylPart.LINS        : DrawerFrontParams != null ? DrawerFrontParams.LINS    : 0;      // The RINS of the Back view is the LINS of the Front view 
            DTYP1       = vinylPart != null ? vinylPart.DTYP1       : DrawerFrontParams != null ? DrawerFrontParams.DTYP1   : 0;
            DTYP2       = vinylPart != null ? vinylPart.DTYP2       : DrawerFrontParams != null ? DrawerFrontParams.DTYP2   : 0;
            INUP1       = vinylPart != null ? vinylPart.INUP1       : DrawerFrontParams != null ? DrawerFrontParams.INUP1   : 0;
            INUP2       = vinylPart != null ? vinylPart.INUP2       : DrawerFrontParams != null ? DrawerFrontParams.INUP2   : 0;
            DrawerHDIA        = vinylPart != null ? vinylPart.DrawerHDIA        : DrawerFrontParams != null ? DrawerFrontParams.HDIA    : 0;

            DrawerHoleDepth   = vinylPart != null ? vinylPart.DrawerHoleDepth   : 0 ;

            CustomHole1LeftInset    = vinylPart != null ? vinylPart.CustomHole1LeftInset    : 0;
            CustomHole1TopInset     = vinylPart != null ? vinylPart.CustomHole1TopInset     : 0;
            CustomHole1HDIA         = vinylPart != null ? vinylPart.CustomHole1HDIA         : 0;
            CustomHole1Depth        = vinylPart != null ? vinylPart.CustomHole1Depth        : 0;
            CustomHole1ApplyTarget  = vinylPart != null ? vinylPart.CustomHole1ApplyTarget  : APPLYTARGET.None;
            HasCustomHole1Drilling  = vinylPart != null ? (vinylPart.CustomHole1LeftInset > 0 && vinylPart.CustomHole1TopInset > 0 && vinylPart.CustomHole1HDIA > 0 && vinylPart.CustomHole1Depth > 0 && vinylPart.CustomHole1ApplyTarget != APPLYTARGET.None) : false;
        }

        public static void AddDrillings(GenericPiece configuredPiece, VinylPart? vinyl_part = null, CabinetPart? cabinet_part = null)
        {
            if (vinyl_part == null && cabinet_part == null) return;

            vinylPart = vinyl_part;
            cabPart = cabinet_part;

            ConfiguredPiece = configuredPiece;
            SetDrillingProperties();

            // AddDrillings drillings
            //Add Custom Hole Drillings
            if (HasCustomHole1Drilling)
            {
                AddCustomHole1FromTopLeft();
            }

            if (HingeType == HINGETYPE.BlumLdf)
            {
                HingeType = HINGETYPE.Blum; //Set the  hinge type to Blum
                AddHinges("right", HingeCupInset);
            }
            else if (HingeType == HINGETYPE.BlumRdf)
            {
                HingeType = HINGETYPE.Blum; //Set the  hinge type to Blum
                AddHinges("left", HingeCupInset);
            }
            else
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
            }
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
                        //double holeDepth = (HoleDepth > 0) ? HoleDepth : holePattern.HoleDepth;
                        double holeDepth = (DrawerHoleDepth > 0) ? DrawerHoleDepth : (ProductType == PRODUCTTYPE.CompactLaminate) ? CompactDrawerHoleDepth.HoleDepth : holePattern.HoleDepth;

                        var holeRadius = DrawerHDIA / 2;

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

        private static void AddCustomHole1FromTopLeft()
        {
            if (ConfiguredPiece != null)
            {
                var applyTarget = CustomHole1ApplyTarget == APPLYTARGET.Front ? ApplyTarget.Front : ApplyTarget.Back;
                var leftInset = CustomHole1ApplyTarget == APPLYTARGET.Front ? CustomHole1LeftInset : Width - CustomHole1LeftInset;
                ConfiguredPiece.Features.AddHoleFromTopLeft(applyTarget, CustomHole1TopInset, leftInset, CustomHole1HDIA / 2, CustomHole1Depth);
            }
        }



        //AddDrillings hinge holes to door from back
        private static void AddHinges(string addToSide, double offset)
        {
            if (ConfiguredPiece != null)
            {
                //if (NumHoles > 0 && offset > 0 && HingeType != HINGETYPE.None && HingeType != HINGETYPE.Blum11)
                if (NumHoles > 0 && offset > 0 && (HingeType == HINGETYPE.Blum || HingeType == HINGETYPE.Hettich))
                {
                    var Htype = HingeType == HINGETYPE.Blum ? BorgEdi.Enums.HingeType.Blum : BorgEdi.Enums.HingeType.Hettich;

                    if (addToSide == "left" || addToSide == "right")
                    {
                        var primaryAxisReference = (addToSide == "left") ? PrimaryAxisReference.Left : PrimaryAxisReference.Right;

                        if (Hole1FromBot > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Bottom, Hole1FromBot));
                        if (Hole2FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole2FromTop));
                        if (Hole3FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole3FromTop));
                        if (Hole4FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole4FromTop));
                        if (Hole5FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole5FromTop));
                        if (Hole6FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole6FromTop));
                    }
                    else if (addToSide == "top" || addToSide == "bottom")
                    {
                        var primaryAxisReference = (addToSide == "top") ? PrimaryAxisReference.Top : PrimaryAxisReference.Bottom;

                        if (Hole1FromBot > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Right, Hole1FromBot));
                        if (Hole2FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole2FromTop));
                        if (Hole3FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole3FromTop));
                        if (Hole4FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole4FromTop));
                        if (Hole5FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole5FromTop));
                        if (Hole6FromTop > 0) ConfiguredPiece.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole6FromTop));
                    }
                }

            }
        }
    }
}
