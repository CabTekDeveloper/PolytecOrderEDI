

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    
    static class CustomDrillingOnProduct
    {
        private static Product? ConfiguredProduct { get; set; }
        private static VinylPart? vinylPart { get; set; } = null;
        private static CabinetPart? cabPart { get; set; } = null;

        private static BuildParameter_Door? DoorParams { get; set; } = null;
        private static BuildParameter_DrawerFront? DrawerFrontParams { get; set; } = null;
        private static BuildParameter_Handle? HandleParams { get; set; } = null;


        private static PRODUCTTYPE ProductType { get; set; } = PRODUCTTYPE.None;
        private static PRODUCT ProductName { get; set; } = PRODUCT.None;
        private static PARTNAME PartName { get; set; } =PARTNAME.None;
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }

        private static HINGETYPE HingeType { get; set; } = HINGETYPE.None;
        private static double HingeCupInset { get; set; }
        private static double HingeBlockInset { get; set; }
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
        private static double HDIA { get; set; }
        private static double HoleDepth { get; set; }

        private static double CustomHole1LeftInset { get; set; }
        private static double CustomHole1TopInset { get; set; }
        private static double CustomHole1HDIA { get; set; }
        private static double CustomHole1Depth { get; set; }
        private static bool HasCustomHole1Drilling { get; set; }

        // Method to set the drilling properties.
        private static void SetDrillingProperties()
        {
            ProductType = vinylPart != null ? vinylPart.ProductType : cabPart != null ? cabPart.ProductType : PRODUCTTYPE.None;
            ProductName = vinylPart != null ? vinylPart.Product     : cabPart != null ? cabPart.Product     : PRODUCT.None;
            PartName    = vinylPart != null ? vinylPart.PartName    : cabPart != null ? cabPart.PartName    : PARTNAME.None;
            Height      = vinylPart != null ? vinylPart.Height      : cabPart != null ? cabPart.Height      : 0;
            Width       = vinylPart != null ? vinylPart.Width       : cabPart != null ? cabPart.Width       : 0;
            Thickness   = vinylPart != null ? vinylPart.Thickness   : cabPart != null ? cabPart.Thickness   : 0;

            if (cabPart != null)
            {
                DoorParams          = new(cabPart);
                DrawerFrontParams   = new(cabPart);
                HandleParams        = new(cabPart);
            }

            HingeType           = vinylPart != null ? vinylPart.HingeType           : DoorParams != null ? DoorParams.HingeType             : HINGETYPE.None;
            HingeCupInset       = vinylPart != null ? vinylPart.HingeCupInset       : DoorParams != null ? DoorParams.HingeCupInset         : 0;
            HingeBlockInset     = vinylPart != null ? vinylPart.HingeBlockInset     : DoorParams != null ? DoorParams.HingeBlockInset       : 0;
            BifoldHingeCupInset = vinylPart != null ? vinylPart.BifoldHingeCupInset : DoorParams != null ? DoorParams.BifoldHingeCupInset   : 0;
            HTOD                = vinylPart != null ? vinylPart.HTOD                : DoorParams != null ? DoorParams.HTOD                  : 0;

            Hole1FromBot        = vinylPart != null ? vinylPart.Hole1FromBot   : DoorParams != null ? DoorParams.Hole1FromBot  : 0;
            Hole2FromTop        = vinylPart != null ? vinylPart.Hole2FromTop   : DoorParams != null ? DoorParams.Hole2FromTop  : 0;
            Hole3FromTop        = vinylPart != null ? vinylPart.Hole3FromTop   : DoorParams != null ? DoorParams.Hole3FromTop  : 0;
            Hole4FromTop        = vinylPart != null ? vinylPart.Hole4FromTop   : DoorParams != null ? DoorParams.Hole4FromTop  : 0;
            Hole5FromTop        = vinylPart != null ? vinylPart.Hole5FromTop   : DoorParams != null ? DoorParams.Hole5FromTop  : 0;
            Hole6FromTop        = vinylPart != null ? vinylPart.Hole6FromTop   : DoorParams != null ? DoorParams.Hole6FromTop  : 0;
            NumHoles            = vinylPart != null ? vinylPart.NumHoles       : DoorParams != null ? DoorParams.NumHoles      : 0;

            LINS                = vinylPart != null ? vinylPart.RINS    : DrawerFrontParams != null ? DrawerFrontParams.RINS    : 0;     // The LINS of the Back view is the RINS of the Front view
            RINS                = vinylPart != null ? vinylPart.LINS    : DrawerFrontParams != null ? DrawerFrontParams.LINS    : 0;     // The RINS of the Back view is the LINS of the Front view 
            DTYP1               = vinylPart != null ? vinylPart.DTYP1   : DrawerFrontParams != null ? DrawerFrontParams.DTYP1   : 0;
            DTYP2               = vinylPart != null ? vinylPart.DTYP2   : DrawerFrontParams != null ? DrawerFrontParams.DTYP2   : 0;
            INUP1               = vinylPart != null ? vinylPart.INUP1   : DrawerFrontParams != null ? DrawerFrontParams.INUP1   : 0;
            INUP2               = vinylPart != null ? vinylPart.INUP2   : DrawerFrontParams != null ? DrawerFrontParams.INUP2   : 0;
            HDIA                = vinylPart != null ? vinylPart.HDIA    : DrawerFrontParams != null ? DrawerFrontParams.HDIA    : 0;

            HoleDepth           = vinylPart != null ? vinylPart.HoleDepth : 0;

            CustomHole1LeftInset    = vinylPart != null ? vinylPart.CustomHole1LeftInset    : 0;
            CustomHole1TopInset     = vinylPart != null ? vinylPart.CustomHole1TopInset     : 0;
            CustomHole1HDIA         = vinylPart != null ? vinylPart.CustomHole1HDIA         : 0;
            CustomHole1Depth        = vinylPart != null ? vinylPart.CustomHole1Depth        : 0;
            HasCustomHole1Drilling  = vinylPart != null ? (vinylPart.CustomHole1LeftInset > 0 && vinylPart.CustomHole1TopInset > 0 && vinylPart.CustomHole1HDIA > 0 && vinylPart.CustomHole1Depth > 0) : false;
        }


        public static void AddDrillings(Product configuredProduct, VinylPart? vinyl_part = null, CabinetPart? cabinet_part = null)
        {
            if (vinyl_part == null && cabinet_part == null) return;

            vinylPart = vinyl_part;
            cabPart = cabinet_part;
            ConfiguredProduct = configuredProduct;
            SetDrillingProperties();

            ////Add Custom Hole Drillings
            //if (HasCustomHole1Drilling)
            //{
            //    ConfiguredProduct.Features.AddHoleFromTopLeft()
            //}


            //Single drawer fronts are added as a door
            if (ProductName == PRODUCT.DrawerFront)
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
                return;
            }

            if (ProductName == PRODUCT.Panel)
            {
                if (DTYP1 == 1002)
                {
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                }
            }

            switch (PartName)
            {
                case PARTNAME.None:
                    break;

                case PARTNAME.Left:
                    AddHinges("right", HingeCupInset);
                    if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("right");
                    if (ProductType == PRODUCTTYPE.CompactLaminate) AddHingeBlocks("right", HingeBlockInset);
                    break;

                case PARTNAME.Right:
                    AddHinges("left", HingeCupInset);
                    if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("left");
                    if (ProductType == PRODUCTTYPE.CompactLaminate) AddHingeBlocks("left", HingeBlockInset);
                    break;

                case PARTNAME.Top:
                    AddHinges("top", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Bottom:
                    AddHinges("bottom", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Left_Bifold:
                    AddHinges("right", HingeCupInset);
                    AddHinges("left", BifoldHingeCupInset);
                    if (cabPart != null && !cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                    {
                        if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("right");
                    }
                    break;

                case PARTNAME.Right_Bifold:
                    AddHinges("left", HingeCupInset);
                    AddHinges("right", BifoldHingeCupInset);
                    if (cabPart != null && !cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                    {
                        if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("left");
                    }
                    break;

                case PARTNAME.Top_Bifold:
                    AddHinges("top", HingeCupInset);
                    AddHingeBlocks(addToSide: "bottom", offset: HingeBlockInset, hDepth: HoleDepth);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    if (NumHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor(INUP1);
                    break;

                case PARTNAME.Bottom_Bifold:
                    AddHinges("bottom", HingeCupInset);
                    AddHingeBlocks(addToSide: "top", offset: HingeBlockInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    if (NumHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor(INUP1);
                    break;

                case PARTNAME.Left_Leaf: //Left LEaf
                    AddHingeBlocks(addToSide: "left", offset: HingeBlockInset);
                    if (cabPart != null && cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                    {
                        if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("left");
                    }
                    break;

                case PARTNAME.Right_Leaf: //Right leaf
                    AddHingeBlocks(addToSide: "right", offset: HingeBlockInset);
                    if (cabPart != null && cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                    {
                        if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("right");
                    }
                    break;

                case PARTNAME.Top_Leaf: //Top leaf
                    AddHinges("bottom", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Bottom_Leaf:  //Bottom leaf
                    AddHinges("top", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Left_Blind_Panel:
                    AddHingeBlocks(addToSide: "left", offset: HingeBlockInset);
                    break;

                case PARTNAME.Right_Blind_Panel:
                    AddHingeBlocks(addToSide: "right", offset: HingeBlockInset);
                    break;

                case PARTNAME.Left_770: 
                    AddHinges("right", HingeCupInset);
                    AddHingeBlocks("left", offset: HingeBlockInset, hDepth: 1);
                    if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("right");
                    break;

                case PARTNAME.Right_770:
                    AddHinges("left", HingeCupInset);
                    AddHingeBlocks("right", offset: HingeBlockInset, hDepth: 1);
                    if (HandleParams != null && HandleParams.HasHandle) AddHandleOnFront("left");
                    break;

                case PARTNAME.Left_Leaf_770:
                    AddHingeBlocks("left", offset: HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("right", HingeCupInset);
                    break;

                case PARTNAME.Right_Leaf_770:
                    AddHingeBlocks("right", offset: HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("left", HingeCupInset);
                    break;

                default:
                    break;
            }

        }

        //AddDrillings Handle
        private static void AddHandleOnFront(string addToSide = "")
        {
            if (ConfiguredProduct != null)
            {
                if (HandleParams != null)
                {
                    var hDepth              = Thickness;
                    var hGap                = HandleParams.HoleGap;
                    var hRadius             = HandleParams.HoleDiameter / 2;
                    var sideInset           = HandleParams.SideInset;
                    bool isVerticalHandle   = HandleParams.IsHandleVertical;

                    var hole1TopInset   = HandleParams.Hole1TopInset;
                    var hole1LeftInset  = (addToSide == "left" || addToSide == "") ? (sideInset) : (Width - sideInset);

                    var hole2TopInset = HandleParams.Hole2TopInset;
                    double hole2LeftInset;
                    if (isVerticalHandle) hole2LeftInset = (addToSide == "left" || addToSide == "") ? (sideInset) : (Width - sideInset);
                    else hole2LeftInset = (addToSide == "left" || addToSide == "") ? (hole1LeftInset + hGap) : (hole1LeftInset - hGap);

                    //AddDrillings Holes from top left
                    if (hole1TopInset > 0 && hole1LeftInset > 0) { ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Front, hole1TopInset, hole1LeftInset, hRadius, hDepth); }  // Handle Hole1
                    if (hole2TopInset > 0 && hole2LeftInset > 0) { ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Front, hole2TopInset, hole2LeftInset, hRadius, hDepth); }  // Handle Hole2
                }
            }
        }


        //AddDrillings hinge holes to door from back
        private static void AddHinges(string addToSide, double offset)
        {
            if (ConfiguredProduct != null)
            {
                if (NumHoles > 0 && offset > 0 && HingeType != HINGETYPE.None)
                {
                    HingeType Htype = HingeType == HINGETYPE.Blum ? BorgEdi.Enums.HingeType.Blum : BorgEdi.Enums.HingeType.Hettich;

                    if (addToSide == "left" || addToSide == "right")
                    {
                        var primaryAxisReference = (addToSide == "left") ? PrimaryAxisReference.Left : PrimaryAxisReference.Right;

                        if (Hole1FromBot > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Bottom, Hole1FromBot));
                        if (Hole2FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole2FromTop));
                        if (Hole3FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole3FromTop));
                        if (Hole4FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole4FromTop));
                        if (Hole5FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole5FromTop));
                        if (Hole6FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Top, Hole6FromTop));
                    }
                    else if (addToSide == "top" || addToSide == "bottom")
                    {
                        var primaryAxisReference = (addToSide == "top") ? PrimaryAxisReference.Top : PrimaryAxisReference.Bottom;

                        if (Hole1FromBot > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Right, Hole1FromBot));
                        if (Hole2FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole2FromTop));
                        if (Hole3FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole3FromTop));
                        if (Hole4FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole4FromTop));
                        if (Hole5FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole5FromTop));
                        if (Hole6FromTop > 0) ConfiguredProduct.Features.Add(new Hinge(Htype, primaryAxisReference, offset, AdjacentAxisReference.Left, Hole6FromTop));
                    }
                }

            }
        }



        ////Method to add Hinge Block
        private static void AddHingeBlocks(string addToSide, double offset, double hDepth = 0, double hRadius = 0, double hGap = 0)
        {
            if (ConfiguredProduct != null)
            {
                if (NumHoles > 0 && offset > 0)
                {
                    hDepth  = (hDepth > 0)  ? hDepth    : (ProductType == PRODUCTTYPE.CompactLaminate) ? CompactDoorHingeBlockHole.Depth    : CustomHingeBlock.HoleDepth;
                    hRadius = (hRadius > 0) ? hRadius   : (ProductType == PRODUCTTYPE.CompactLaminate) ? CompactDoorHingeBlockHole.Radius   : CustomHingeBlock.HoleRadius;
                    hGap    = (hGap > 0)    ? hGap      : (ProductType == PRODUCTTYPE.CompactLaminate) ? CompactDoorHingeBlockHole.Gap      : CustomHingeBlock.HoleGap;

                    if (addToSide == "left" || addToSide == "right")
                    {
                        double leftOffset = (addToSide == "left") ? offset : (addToSide == "right") ? (Width - offset) : 0;

                        if (Hole1FromBot > 0) { DrillVerticalHingeblockHoles(Hole1FromBot); }
                        if (Hole2FromTop > 0) { DrillVerticalHingeblockHoles((Height - Hole2FromTop)); }
                        if (Hole3FromTop > 0) { DrillVerticalHingeblockHoles((Height - Hole3FromTop)); }
                        if (Hole4FromTop > 0) { DrillVerticalHingeblockHoles((Height - Hole4FromTop)); }
                        if (Hole5FromTop > 0) { DrillVerticalHingeblockHoles((Height - Hole5FromTop)); }
                        if (Hole6FromTop > 0) { DrillVerticalHingeblockHoles((Height - Hole6FromTop)); }

                        void DrillVerticalHingeblockHoles(double hingeHolePositionFromBottom)
                        {
                            if (ConfiguredProduct != null)
                            {
                                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hingeHolePositionFromBottom - (hGap / 2), leftOffset, hRadius, hDepth);
                                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hingeHolePositionFromBottom + (hGap / 2), leftOffset, hRadius, hDepth);
                            }
                        }
                    }

                    else if (addToSide == "top" || addToSide == "bottom")
                    {
                        double bottomOffset = (addToSide == "top") ? (Height - offset) : (addToSide == "bottom") ? offset : 0;

                        if (Hole1FromBot > 0) { DrillHorizontalHingeblockHoles(Width - Hole1FromBot); }
                        if (Hole2FromTop > 0) { DrillHorizontalHingeblockHoles((Hole2FromTop)); }
                        if (Hole3FromTop > 0) { DrillHorizontalHingeblockHoles((Hole3FromTop)); }
                        if (Hole4FromTop > 0) { DrillHorizontalHingeblockHoles((Hole4FromTop)); }
                        if (Hole5FromTop > 0) { DrillHorizontalHingeblockHoles((Hole5FromTop)); }
                        if (Hole6FromTop > 0) { DrillHorizontalHingeblockHoles((Hole6FromTop)); }

                        void DrillHorizontalHingeblockHoles(double hingeHolePositionFromLeft)
                        {
                            if (ConfiguredProduct != null)
                            {
                                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, hingeHolePositionFromLeft - (hGap / 2), hRadius, hDepth);
                                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, hingeHolePositionFromLeft + (hGap / 2), hRadius, hDepth);
                            }
                        }
                    }
                }
            }
        }



        //Method to add vertical holes on left and right on a panel
        public static void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
        {
            if (ConfiguredProduct != null)
            {
                if (DTYP > 0 && INUP > 0)
                {
                    var holePattern = (ProductName == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, PartName.ToString().ToLower()) : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

                    if (holePattern.HasDrillingInfo)
                    {
                        double holeDepth = (HoleDepth > 0) ? HoleDepth : (ProductType == PRODUCTTYPE.CompactLaminate) ? CompactDrawerHoleDepth.HoleDepth : holePattern.HoleDepth;
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

                            if (holePattern.NumHolesLeft > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Left Hole1
                            if (holePattern.NumHolesLeft > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Left Hole2
                            if (holePattern.NumHolesLeft > 2) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Left Hole3
                            if (holePattern.NumHolesLeft > 3) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Left Hole4
                            if (holePattern.NumHolesLeft > 4) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Left Hole5
                            if (holePattern.NumHolesLeft > 5) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Left Hole6
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

                            if (holePattern.NumHolesRight > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Right Hole1
                            if (holePattern.NumHolesRight > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Right Hole2
                            if (holePattern.NumHolesRight > 2) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Right Hole3
                            if (holePattern.NumHolesRight > 3) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Right Hole4
                            if (holePattern.NumHolesRight > 4) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Right Hole5
                            if (holePattern.NumHolesRight > 5) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Right Hole6
                        }
                    }
                }
            }
        }



        //Method to add extra holes to Hamper Bifold door to match the number of hinges on the Bifold leaf door
        private static void AddExtraHingeBlockHolesToHamperBifoldDoor(double INUP)
        {
            if (ConfiguredProduct != null && INUP > 0)
            {
                var drillingInfo = HolePatternDoorAndPanel.GetDrillingInfo(2);
                if (drillingInfo.HasDrillingInfo)
                {
                    double hole1Height = INUP + drillingInfo.LeftDefaultINUP;
                    double hole2Height = INUP + drillingInfo.LeftDefaultINUP + drillingInfo.Gap1;

                    if (Hole3FromTop > 0) { DrillExtraHoles((Hole3FromTop)); }  //hole3FromTop == hole3 from left
                    if (Hole4FromTop > 0) { DrillExtraHoles((Hole3FromTop)); }  //hole4FromTop == hole4 from left
                    if (Hole5FromTop > 0) { DrillExtraHoles((Hole5FromTop)); }  //hole5FromTop == hole5 from left
                    if (Hole6FromTop > 0) { DrillExtraHoles((Hole6FromTop)); }  //hole6FromTop == hole5 from left

                    void DrillExtraHoles(double leftOffset)
                    {
                        if (ConfiguredProduct != null)
                        {
                            if (drillingInfo.NumHolesLeft > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, HDIA / 2, drillingInfo.HoleDepth); }  // Left Hole1
                            if (drillingInfo.NumHolesLeft > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, HDIA / 2, drillingInfo.HoleDepth); }  //Left Hole2
                        }
                    }
                }
            }
        }


        //Method to add 35mm cup holes to 770 style leaf doors
        private static void Add35mmCupHolesTo770StyleLeadDoor(string addToSide, double offset)
        {
            if (ConfiguredProduct != null && offset > 0)
            {
                double cupHoleGap = DoorStyle770.CupHoleGap;
                double cupHoleRadius = DoorStyle770.CupHoleRadius;
                double cupHoleDepth = DoorStyle770.CupHoleDepth;

                double CupHole1FromTop = cupHoleGap - HTOD;
                double CupHole2FromTop = CupHole1FromTop + cupHoleGap;

                if (addToSide == "left")
                {
                    ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, CupHole1FromTop, offset, cupHoleRadius, cupHoleDepth);
                    ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, CupHole2FromTop, offset, cupHoleRadius, cupHoleDepth);
                }
                else if (addToSide == "right")
                {
                    offset = Width - offset;
                    ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, CupHole1FromTop, offset, cupHoleRadius, cupHoleDepth);
                    ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, CupHole2FromTop, offset, cupHoleRadius, cupHoleDepth);
                }
            }
        }



        //AddDrillings spot holes
        private static void AddSingleSpotHole(string addToSide = "")
        {
            if (ConfiguredProduct != null)
            {
                double offset = (addToSide == "left") ? SpotHole.Inset : (addToSide == "right") ? Width - SpotHole.Inset : Width / 2;
                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.Inup, offset, (SpotHole.Radius), SpotHole.Depth);
            }
        }



    }
}
