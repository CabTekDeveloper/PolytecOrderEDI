
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{

    static class DecorativeProductCustomDrilling
    {
        private static Product? ConfiguredProduct { get; set; } 
        private static CabinetPart Part { get; set; } = new();

        //Part Properties
        private static PARTNAME PartName { get; set; }
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }
        
        //Door Parameters
        private static HINGETYPE HingeType { get; set; }
        private static double HingeCupInset { get; set; }
        private static double HingeBlockInset { get; set; }
        private static double HTOD { get; set; }
        private static double BifoldHingeCupInset { get; set; }
        private static double Hole1FromBot { get; set; }
        private static double Hole2FromTop { get; set; }
        private static double Hole3FromTop { get; set; }
        private static double Hole4FromTop { get; set; }
        private static double Hole5FromTop { get; set; }
        private static double Hole6FromTop { get; set; }
        private static int NumHoles { get; set; }

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
            PartName    = Part.PartName;
            Height      = Part.Height;
            Width       = Part.Width;
            Thickness   = Part.Thickness;

            var DoorParams  = new BuildParameter_Door(Part); //Door Drilling Parameters
            HingeType           = DoorParams.HingeType;
            HingeCupInset       = DoorParams.HingeCupInset;
            HingeBlockInset     = DoorParams.HingeBlockInset;
            HTOD                = DoorParams.HTOD;
            BifoldHingeCupInset = DoorParams.BifoldHingeCupInset;
            Hole1FromBot        = DoorParams.Hole1FromBot;
            Hole2FromTop        = DoorParams.Hole2FromTop;
            Hole3FromTop        = DoorParams.Hole3FromTop;
            Hole4FromTop        = DoorParams.Hole4FromTop;
            Hole5FromTop        = DoorParams.Hole5FromTop;
            Hole6FromTop        = DoorParams.Hole6FromTop;
            NumHoles            = DoorParams.NumHoles;

            var DrawerFrontParams = new BuildParameter_DrawerFront(Part);   //DrawerFront Drilling Parameters
            LINS    = DrawerFrontParams.RINS;      // The LINS of the Back view is the RINS of the Front view
            RINS    = DrawerFrontParams.LINS;      // The RINS of the Back view is the LINS of the Front view 
            DTYP1   = DrawerFrontParams.DTYP1;
            DTYP2   = DrawerFrontParams.DTYP2;
            INUP1   = DrawerFrontParams.INUP1;
            INUP2   = DrawerFrontParams.INUP2;
            HDIA    = DrawerFrontParams.HDIA;
        }


        public static void Add( CabinetPart part, Product configuredProduct)
        {
            Part = part;
            ConfiguredProduct = configuredProduct;
            SetDrillingProperties();

            //Single drawer fronts are added as a  door
            if (Part.Product == PRODUCT.DrawerFront)
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
                return;
            }

            switch (PartName)
            {
                case PARTNAME.None:
                    break;

                case PARTNAME.Top:
                    AddHinges("top", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Top_Bifold:
                    AddHinges("top", HingeCupInset);
                    AddHingeBlocks(addToSide: "bottom", offset: HingeBlockInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    if (NumHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor( INUP1);
                    break;

                case PARTNAME.Bottom_Leaf:
                    AddHinges( "top", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                //case "hamp_door_bottom":
                //    AddHinges("bottom", hingeCupInset);
                //    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                //    break;

                //case "hamp_door_bottom_bifold":
                //    AddHinges("bottom", hingeCupInset);
                //    AddHingeBlocks(addToSide: "top", offset: hingeBlockInset);
                //    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                //    if (numHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor(INUP1);
                //    break;

                //case "hamp_door_top_leaf":
                //    AddHinges("bottom", hingeCupInset);
                //    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                //    break;

                case PARTNAME.Left:
                    AddHinges("right", HingeCupInset);
                    //AddHandleOnFront("right");
                    AddHandleOnFront( "right");
                    break;

                case PARTNAME.Right:
                    AddHinges("left", HingeCupInset);
                    //AddHandleOnFront("left");
                    AddHandleOnFront("left");
                    break;

                case PARTNAME.Left_Bifold:
                    AddHinges("right", HingeCupInset);
                    AddHinges("left", BifoldHingeCupInset);
                    if (!Part.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase)) AddHandleOnFront("right");
                    break;

                case PARTNAME.Right_Bifold:
                    AddHinges("left", HingeCupInset);
                    AddHinges("right", BifoldHingeCupInset);
                    if (!Part.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase)) AddHandleOnFront("left");
                    break;

                case PARTNAME.Right_Leaf:
                    AddHingeBlocks(addToSide: "right", offset: HingeBlockInset);
                    if (Part.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase)) AddHandleOnFront("right");
                    break;

                case PARTNAME.Left_Leaf:
                    AddHingeBlocks(addToSide: "left", offset: HingeBlockInset);
                    if (Part.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase)) AddHandleOnFront("left");
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
                    AddHandleOnFront("right");
                    break;

                case PARTNAME.Right_770:
                    AddHinges("left", HingeCupInset);
                    AddHingeBlocks("right", offset: HingeBlockInset, hDepth: 1);
                    AddHandleOnFront("left");
                    break;

                case PARTNAME.Right_Leaf_770:
                    AddHingeBlocks("right", offset: HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("left", HingeCupInset);
                    break;

                case PARTNAME.Left_Leaf_770:
                    AddHingeBlocks("left", offset: HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("right", HingeCupInset);
                    break;

                default:
                    break;
            }
        }

        //AddDrillings Handle
        private static void AddHandleOnFront( string addToSide = "") 
        {
            if (ConfiguredProduct != null)
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
                    HingeType Htype = (HingeType == HINGETYPE.Blum) ? BorgEdi.Enums.HingeType.Blum : BorgEdi.Enums.HingeType.Hettich;

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
        private static void AddHingeBlocks( string addToSide, double offset, double hDepth = 0, double hRadius = 0, double hGap = 0)
        {
            if (ConfiguredProduct != null)
            {
                if (NumHoles > 0 && offset > 0)
                {
                    hDepth = (hDepth > 0) ? hDepth : CustomHingeBlock.holeDepth;
                    hRadius = (hRadius > 0) ? hRadius : CustomHingeBlock.holeRadius;
                    hGap = (hGap > 0) ? hGap : CustomHingeBlock.holeGap;

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
                            if(ConfiguredProduct != null)
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



        //Method to add vertical holes 
        private static void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
        {
            if (ConfiguredProduct != null)
            {
                if (DTYP > 0 && INUP > 0)
                {
                    var holePattern = (Part.Product == PRODUCT.DrawerFront) ? HolePatternDrawerFront.GetDrillingInfo(DTYP, PartName.ToString().ToLower()) : HolePatternHamperDoor.GetDrillingInfo(DTYP);

                    if (holePattern.hasDrillingInfo)
                    {
                        double holeDepth = holePattern.holeDepth;
                        var holeRadius = HDIA / 2;

                        //AddDrillings leftside drilling 
                        double hole1Height = INUP + holePattern.leftDefaultINUP;
                        double hole2Height = INUP + holePattern.leftDefaultINUP + holePattern.gap1;
                        double hole3Height = INUP + holePattern.leftDefaultINUP + holePattern.gap1 + holePattern.gap2;
                        double hole4Height = INUP + holePattern.leftDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3;
                        double hole5Height = INUP + holePattern.leftDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3 + holePattern.gap4;
                        double hole6Height = INUP + holePattern.leftDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3 + holePattern.gap4 + holePattern.gap5;


                        if (LINS > 0)
                        {
                            double leftOffset = LINS;

                            if (holePattern.numHolesLeft > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Left Hole1
                            if (holePattern.numHolesLeft > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Left Hole2
                            if (holePattern.numHolesLeft > 2) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Left Hole3
                            if (holePattern.numHolesLeft > 3) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Left Hole4
                            if (holePattern.numHolesLeft > 4) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Left Hole5
                            if (holePattern.numHolesLeft > 5) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Left Hole6


                        }

                        //AddDrillings rightside drilling 
                        hole1Height = INUP + holePattern.rightDefaultINUP;
                        hole2Height = INUP + holePattern.rightDefaultINUP + holePattern.gap1;
                        hole3Height = INUP + holePattern.rightDefaultINUP + holePattern.gap1 + holePattern.gap2;
                        hole4Height = INUP + holePattern.rightDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3;
                        hole5Height = INUP + holePattern.rightDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3 + holePattern.gap4;
                        hole6Height = INUP + holePattern.rightDefaultINUP + holePattern.gap1 + holePattern.gap2 + holePattern.gap3 + holePattern.gap4 + holePattern.gap5;

                        if (RINS > 0)
                        {
                            double leftOffset = Width - RINS;

                            if (holePattern.numHolesRight > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, holeRadius, holeDepth); }  // Right Hole1
                            if (holePattern.numHolesRight > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, holeRadius, holeDepth); }  //Right Hole2
                            if (holePattern.numHolesRight > 2) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole3Height, leftOffset, holeRadius, holeDepth); }  //Right Hole3
                            if (holePattern.numHolesRight > 3) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole4Height, leftOffset, holeRadius, holeDepth); }  //Right Hole4
                            if (holePattern.numHolesRight > 4) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole5Height, leftOffset, holeRadius, holeDepth); }  //Right Hole5
                            if (holePattern.numHolesRight > 5) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole6Height, leftOffset, holeRadius, holeDepth); }  //Right Hole6


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
                var drillingInfo = HolePatternHamperDoor.GetDrillingInfo(2);
                if (drillingInfo.hasDrillingInfo)
                {
                    double hole1Height = INUP + drillingInfo.leftDefaultINUP;
                    double hole2Height = INUP + drillingInfo.leftDefaultINUP + drillingInfo.gap1;

                    if (Hole3FromTop > 0) { DrillExtraHoles((Hole3FromTop)); }  //hole3FromTop == hole3 from left
                    if (Hole4FromTop > 0) { DrillExtraHoles((Hole3FromTop)); }  //hole4FromTop == hole4 from left
                    if (Hole5FromTop > 0) { DrillExtraHoles((Hole5FromTop)); }  //hole5FromTop == hole5 from left
                    if (Hole6FromTop > 0) { DrillExtraHoles((Hole6FromTop)); }  //hole6FromTop == hole5 from left

                    void DrillExtraHoles(double leftOffset)
                    {
                        if (ConfiguredProduct != null)
                        {
                            if (drillingInfo.numHolesLeft > 0) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole1Height, leftOffset, HDIA / 2, drillingInfo.holeDepth); }  // Left Hole1
                            if (drillingInfo.numHolesLeft > 1) { ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, hole2Height, leftOffset, HDIA / 2, drillingInfo.holeDepth); }  //Left Hole2
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
                double offset = (addToSide == "left") ? SpotHole.inset : (addToSide == "right") ? Width - SpotHole.inset : Width / 2;
                ConfiguredProduct.Features.AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.inup, offset, (SpotHole.radius), SpotHole.depth);
            }
        }

        //End of Methods
    }
}
