
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    class VinylPart
    {
        public int LineNo { get; set; }  //This line no is from Excel Door Order Form. The EDI will generate its own line number which we don't have control over.
        public PRODUCTTYPE ProductType { get; set; } = PRODUCTTYPE.None;
        public PRODUCT Product { get; set; } = PRODUCT.None;
        public int Quantity { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double DfHeight { get; set; }
        public double Thickness { get; set; }
        public PARTNAME PartName { get; set; } = PARTNAME.None;
        public string EdgeLocation { get; set; } = "";
        public string HandleSystem { get; set; } = "";
        //public string HingeType { get; set; } = "";
        public HINGETYPE HingeType { get; set; }
        public string StyleProfile { get; set; } = "";
        public int MultiPieceID { get; set; }
        public int PressedSides { get; set; }
        public int EzeNo { get; set; }
        public string AdditionalInstructions { get; set; } = "";
        public string EdgeMould { get; set; } = "";
        public string FaceProfile { get; set; } = "";
        public string Colour { get; set; } = "";
        public string Finish { get; set; } = "";
        public string PoNumber { get; set; } = "";
        public string RequestedDate { get; set; } = "";
        public string Contact { get; set; } = "";

        //Door data
        public double HingeCupInset { get; set; }
        public double HingeBlockInset { get; set; }
        public double Hole1FromBot { get; set; }
        public double Hole2FromTop { get; set; }
        public double Hole3FromTop { get; set; }
        public double Hole4FromTop { get; set; }
        public double Hole5FromTop { get; set; }
        public double Hole6FromTop { get; set; }
        public int NumHoles { get; set; }
        public double HTOD {  get; set; }  
        public double BifoldHingeCupInset { get; set; }
        public int KickHeight { get; set; }
        public int MidRailHeight { get; set; }
        public bool DoubleMidRail { get; set; }
        public int PanelCount { get; set; }

        //Drawer Data
        public int DTYP1 { get; set; }
        public double INUP1 { get; set; }
        public double LINS {  get; set; }
        public double RINS { get; set; }
        public double HDIA { get; set; }
        public int DTYP2 { get; set; }
        public double INUP2 { get; set; }
        public double HoleDepth { get; set; }
        
        //For panels like Microwave panel, cuouts, roller frame cutous..
        public double CutoutTopBorder { get; set; }
        public double CutoutBottomBorder { get; set; }
        public double CutoutLeftBorder { get; set; }
        public double CutoutRightBorder { get; set; }
        public double CutoutInternalHeight1 { get; set; }
        public bool HasCutout2 { get; set; }
        public double CutoutLeftBorder2 { get; set; }
        public double CutoutRightBorder2 { get; set; }
        public double CutoutBottomBorder2 { get; set; }

        //Return1 info
        public string Return1Edge { get; set; } = "";
        public double Return1Thickness { get; set; }
        public double Return1Width { get; set; }
        //Return2 info
        public string Return2Edge { get; set; } = "";
        public double Return2Thickness { get; set; }
        public double Return2Width { get; set; }

        //Contrasting Edge info
        public string ContrastingEdgeColour { get; set; } = "";
        public string ContrastingEdgeFinish { get; set; } = "";


        //Bar Panel Info
        public int NumberOfPanels { get; set; }
        public bool EvenlySizedProfiles { get; set; } 
        public double Profile1Size { get; set; }
        public double Profile2Size { get; set; }
        public double Profile3Size { get; set; }
        public double Profile4Size { get; set; }
        public double Profile5Size { get; set; }
        public double Profile6Size { get; set; }
        public double Profile7Size { get; set; }
        public double Profile8Size { get; set; }

        public VinylPart()
        { 
            //Empty Object
        }


        public  VinylPart(string[] arrProductVal)
        {
            try
            {
                LineNo = string.IsNullOrEmpty(arrProductVal[0].Trim()) ? 0 : Int32.Parse(arrProductVal[0].Trim());
                ProductType = Workout_ProductType(arrProductVal[1].Trim());
                Product = Workout_Product(arrProductVal[2].Trim());
                Quantity = string.IsNullOrEmpty(arrProductVal[3].Trim()) ? 0 : Int32.Parse(arrProductVal[3].Trim());
                Height = string.IsNullOrEmpty(arrProductVal[4].Trim()) ? 0 : double.Parse(arrProductVal[4].Trim());
                Width = string.IsNullOrEmpty(arrProductVal[5].Trim()) ? 0 : double.Parse(arrProductVal[5].Trim());
                DfHeight = string.IsNullOrEmpty(arrProductVal[6].Trim()) ? 0 : double.Parse(arrProductVal[6].Trim());
                Thickness = string.IsNullOrEmpty(arrProductVal[7].Trim()) ? 0 : double.Parse(arrProductVal[7].Trim());
                PartName = Workout_PartName(arrProductVal[8].Trim());

                EdgeLocation = arrProductVal[9].Trim().ToUpper();
                HandleSystem = string.IsNullOrEmpty(arrProductVal[10].Trim()) ? "None" : arrProductVal[10].Trim();
                //HingeType = arrProductVal[11].Trim().ToLower();
                HingeType = Workout_HingeType(arrProductVal[11].Trim());
                StyleProfile = arrProductVal[12].Trim().ToUpper();
                MultiPieceID = string.IsNullOrEmpty(arrProductVal[13].Trim()) ? 0 : Int32.Parse(arrProductVal[13].Trim());
                PressedSides = string.IsNullOrEmpty(arrProductVal[14].Trim()) ? 0 : Int32.Parse(arrProductVal[14].Trim());
                EzeNo = string.IsNullOrEmpty(arrProductVal[15].Trim()) ? 0 : Int32.Parse(arrProductVal[15].Trim());

                //Set door data
                HingeCupInset = string.IsNullOrEmpty(arrProductVal[17].Trim()) ? 0 : double.Parse(arrProductVal[17].Trim());
                HingeBlockInset = string.IsNullOrEmpty(arrProductVal[18].Trim()) ? 0 : double.Parse(arrProductVal[18].Trim());
                Hole1FromBot = string.IsNullOrEmpty(arrProductVal[19].Trim()) ? 0 : double.Parse(arrProductVal[19].Trim());
                Hole2FromTop = string.IsNullOrEmpty(arrProductVal[20].Trim()) ? 0 : double.Parse(arrProductVal[20].Trim());
                Hole3FromTop = string.IsNullOrEmpty(arrProductVal[21].Trim()) ? 0 : double.Parse(arrProductVal[21].Trim());
                Hole4FromTop = string.IsNullOrEmpty(arrProductVal[22].Trim()) ? 0 : double.Parse(arrProductVal[22].Trim());
                Hole5FromTop = string.IsNullOrEmpty(arrProductVal[23].Trim()) ? 0 : double.Parse(arrProductVal[23].Trim());
                Hole6FromTop = string.IsNullOrEmpty(arrProductVal[24].Trim()) ? 0 : double.Parse(arrProductVal[24].Trim());
                NumHoles = (Hole1FromBot > 0 ? 1 : 0) + (Hole2FromTop > 0 ? 1 : 0) + (Hole3FromTop > 0 ? 1 : 0) + (Hole4FromTop > 0 ? 1 : 0) + (Hole5FromTop > 0 ? 1 : 0) + (Hole6FromTop > 0 ? 1 : 0);
                HTOD = string.IsNullOrEmpty(arrProductVal[25].Trim()) ? 0 : double.Parse(arrProductVal[25].Trim());
                BifoldHingeCupInset = string.IsNullOrEmpty(arrProductVal[26].Trim()) ? 0 : double.Parse(arrProductVal[26].Trim());
                KickHeight = string.IsNullOrEmpty(arrProductVal[27].Trim()) ? 0 : Int32.Parse(arrProductVal[27].Trim());
                MidRailHeight = string.IsNullOrEmpty(arrProductVal[28].Trim()) ? 0 : Int32.Parse(arrProductVal[28].Trim());
                DoubleMidRail = string.Equals(arrProductVal[29].Trim(), "yes", StringComparison.OrdinalIgnoreCase);
                PanelCount = (MidRailHeight > 0) ? 2 : 1;

                //Set Drawer data
                DTYP1 = string.IsNullOrEmpty(arrProductVal[32].Trim()) ? 0 : Int32.Parse(arrProductVal[32]);
                INUP1 = string.IsNullOrEmpty(arrProductVal[33].Trim()) ? 0 : double.Parse(arrProductVal[33]);
                LINS = string.IsNullOrEmpty(arrProductVal[34].Trim()) ? 0 : double.Parse(arrProductVal[34]);
                RINS = string.IsNullOrEmpty(arrProductVal[35].Trim()) ? 0 : double.Parse(arrProductVal[35]);
                HDIA = string.IsNullOrEmpty(arrProductVal[36].Trim()) ? 0 : double.Parse(arrProductVal[36]);
                DTYP2 = string.IsNullOrEmpty(arrProductVal[37].Trim()) ? 0 : Int32.Parse(arrProductVal[37]);
                INUP2 = string.IsNullOrEmpty(arrProductVal[38].Trim()) ? 0 : double.Parse(arrProductVal[38]);
                HoleDepth = string.IsNullOrEmpty(arrProductVal[39].Trim()) ? 0 : double.Parse(arrProductVal[39]);

                AdditionalInstructions = arrProductVal[41].Trim();
                EdgeMould = arrProductVal[42].Trim();
                FaceProfile = arrProductVal[43].Trim();
                if (string.Equals(FaceProfile, "Guildford", StringComparison.OrdinalIgnoreCase)) FaceProfile = "Guilford";

                Colour = arrProductVal[44].Trim();
                Finish = arrProductVal[45].Trim();
                PoNumber = arrProductVal[46].Trim().ToUpper();
                RequestedDate = arrProductVal[47].Trim();

                if (!string.IsNullOrEmpty(RequestedDate) && RequestedDate.Contains('/'))
                {
                    string[] tempArray = RequestedDate.Split('/');
                    if (tempArray[0].Trim().Length == 1) tempArray[0] = "0" + tempArray[0].Trim();
                    if (tempArray[1].Trim().Length == 1) tempArray[1] = "0" + tempArray[1].Trim();
                    if (tempArray[2].Trim().Length == 2) tempArray[2] = "20" + tempArray[2].Trim();
                    RequestedDate = string.Join("/", tempArray);
                }

                Contact = arrProductVal[48].Trim();

                ////Cutout and roller frame data
                CutoutTopBorder = string.IsNullOrEmpty(arrProductVal[49].Trim()) ? 0 : double.Parse(arrProductVal[49].Trim());
                CutoutBottomBorder = string.IsNullOrEmpty(arrProductVal[50].Trim()) ? 0 : double.Parse(arrProductVal[50].Trim());
                CutoutLeftBorder = string.IsNullOrEmpty(arrProductVal[51].Trim()) ? 0 : double.Parse(arrProductVal[51].Trim());
                CutoutRightBorder = string.IsNullOrEmpty(arrProductVal[52].Trim()) ? 0 : double.Parse(arrProductVal[52].Trim());
                CutoutInternalHeight1 = string.IsNullOrEmpty(arrProductVal[53].Trim()) ? 0 : double.Parse(arrProductVal[53].Trim());
                HasCutout2 = CutoutInternalHeight1 != 0;
                CutoutLeftBorder2 = string.IsNullOrEmpty(arrProductVal[54].Trim()) ? 0 : double.Parse(arrProductVal[54].Trim());
                CutoutRightBorder2 = string.IsNullOrEmpty(arrProductVal[55].Trim()) ? 0 : double.Parse(arrProductVal[55].Trim());
                CutoutBottomBorder2 = string.IsNullOrEmpty(arrProductVal[56].Trim()) ? 0 : double.Parse(arrProductVal[56].Trim());

                //Return1 info
                Return1Edge = arrProductVal[57].Trim().ToLower();
                Return1Thickness = string.IsNullOrEmpty(arrProductVal[58].Trim()) ? 0 : double.Parse(arrProductVal[58].Trim());
                Return1Width = string.IsNullOrEmpty(arrProductVal[59].Trim()) ? 0 : double.Parse(arrProductVal[59].Trim());

                //Return2 info
                Return2Edge = arrProductVal[60].Trim().ToLower();
                Return2Thickness = string.IsNullOrEmpty(arrProductVal[61].Trim()) ? 0 : double.Parse(arrProductVal[61].Trim());
                Return2Width = string.IsNullOrEmpty(arrProductVal[62].Trim()) ? 0 : double.Parse(arrProductVal[62].Trim());

                //Contrasting Edge info
                ContrastingEdgeColour = arrProductVal[63].Trim();
                ContrastingEdgeFinish = arrProductVal[64].Trim();

                //Bar Panel Info
                NumberOfPanels = string.IsNullOrEmpty(arrProductVal[65].Trim()) ? 0 : Int32.Parse(arrProductVal[65]);
                EvenlySizedProfiles = string.Equals(arrProductVal[66].Trim(), "yes") ? true : false;
                Profile1Size = string.IsNullOrEmpty(arrProductVal[67].Trim()) ? 0 : double.Parse(arrProductVal[67].Trim());
                Profile2Size = string.IsNullOrEmpty(arrProductVal[68].Trim()) ? 0 : double.Parse(arrProductVal[68].Trim());
                Profile3Size = string.IsNullOrEmpty(arrProductVal[69].Trim()) ? 0 : double.Parse(arrProductVal[69].Trim());
                Profile4Size = string.IsNullOrEmpty(arrProductVal[70].Trim()) ? 0 : double.Parse(arrProductVal[70].Trim());
                Profile5Size = string.IsNullOrEmpty(arrProductVal[71].Trim()) ? 0 : double.Parse(arrProductVal[71].Trim());
                Profile6Size = string.IsNullOrEmpty(arrProductVal[72].Trim()) ? 0 : double.Parse(arrProductVal[72].Trim());
                Profile7Size = string.IsNullOrEmpty(arrProductVal[73].Trim()) ? 0 : double.Parse(arrProductVal[73].Trim());
                Profile8Size = string.IsNullOrEmpty(arrProductVal[74].Trim()) ? 0 : double.Parse(arrProductVal[74].Trim());
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private static HINGETYPE Workout_HingeType(string hingeType)
        {
            try
            {
                if (string.IsNullOrEmpty(hingeType)) return HINGETYPE.None;
                if (string.Equals(hingeType, "blum", StringComparison.OrdinalIgnoreCase)) return HINGETYPE.Blum;
                if (string.Equals(hingeType, "hettich", StringComparison.OrdinalIgnoreCase)) return HINGETYPE.Hettich;
                return HINGETYPE.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in working out Hinge Type of Vinyl part!\n\n{ex.Message}");
                return HINGETYPE.None;
            }
        }

        private static PRODUCTTYPE Workout_ProductType(string productType)
        {
            try
            {
                if (string.IsNullOrEmpty(productType)) return PRODUCTTYPE.None;
                if (string.Equals(productType, "thermo", StringComparison.OrdinalIgnoreCase)) return PRODUCTTYPE.Thermo;
                if (string.Equals(productType, "cut&rout", StringComparison.OrdinalIgnoreCase)) return PRODUCTTYPE.CutAndRout;
                if (string.Equals(productType, "compactlaminate", StringComparison.OrdinalIgnoreCase)) return PRODUCTTYPE.CompactLaminate;
                return PRODUCTTYPE.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in working out Product Type of Vinyl part!\n\n{ex.Message}");
                return PRODUCTTYPE.None;
            }
        }

        private static PRODUCT Workout_Product(string product)
        {
            try
            {   
                if (string.IsNullOrEmpty(product)) return PRODUCT.None; 
                if (string.Equals(product, "door", StringComparison.OrdinalIgnoreCase)) return PRODUCT.Door;
                if (string.Equals(product, "drawers", StringComparison.OrdinalIgnoreCase)) return PRODUCT.DrawerFront;
                if (string.Equals(product, "heatdeflectors", StringComparison.OrdinalIgnoreCase)) return PRODUCT.HeatDeflectors;
                if (string.Equals(product, "barpanel", StringComparison.OrdinalIgnoreCase)) return PRODUCT.BarPanel;
                if (string.Equals(product, "panel", StringComparison.OrdinalIgnoreCase)) return PRODUCT.Panel;
                if (string.Equals(product, "glassframe", StringComparison.OrdinalIgnoreCase)) return PRODUCT.GlassFrame;
                if (string.Equals(product, "cutout", StringComparison.OrdinalIgnoreCase)) return PRODUCT.CutOut;
                if (string.Equals(product, "rollerframe", StringComparison.OrdinalIgnoreCase)) return PRODUCT.RollerFrame;
                if (string.Equals(product, "recessedrail", StringComparison.OrdinalIgnoreCase)) return PRODUCT.RecessedRail;
                if (string.Equals(product, "pantrydoor", StringComparison.OrdinalIgnoreCase)) return PRODUCT.PantryDoor;
                return PRODUCT.None;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error in working out Product name of Vinyl part!\n\n{ex.Message}");
                return PRODUCT.None;
            }
        }

        private static PARTNAME Workout_PartName(string partName)
        {
            try
            {
                if (string.IsNullOrEmpty(partName)) return PARTNAME.None;
                if (string.Equals(partName, "c shapped", StringComparison.OrdinalIgnoreCase) || string.Equals(partName, "l shapped", StringComparison.OrdinalIgnoreCase))
                {
                    _ = partName.Replace("pp", "p");
                }

                if (string.Equals(partName, "pair", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Pair;
                if (string.Equals(partName, "left", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left;
                if (string.Equals(partName, "right", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right;
                if (string.Equals(partName, "top", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Top;
                if (string.Equals(partName, "bottom", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Bottom;

                if (string.Equals(partName, "left bifold", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left_Bifold;
                if (string.Equals(partName, "right bifold", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right_Bifold;
                if (string.Equals(partName, "top bifold", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Top_Bifold;
                if (string.Equals(partName, "bottom bifold", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Bottom_Bifold;

                if (string.Equals(partName, "left bifold leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right_Leaf;
                if (string.Equals(partName, "right bifold leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left_Leaf;
                if (string.Equals(partName, "top bifold leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Bottom_Leaf;
                if (string.Equals(partName, "bottom bifold leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Top_Leaf;

                if (string.Equals(partName, "770 left", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left_770;
                if (string.Equals(partName, "770 right", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right_770;
                if (string.Equals(partName, "770 left leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right_Leaf_770;
                if (string.Equals(partName, "770 right leaf", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left_Leaf_770;

                if (string.Equals(partName, "left blind panel", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Left_Blind_Panel;
                if (string.Equals(partName, "right blind panel", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Right_Blind_Panel;

                if (string.Equals(partName, "angled", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Angled;
                if (string.Equals(partName, "straight", StringComparison.OrdinalIgnoreCase)) return PARTNAME.Straight;

                if (string.Equals(partName, "c shaped", StringComparison.OrdinalIgnoreCase)) return PARTNAME.C_Shaped;
                if (string.Equals(partName, "l shaped", StringComparison.OrdinalIgnoreCase)) return PARTNAME.L_Shaped;

                return PARTNAME.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in working out Part Name of Vinyl part!\n\n{ex.Message}");
                return PARTNAME.None;
            }
        }
    }
}


// Csv header   - this might get updated, so refer the csv saved in the folder

//   0:Line No,
//   1:ConfiguredPiece Type,
//   2:ConfiguredPiece,
//   3:Qty,
//   4:Height,
//   5:Width,
//   6:DF Height,
//   7:Thickness,
//   8:LorR,
//   9:Edge Location,
//  10:Handle System,
//  11:Hinge Type,
//  12:Vinyl Style (Profile),
//  13:Multi Piece ID,
//  14:Pressed Side,
//  15:Eze No,
//  16:spare,

//  17:Hinge Cup Inset,
//  18:Hinge Block Inset,
//  19:Hole1 From Bot,
//  20:Hole2 From Top,
//  21:Hole3 From Top,
//  22:Hole4 From Top,
//  23:Hole5 From Top,
//  24:Hole6 From Top,
//  25:HTOD,
//  26:Bifold Hingecup inset,

//  27:Kick Height,
//  28:Mid Rail Height,
//  29:Double Mid Rail

//  30:spare,
//  31:spare,

//  32:DTYP,
//  33:Bot Inset (INUP),
//  34:Left Inset (LINS),
//  35:Right Inset (RINS),
//  36:Drawer HDIA,
//  37:2nd DTYP,
//  38:2nd INUP From Bot,

//  39:spare,
//  40:spare,           

//  41:Additional Instructions,
//  42:Edge Mould,
//  43:Face Profile,
//  44:Color,
//  45:Finish,

//  46:PO Number,
//  47:Requested  Date,
//  48:Contact     

//  49:Cutout Top Border,
//  50:Cutout Bottom Border,
//  51:Cutout Left Border,
//  52:Cutout Right Border,
//  53:Cutout Internal Height1
//  54:2nd Cutout Left Border,
//  55:2nd Cutout right Border,
//  56:2nd Cutout Bottom Border

//  57:Return1Edge
//  58:Return1Thickness
//  59:Return1Width
//  60:Return2Edge
//  61:Return2Thickness
//  62:Return2Width

//  63:Contrasting Edge Colour
//  64:ContrastingEdgeFinish

//  65:NumbeOfPanels
//  66:EvenlySizedProfiles
//  67:Profile1Size
//  68:Profile2Size
//  69:Profile3Size
//  70:Profile4Size
//  71:Profile5Size
//  72:Profile6Size
//  73:Profile7Size
//  74:Profile8Size
