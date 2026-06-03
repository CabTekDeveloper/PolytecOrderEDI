using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class CustomDrilling
    {
        // ── Configured target (one will be non-null) ──────────────────────────
        private static Product? ConfiguredProduct { get; set; }
        private static GenericPiece? ConfiguredPiece { get; set; }

        // ── Source parts ──────────────────────────────────────────────────────
        private static VinylPart? vinylPart { get; set; } = null;
        private static CabinetPart? cabPart { get; set; } = null;

        // ── Build parameters ──────────────────────────────────────────────────
        private static BuildParameter_Door? DoorParams { get; set; } = null;
        private static BuildParameter_DrawerFront? DrawerFrontParams { get; set; } = null;
        private static BuildParameter_Handle? HandleParams { get; set; } = null;

        // ── Piece dimensions & identity ───────────────────────────────────────
        private static PRODUCTTYPE ProductType { get; set; } = PRODUCTTYPE.None;
        private static PRODUCT ProductName { get; set; } = PRODUCT.None;
        private static PARTNAME PartName { get; set; } = PARTNAME.None;
        private static double Height { get; set; }
        private static double Width { get; set; }
        private static double Thickness { get; set; }

        // ── Hinge properties ──────────────────────────────────────────────────
        private static HINGETYPE HingeType { get; set; } = HINGETYPE.None;
        private static double HingeCupInset { get; set; }
        private static double HingeBlockInset { get; set; }
        private static double HingeBlockHDIA { get; set; }
        private static double HingeBlockHoleDepth { get; set; }
        private static double BifoldHingeCupInset { get; set; }
        private static double HTOD { get; set; }

        // ── Hinge hole positions ──────────────────────────────────────────────
        private static double Hole1FromBot { get; set; }
        private static double Hole2FromTop { get; set; }
        private static double Hole3FromTop { get; set; }
        private static double Hole4FromTop { get; set; }
        private static double Hole5FromTop { get; set; }
        private static double Hole6FromTop { get; set; }
        private static int NumHoles { get; set; }

        // ── Drawer drilling properties ────────────────────────────────────────
        private static double LINS { get; set; }
        private static double RINS { get; set; }
        private static int DTYP1 { get; set; }
        private static int DTYP2 { get; set; }
        private static double INUP1 { get; set; }
        private static double INUP2 { get; set; }
        private static double DrawerHDIA { get; set; }
        private static double DrawerHoleDepth { get; set; }

        // ── Custom hole 1 ─────────────────────────────────────────────────────
        private static double CustomHole1LeftInset { get; set; }
        private static double CustomHole1TopInset { get; set; }
        private static double CustomHole1HDIA { get; set; }
        private static double CustomHole1Depth { get; set; }
        private static APPLYTARGET CustomHole1ApplyTarget { get; set; } = APPLYTARGET.None;
        private static bool HasCustomHole1Drilling { get; set; }

        // ── Target-agnostic feature accessors ────────────────────────────────
        private static void AddHoleFromBottomLeft(ApplyTarget target, double fromBottom, double fromLeft, double radius, double depth)
        {
            ConfiguredProduct?.Features.AddHoleFromBottomLeft(target, fromBottom, fromLeft, radius, depth);
            ConfiguredPiece?.Features.AddHoleFromBottomLeft(target, fromBottom, fromLeft, radius, depth);
        }

        private static void AddHoleFromTopLeft(ApplyTarget target, double fromTop, double fromLeft, double radius, double depth)
        {
            ConfiguredProduct?.Features.AddHoleFromTopLeft(target, fromTop, fromLeft, radius, depth);
            ConfiguredPiece?.Features.AddHoleFromTopLeft(target, fromTop, fromLeft, radius, depth);
        }

        private static void AddFeature(Feature feature)
        {
            ConfiguredProduct?.Features.Add(feature);
            ConfiguredPiece?.Features.Add(feature);
        }

        private static bool HasTarget => ConfiguredProduct != null || ConfiguredPiece != null;

        // ─────────────────────────────────────────────────────────────────────
        // SetDrillingProperties
        // ─────────────────────────────────────────────────────────────────────
        private static void SetDrillingProperties()
        {
            // Reset build parameters — prevents stale values leaking when the same
            DoorParams          = null;
            DrawerFrontParams   = null;
            HandleParams        = null;

            ProductType = vinylPart?.ProductType    ?? cabPart?.ProductType ?? PRODUCTTYPE.None;
            ProductName = vinylPart?.Product        ?? cabPart?.Product     ?? PRODUCT.None;
            PartName    = vinylPart?.PartName       ?? cabPart?.PartName    ?? PARTNAME.None;
            Height      = vinylPart?.Height         ?? cabPart?.Height      ?? 0;
            Width       = vinylPart?.Width          ?? cabPart?.Width       ?? 0;
            Thickness   = vinylPart?.Thickness      ?? cabPart?.Thickness   ?? 0;

            if (cabPart != null)
            {
                DoorParams          = new(cabPart);
                DrawerFrontParams   = new(cabPart);
                HandleParams        = new(cabPart);
            }

            HingeType           = vinylPart?.HingeType              ?? DoorParams?.HingeType            ?? HINGETYPE.None;
            HingeCupInset       = vinylPart?.HingeCupInset          ?? DoorParams?.HingeCupInset        ?? 0;
            HingeBlockInset     = vinylPart?.HingeBlockInset        ?? DoorParams?.HingeBlockInset      ?? 0;
            HingeBlockHDIA      = vinylPart?.HingeBlockHDIA         ?? 0;
            HingeBlockHoleDepth = vinylPart?.HingeBlockHoleDepth    ?? 0;
            BifoldHingeCupInset = vinylPart?.BifoldHingeCupInset    ?? DoorParams?.BifoldHingeCupInset  ?? 0;
            HTOD                = vinylPart?.HTOD                   ?? DoorParams?.HTOD                 ?? 0;

            Hole1FromBot    = vinylPart?.Hole1FromBot   ?? DoorParams?.Hole1FromBot ?? 0;
            Hole2FromTop    = vinylPart?.Hole2FromTop   ?? DoorParams?.Hole2FromTop ?? 0;
            Hole3FromTop    = vinylPart?.Hole3FromTop   ?? DoorParams?.Hole3FromTop ?? 0;
            Hole4FromTop    = vinylPart?.Hole4FromTop   ?? DoorParams?.Hole4FromTop ?? 0;
            Hole5FromTop    = vinylPart?.Hole5FromTop   ?? DoorParams?.Hole5FromTop ?? 0;
            Hole6FromTop    = vinylPart?.Hole6FromTop   ?? DoorParams?.Hole6FromTop ?? 0;
            NumHoles        = vinylPart?.NumHoles       ?? DoorParams?.NumHoles     ?? 0;

            // LINS/RINS are swapped intentionally: the Back view mirrors the Front view.
            LINS                = vinylPart?.RINS               ?? DrawerFrontParams?.RINS  ?? 0;
            RINS                = vinylPart?.LINS               ?? DrawerFrontParams?.LINS  ?? 0;
            DTYP1               = vinylPart?.DTYP1              ?? DrawerFrontParams?.DTYP1 ?? 0;
            DTYP2               = vinylPart?.DTYP2              ?? DrawerFrontParams?.DTYP2 ?? 0;
            INUP1               = vinylPart?.INUP1              ?? DrawerFrontParams?.INUP1 ?? 0;
            INUP2               = vinylPart?.INUP2              ?? DrawerFrontParams?.INUP2 ?? 0;
            DrawerHDIA          = vinylPart?.DrawerHDIA         ?? DrawerFrontParams?.HDIA  ?? 0;
            DrawerHoleDepth     = vinylPart?.DrawerHoleDepth    ?? 0;

            CustomHole1LeftInset    = vinylPart?.CustomHole1LeftInset   ?? 0;
            CustomHole1TopInset     = vinylPart?.CustomHole1TopInset    ?? 0;
            CustomHole1HDIA         = vinylPart?.CustomHole1HDIA        ?? 0;
            CustomHole1Depth        = vinylPart?.CustomHole1Depth       ?? 0;
            CustomHole1ApplyTarget  = vinylPart?.CustomHole1ApplyTarget ?? APPLYTARGET.None;
            HasCustomHole1Drilling  = CustomHole1LeftInset > 0
                                    && CustomHole1TopInset > 0
                                    && CustomHole1HDIA > 0
                                    && CustomHole1Depth > 0
                                    && CustomHole1ApplyTarget != APPLYTARGET.None;
        }

        // ─────────────────────────────────────────────────────────────────────
        // Public entry points
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Entry for Type Product</summary>
        public static void AddDrillings(Product configuredProduct, VinylPart? vinyl_part = null, CabinetPart? cabinet_part = null)
        {
            if (vinyl_part == null && cabinet_part == null) return;

            ConfiguredProduct = configuredProduct;
            ConfiguredPiece = null;
            InitAndDispatch(vinyl_part, cabinet_part, isGenericPiece: false);
        }

        /// <summary>Entry for Type GenericPiece</summary>
        public static void AddDrillings(GenericPiece configuredPiece, VinylPart? vinyl_part = null, CabinetPart? cabinet_part = null)
        {
            if (vinyl_part == null && cabinet_part == null) return;

            ConfiguredProduct = null;
            ConfiguredPiece = configuredPiece;
            InitAndDispatch(vinyl_part, cabinet_part, isGenericPiece: true);
        }

        // ─────────────────────────────────────────────────────────────────────
        // Initialize and dispatch
        // ─────────────────────────────────────────────────────────────────────
        private static void InitAndDispatch(VinylPart? vinyl_part, CabinetPart? cabinet_part, bool isGenericPiece)
        {
            vinylPart = vinyl_part;
            cabPart = cabinet_part;
            SetDrillingProperties();

            if (HasCustomHole1Drilling)
                AddCustomHole1FromTopLeft();

            if (isGenericPiece)
            {
                // GenericPiece
                DispatchGenericPiece();
            }
            else
            {
                // Product
                DispatchProduct();
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // GenericPiece dispatch 
        // ─────────────────────────────────────────────────────────────────────
        private static void DispatchGenericPiece()
        {
            if (HingeType == HINGETYPE.BlumLdf)
            {
                HingeType = HINGETYPE.Blum;
                AddHinges("right", HingeCupInset);
            }
            else if (HingeType == HINGETYPE.BlumRdf)
            {
                HingeType = HINGETYPE.Blum;
                AddHinges("left", HingeCupInset);
            }
            else
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Product dispatch
        // ─────────────────────────────────────────────────────────────────────
        private static void DispatchProduct()
        {
            // Single drawer fronts are treated as doors
            if (ProductName == PRODUCT.DrawerFront)
            {
                if (HingeType == HINGETYPE.BlumLdf)
                {
                    HingeType = HINGETYPE.Blum;
                    AddHinges("right", HingeCupInset);
                }
                else if (HingeType == HINGETYPE.BlumRdf)
                {
                    HingeType = HINGETYPE.Blum;
                    AddHinges("left", HingeCupInset);
                }
                else
                {
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                    AddSingleSpotHole(addToSide: PartName.ToString().ToLower());
                }
                return;
            }

            if (ProductName == PRODUCT.Panel && DTYP1 == 1002)
            {
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
            }

            // Drawer drillings on Pantry doors and Doors (requested by Matt 10-11-2025)
            if (PartName == PARTNAME.None && (ProductName == PRODUCT.PantryDoor || ProductName == PRODUCT.Door))
            {
                var tempProductName = ProductName;
                ProductName = PRODUCT.DrawerFront;
                AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                AddLeftAndRightVerticalHoles(DTYP2, INUP2);
                ProductName = tempProductName;
            }

            switch (PartName)
            {
                case PARTNAME.None:
                    break;

                case PARTNAME.Left:
                    if (HingeType == HINGETYPE.Blum11) AddHinges_Blum11("right", HingeCupInset);
                    else if (HingeType == HINGETYPE.Hole35mm) AddHole35mm("right", HingeCupInset);
                    else AddHinges("right", HingeCupInset);

                    if (HandleParams?.HasHandle == true) AddHandleOnFront("right");
                    if (ProductType == PRODUCTTYPE.CompactLaminate) AddHingeBlocks("right", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    break;

                case PARTNAME.Right:
                    if (HingeType == HINGETYPE.Blum11) AddHinges_Blum11("left", HingeCupInset);
                    else if (HingeType == HINGETYPE.Hole35mm) AddHole35mm("left", HingeCupInset);
                    else AddHinges("left", HingeCupInset);

                    if (HandleParams?.HasHandle == true) AddHandleOnFront("left");
                    if (ProductType == PRODUCTTYPE.CompactLaminate) AddHingeBlocks("left", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    break;

                case PARTNAME.Top:
                    if (HingeType == HINGETYPE.Blum11) AddHinges_Blum11("top", HingeCupInset);
                    else AddHinges("top", HingeCupInset);

                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Bottom:
                    if (HingeType == HINGETYPE.Blum11) AddHinges_Blum11("bottom", HingeCupInset);
                    else AddHinges("bottom", HingeCupInset);

                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Left_Bifold:
                    AddHinges("right", HingeCupInset);
                    AddHinges("left", BifoldHingeCupInset);

                    if (cabPart != null && !cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                        if (HandleParams?.HasHandle == true) AddHandleOnFront("right");
                    break;

                case PARTNAME.Right_Bifold:
                    AddHinges("left", HingeCupInset);
                    AddHinges("right", BifoldHingeCupInset);

                    if (cabPart != null && !cabPart.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase))
                        if (HandleParams?.HasHandle == true) AddHandleOnFront("left");
                    break;

                case PARTNAME.Top_Bifold:
                    AddHinges("top", HingeCupInset);
                    AddHingeBlocks("bottom", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);

                    if (NumHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor(INUP1);
                    break;

                case PARTNAME.Bottom_Bifold:
                    AddHinges("bottom", HingeCupInset);
                    AddHingeBlocks("top", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);

                    if (NumHoles > 2) AddExtraHingeBlockHolesToHamperBifoldDoor(INUP1);
                    break;

                case PARTNAME.Left_Leaf:
                    AddHingeBlocks("left", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);

                    if (cabPart?.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase) == true)
                        if (HandleParams?.HasHandle == true) AddHandleOnFront("left");
                    break;

                case PARTNAME.Right_Leaf:
                    AddHingeBlocks("right", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);

                    if (cabPart?.CabinetName.Contains("Corner", StringComparison.OrdinalIgnoreCase) == true)
                        if (HandleParams?.HasHandle == true) AddHandleOnFront("right");
                    break;

                case PARTNAME.Top_Leaf:
                    AddHinges("bottom", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Bottom_Leaf:
                    AddHinges("top", HingeCupInset);
                    AddLeftAndRightVerticalHoles(DTYP1, INUP1);
                    break;

                case PARTNAME.Left_Blind_Panel:
                    AddHingeBlocks("left", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    break;

                case PARTNAME.Right_Blind_Panel:
                    AddHingeBlocks("right", HingeBlockInset, HingeBlockHoleDepth, HingeBlockHDIA);
                    break;

                case PARTNAME.Left_770:
                    AddHinges("right", HingeCupInset);
                    AddHingeBlocks("left", HingeBlockInset, hDepth: 1);
                    if (HandleParams?.HasHandle == true) AddHandleOnFront("right");
                    break;

                case PARTNAME.Right_770:
                    AddHinges("left", HingeCupInset);
                    AddHingeBlocks("right", HingeBlockInset, hDepth: 1);
                    if (HandleParams?.HasHandle == true) AddHandleOnFront("left");
                    break;

                case PARTNAME.Left_Leaf_770:
                    AddHingeBlocks("left", HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("right", HingeCupInset);
                    break;

                case PARTNAME.Right_Leaf_770:
                    AddHingeBlocks("right", HingeBlockInset, hDepth: 1);
                    Add35mmCupHolesTo770StyleLeadDoor("left", HingeCupInset);
                    break;

                default:
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // Shared drilling methods
        // ─────────────────────────────────────────────────────────────────────

        /// <summary>Adds vertical drawer holes on both the left and right sides of a panel.</summary>
        public static void AddLeftAndRightVerticalHoles(int DTYP, double INUP)
        {
            if (!HasTarget || DTYP <= 0 || INUP <= 0) return;

            var holePattern = (ProductName == PRODUCT.DrawerFront)
                ? HolePatternDrawerFront.GetDrillingInfo(DTYP, PartName.ToString().ToLower())
                : HolePatternDoorAndPanel.GetDrillingInfo(DTYP);

            if (!holePattern.HasDrillingInfo) return;

            double holeDepth = DrawerHoleDepth > 0 ? DrawerHoleDepth
                              : ProductType == PRODUCTTYPE.CompactLaminate ? CompactDrawerHoleDepth.HoleDepth
                              : holePattern.HoleDepth;
            double holeRadius = DrawerHDIA / 2;

            // Left-side heights
            double lh1 = INUP + holePattern.LeftDefaultINUP;
            double lh2 = lh1 + holePattern.Gap1;
            double lh3 = lh2 + holePattern.Gap2;
            double lh4 = lh3 + holePattern.Gap3;
            double lh5 = lh4 + holePattern.Gap4;
            double lh6 = lh5 + holePattern.Gap5;

            if (LINS > 0)
            {
                double leftOffset = LINS;
                if (holePattern.NumHolesLeft > 0) AddHoleFromBottomLeft(ApplyTarget.Back, lh1, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesLeft > 1) AddHoleFromBottomLeft(ApplyTarget.Back, lh2, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesLeft > 2) AddHoleFromBottomLeft(ApplyTarget.Back, lh3, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesLeft > 3) AddHoleFromBottomLeft(ApplyTarget.Back, lh4, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesLeft > 4) AddHoleFromBottomLeft(ApplyTarget.Back, lh5, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesLeft > 5) AddHoleFromBottomLeft(ApplyTarget.Back, lh6, leftOffset, holeRadius, holeDepth);
            }

            // Right-side heights
            double rh1 = INUP + holePattern.RightDefaultINUP;
            double rh2 = rh1 + holePattern.Gap1;
            double rh3 = rh2 + holePattern.Gap2;
            double rh4 = rh3 + holePattern.Gap3;
            double rh5 = rh4 + holePattern.Gap4;
            double rh6 = rh5 + holePattern.Gap5;

            if (RINS > 0)
            {
                double leftOffset = Width - RINS;
                if (holePattern.NumHolesRight > 0) AddHoleFromBottomLeft(ApplyTarget.Back, rh1, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesRight > 1) AddHoleFromBottomLeft(ApplyTarget.Back, rh2, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesRight > 2) AddHoleFromBottomLeft(ApplyTarget.Back, rh3, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesRight > 3) AddHoleFromBottomLeft(ApplyTarget.Back, rh4, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesRight > 4) AddHoleFromBottomLeft(ApplyTarget.Back, rh5, leftOffset, holeRadius, holeDepth);
                if (holePattern.NumHolesRight > 5) AddHoleFromBottomLeft(ApplyTarget.Back, rh6, leftOffset, holeRadius, holeDepth);
            }
        }

        /// <summary>Adds a single spot hole, positioned by side.</summary>
        public static void AddSingleSpotHole(string addToSide = "")
        {
            if (!HasTarget) return;
            double offset = addToSide == "left" ? SpotHole.Inset
                          : addToSide == "right" ? Width - SpotHole.Inset
                          : Width / 2;
            AddHoleFromBottomLeft(ApplyTarget.Back, SpotHole.Inup, offset, SpotHole.Radius, SpotHole.Depth);
        }

        private static void AddCustomHole1FromTopLeft()
        {
            if (!HasTarget) return;
            var applyTarget = CustomHole1ApplyTarget == APPLYTARGET.Front ? ApplyTarget.Front : ApplyTarget.Back;
            var leftInset = CustomHole1ApplyTarget == APPLYTARGET.Front ? CustomHole1LeftInset : Width - CustomHole1LeftInset;
            AddHoleFromTopLeft(applyTarget, CustomHole1TopInset, leftInset, CustomHole1HDIA / 2, CustomHole1Depth);
        }

        /// <summary>Adds Blum or Hettich hinge cup holes to a door from the back.</summary>
        private static void AddHinges(string addToSide, double offset)
        {
            if (!HasTarget || NumHoles <= 0 || offset <= 0) return;
            if (HingeType != HINGETYPE.Blum && HingeType != HINGETYPE.Hettich) return;

            var Htype = HingeType == HINGETYPE.Blum ? BorgEdi.Enums.HingeType.Blum : BorgEdi.Enums.HingeType.Hettich;

            if (addToSide == "left" || addToSide == "right")
            {
                var axis = addToSide == "left" ? PrimaryAxisReference.Left : PrimaryAxisReference.Right;
                if (Hole1FromBot > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Bottom, Hole1FromBot));
                if (Hole2FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Top, Hole2FromTop));
                if (Hole3FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Top, Hole3FromTop));
                if (Hole4FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Top, Hole4FromTop));
                if (Hole5FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Top, Hole5FromTop));
                if (Hole6FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Top, Hole6FromTop));
            }
            else if (addToSide == "top" || addToSide == "bottom")
            {
                var axis = addToSide == "top" ? PrimaryAxisReference.Top : PrimaryAxisReference.Bottom;
                if (Hole1FromBot > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Right, Hole1FromBot));
                if (Hole2FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Left, Hole2FromTop));
                if (Hole3FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Left, Hole3FromTop));
                if (Hole4FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Left, Hole4FromTop));
                if (Hole5FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Left, Hole5FromTop));
                if (Hole6FromTop > 0) AddFeature(new Hinge(Htype, axis, offset, AdjacentAxisReference.Left, Hole6FromTop));
            }
        }

        /// <summary>Adds Blum 110° (Blum11) hinge — cup hole + two lug holes — from the back.</summary>
        private static void AddHinges_Blum11(string addToSide, double offset)
        {
            if (!HasTarget || NumHoles <= 0 || offset <= 0 || HingeType != HINGETYPE.Blum11) return;

            double cupDepth = Blum11.CupHoleDepth;
            double cupHdia = Blum11.CupHoleHdia;
            double lugDepth = Blum11.LugHoleDepth;
            double lugHdia = Blum11.LugHoleHdia;
            double lugGap = Blum11.LugHolesGap;
            double cupLugGap = Blum11.CupHoleAndLugHoleGap;

            if (addToSide == "left" || addToSide == "right")
            {
                void DrillLeftRight(double posFromBottom)
                {
                    double cupL = addToSide == "left" ? offset : Width - offset;
                    double lugL = addToSide == "left" ? cupL + cupLugGap : cupL - cupLugGap;
                    double lug1B = posFromBottom - lugGap / 2;
                    double lug2B = posFromBottom + lugGap / 2;
                    AddHoleFromBottomLeft(ApplyTarget.Back, posFromBottom, cupL, cupHdia / 2, cupDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, lug1B, lugL, lugHdia / 2, lugDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, lug2B, lugL, lugHdia / 2, lugDepth);
                }

                if (Hole1FromBot > 0) DrillLeftRight(Hole1FromBot);
                if (Hole2FromTop > 0) DrillLeftRight(Height - Hole2FromTop);
                if (Hole3FromTop > 0) DrillLeftRight(Height - Hole3FromTop);
                if (Hole4FromTop > 0) DrillLeftRight(Height - Hole4FromTop);
                if (Hole5FromTop > 0) DrillLeftRight(Height - Hole5FromTop);
                if (Hole6FromTop > 0) DrillLeftRight(Height - Hole6FromTop);
            }
            else if (addToSide == "top" || addToSide == "bottom")
            {
                void DrillTopBottom(double posFromLeft)
                {
                    double cupB = addToSide == "top" ? Height - offset : offset;
                    double lugB = addToSide == "top" ? cupB - cupLugGap : cupB + cupLugGap;
                    double lug1L = posFromLeft - lugGap / 2;
                    double lug2L = posFromLeft + lugGap / 2;
                    AddHoleFromBottomLeft(ApplyTarget.Back, cupB, posFromLeft, cupHdia / 2, cupDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, lugB, lug1L, lugHdia / 2, lugDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, lugB, lug2L, lugHdia / 2, lugDepth);
                }

                if (Hole1FromBot > 0) DrillTopBottom(Width - Hole1FromBot);
                if (Hole2FromTop > 0) DrillTopBottom(Hole2FromTop);
                if (Hole3FromTop > 0) DrillTopBottom(Hole3FromTop);
                if (Hole4FromTop > 0) DrillTopBottom(Hole4FromTop);
                if (Hole5FromTop > 0) DrillTopBottom(Hole5FromTop);
                if (Hole6FromTop > 0) DrillTopBottom(Hole6FromTop);
            }
        }

        /// <summary>Adds a plain 35 mm cup hole (no lug holes) — 09-04-2026.</summary>
        private static void AddHole35mm(string addToSide, double offset)
        {
            if (!HasTarget || NumHoles <= 0 || offset <= 0) return;

            const double radius = 17.5;
            const double depth = 13;
            double leftOffset = addToSide == "left" ? offset
                                : addToSide == "right" ? Width - offset
                                : 0;

            if (Hole1FromBot > 0) AddHoleFromBottomLeft(ApplyTarget.Back, Hole1FromBot, leftOffset, radius, depth);
            if (Hole2FromTop > 0) AddHoleFromTopLeft(ApplyTarget.Back, Hole2FromTop, leftOffset, radius, depth);
            if (Hole3FromTop > 0) AddHoleFromTopLeft(ApplyTarget.Back, Hole3FromTop, leftOffset, radius, depth);
            if (Hole4FromTop > 0) AddHoleFromTopLeft(ApplyTarget.Back, Hole4FromTop, leftOffset, radius, depth);
            if (Hole5FromTop > 0) AddHoleFromTopLeft(ApplyTarget.Back, Hole5FromTop, leftOffset, radius, depth);
            if (Hole6FromTop > 0) AddHoleFromTopLeft(ApplyTarget.Back, Hole6FromTop, leftOffset, radius, depth);
        }

        /// <summary>Adds hinge-block hole pairs (two holes per hinge position).</summary>
        private static void AddHingeBlocks(string addToSide, double offset, double hDepth = 0, double hdia = 0, double hGap = 0)
        {
            if (!HasTarget || NumHoles <= 0 || offset <= 0) return;

            hDepth = hDepth > 0 ? hDepth : CustomHingeBlock.HoleDepth;
            double hRadius = hdia > 0 ? hdia / 2 : CustomHingeBlock.HoleRadius;
            hGap = hGap > 0 ? hGap : CustomHingeBlock.HoleGap;

            if (addToSide == "left" || addToSide == "right")
            {
                double leftOffset = addToSide == "left" ? offset : Width - offset;

                void DrillV(double posFromBottom)
                {
                    AddHoleFromBottomLeft(ApplyTarget.Back, Math.Round(posFromBottom - hGap / 2, 2), leftOffset, hRadius, hDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, Math.Round(posFromBottom + hGap / 2, 2), leftOffset, hRadius, hDepth);
                }

                if (Hole1FromBot > 0) DrillV(Hole1FromBot);
                if (Hole2FromTop > 0) DrillV(Height - Hole2FromTop);
                if (Hole3FromTop > 0) DrillV(Height - Hole3FromTop);
                if (Hole4FromTop > 0) DrillV(Height - Hole4FromTop);
                if (Hole5FromTop > 0) DrillV(Height - Hole5FromTop);
                if (Hole6FromTop > 0) DrillV(Height - Hole6FromTop);
            }
            else if (addToSide == "top" || addToSide == "bottom")
            {
                double bottomOffset = addToSide == "top" ? Height - offset : offset;

                void DrillH(double posFromLeft)
                {
                    AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, posFromLeft - hGap / 2, hRadius, hDepth);
                    AddHoleFromBottomLeft(ApplyTarget.Back, bottomOffset, posFromLeft + hGap / 2, hRadius, hDepth);
                }

                if (Hole1FromBot > 0) DrillH(Width - Hole1FromBot);
                if (Hole2FromTop > 0) DrillH(Hole2FromTop);
                if (Hole3FromTop > 0) DrillH(Hole3FromTop);
                if (Hole4FromTop > 0) DrillH(Hole4FromTop);
                if (Hole5FromTop > 0) DrillH(Hole5FromTop);
                if (Hole6FromTop > 0) DrillH(Hole6FromTop);
            }
        }

        /// <summary>Adds handle through-holes on the front face.</summary>
        private static void AddHandleOnFront(string addToSide = "")
        {
            if (!HasTarget || HandleParams == null) return;

            double hDepth = Thickness;
            double hRadius = HandleParams.HoleDiameter / 2;
            double hGap = HandleParams.HoleGap;
            double sideInset = HandleParams.SideInset;
            bool isVerticalHandle = HandleParams.IsHandleVertical;

            double hole1TopInset = HandleParams.Hole1TopInset;
            double hole1LeftInset = (addToSide == "left" || addToSide == "") ? sideInset : Width - sideInset;

            double hole2TopInset = HandleParams.Hole2TopInset;
            double hole2LeftInset = isVerticalHandle
                ? hole1LeftInset
                : addToSide == "left" || addToSide == "" ? hole1LeftInset + hGap : hole1LeftInset - hGap;

            if (hole1TopInset > 0 && hole1LeftInset > 0) AddHoleFromTopLeft(ApplyTarget.Front, hole1TopInset, hole1LeftInset, hRadius, hDepth);
            if (hole2TopInset > 0 && hole2LeftInset > 0) AddHoleFromTopLeft(ApplyTarget.Front, hole2TopInset, hole2LeftInset, hRadius, hDepth);
        }

        /// <summary>Adds extra hinge-block holes to hamper bifold doors to match the leaf hinge count.</summary>
        private static void AddExtraHingeBlockHolesToHamperBifoldDoor(double INUP)
        {
            if (!HasTarget || INUP <= 0) return;

            var info = HolePatternDoorAndPanel.GetDrillingInfo(2);
            if (!info.HasDrillingInfo) return;

            double h1 = INUP + info.LeftDefaultINUP;
            double h2 = h1 + info.Gap1;

            void DrillExtra(double leftOffset)
            {
                if (info.NumHolesLeft > 0) AddHoleFromBottomLeft(ApplyTarget.Back, h1, leftOffset, DrawerHDIA / 2, info.HoleDepth);
                if (info.NumHolesLeft > 1) AddHoleFromBottomLeft(ApplyTarget.Back, h2, leftOffset, DrawerHDIA / 2, info.HoleDepth);
            }

            if (Hole3FromTop > 0) DrillExtra(Hole3FromTop);
            if (Hole4FromTop > 0) DrillExtra(Hole4FromTop);   
            if (Hole5FromTop > 0) DrillExtra(Hole5FromTop);
            if (Hole6FromTop > 0) DrillExtra(Hole6FromTop);
        }

        /// <summary>Adds 35 mm cup holes to 770-style leaf doors.</summary>
        private static void Add35mmCupHolesTo770StyleLeadDoor(string addToSide, double offset)
        {
            if (!HasTarget || offset <= 0) return;

            double gap = DoorStyle770.CupHoleGap;
            double radius = DoorStyle770.CupHoleRadius;
            double depth = DoorStyle770.CupHoleDepth;

            double cup1FromTop = gap - HTOD;
            double cup2FromTop = cup1FromTop + gap;

            double leftOffset = addToSide == "right" ? Width - offset : offset;

            AddHoleFromTopLeft(ApplyTarget.Back, cup1FromTop, leftOffset, radius, depth);
            AddHoleFromTopLeft(ApplyTarget.Back, cup2FromTop, leftOffset, radius, depth);
        }
    }
}
