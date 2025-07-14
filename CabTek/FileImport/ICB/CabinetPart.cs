
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using BorgEdi.Enums;
//using BorgEdi.Models;

namespace PolytecOrderEDI
{
    class CabinetPart
    {
        public string CabinetName { get; set; } = string.Empty;
        public string CNCCODE { get; set; } = string.Empty; 
        public int CabinetNumber { get; set; }
        public int Quantity { get; set; }
        public double Height {  get; set; }
        public double Width {  get; set; }
        public int Thickness { get; set; }
        public string Parameter { get; set; } = string.Empty;
        public string PartDescription { get; set; } = string.Empty;
        public int PartNumber { get; set; }
        public int UniquePartId { get; set; }
        public PRODUCTTYPE ProductType { get; set; }
        public PRODUCT Product { get; set; }
        public PARTNAME PartName { get; set; }
        public HINGETYPE HingeType { get; set; }
        public string EdgeLocation { get; set; } = string.Empty;
        public string HandleSystem { get; set; } = string.Empty;
        public string AdditionalInstructions { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Finish { get; set; } = string.Empty;
        public string Side { get; set; } = string.Empty; //Single or Double Sided (SS or DS)
        public string ContrastingEdgeColour { get; set; } = string.Empty;
        public string ContrastingEdgeFinish { get; set; } = string.Empty;


        //Empty class
        public CabinetPart() { }

        public CabinetPart(ICBPart part)
        {
            CabinetName = part.CabinetName;
            CNCCODE = part.CNCCODE;
            CabinetNumber = part.CabinetNumber;
            Quantity = part.Quantity;
            Height = HelperMethods.RoundDownNumberLessThanDecimalValueElseRoundUp(part.Dimx, lessThanDecimal:0.7);
            Width =  HelperMethods.RoundDownNumberLessThanDecimalValueElseRoundUp(part.Dimy, lessThanDecimal:0.7);
            Thickness = (int) part.Dimz;
            Parameter = part.Parameter;
            PartDescription = part.PartDescription;
            PartNumber = part.PartNumber;
            UniquePartId = part.UniquePartId;

            ProductType = Workout_ProductType(part);
            Product = Workout_Product(part);
            PartName = Workout_PartName(part);
            HingeType = Workout_HingeType(part);
            EdgeLocation = Workout_EdgeLocation(part);
            HandleSystem = Workout_HandleSystem(part);

            var materialInfo = Workout_MaterialInfo(part);
            if (materialInfo != null)
            {
                Color = materialInfo.Color;
                Finish = materialInfo.Finish;
                Side = materialInfo.Side;
            }

            var edgeColorInfo = Workout_ContrastingEdgeColorAndFinish(part);
            if (edgeColorInfo != null)
            {
                ContrastingEdgeColour = edgeColorInfo.Color;
                ContrastingEdgeFinish = edgeColorInfo.Finish;
            }

        }

        //Workout ProductType
        private static PRODUCTTYPE Workout_ProductType(ICBPart part)
        {
            var productType = PRODUCTTYPE.None;
            int thickness = (int)part.Dimz;

            //if (thickness == 16) productType = PRODUCTTYPE.Decorative16mm;
            //else if (thickness == 18) productType = PRODUCTTYPE.Decorative18mm;

            //Until Polytec adds new models for different thciknesses of Decoratitive products, any thickness other than 18mm will be categorized as decorative16mm.
            if (thickness == 18) productType = PRODUCTTYPE.Decorative18mm;
            else productType = PRODUCTTYPE.Decorative16mm;

            return productType;
        }


        //Workout ConfiguredPiece
        private static PRODUCT Workout_Product(ICBPart part)
        {
            var product = PRODUCT.None;
            var PartDescription = part.PartDescription;
            var CabinetName = part.CabinetName;
            string[] dfStrings = ["Drawer_Front", "Drw_Front"];
            //string[] panelStrings = ["Panel", "pnl", "Filler"];


            if (dfStrings.Any(str => PartDescription.Contains(str, StringComparison.OrdinalIgnoreCase)))
            {
                product = PRODUCT.DrawerFront;
            }
            
            else if (PartDescription.Contains("Door", StringComparison.OrdinalIgnoreCase))
            {
                if (CabinetName.Contains("Glass", StringComparison.OrdinalIgnoreCase) && CabinetName.Contains("Frame", StringComparison.OrdinalIgnoreCase))
                {
                    product = PRODUCT.GlassFrame;
                }
                else { product = PRODUCT.Door; }
               
            }

            //else if (panelStrings.Any(str => PartDescription.Contains(str, StringComparison.OrdinalIgnoreCase)))
            else
            {
                if (part.CNCCODE == "RSMPANEL" && CabinetName.Contains("Roller Shutter Panel", StringComparison.OrdinalIgnoreCase)) product = PRODUCT.RollerFrame;
                else if (part.CNCCODE == "RSMPANEL" && CabinetName.Contains("Microwave Panel", StringComparison.OrdinalIgnoreCase)) product = PRODUCT.CutOut;
                else product = PRODUCT.Panel;
            }

            return product;
        }



        //Workout_PartName
        private static PARTNAME Workout_PartName(ICBPart part)
        {
            PARTNAME partName = PARTNAME.None;
            PRODUCT product = Workout_Product(part);
            var partDescription = part.PartDescription;
            var CncCode = part.CNCCODE;
            var dict_param = HelperMethods.SplitParameter(part.Parameter);
            var hand = dict_param.TryGetValue("HAND", out double paramValue) ? (int)paramValue : 0;

            //DRAWER FRONTS
            if (product == PRODUCT.DrawerFront)
            {
                if (partDescription.Contains("Left", StringComparison.OrdinalIgnoreCase))         partName = PARTNAME.Left;
                else if (partDescription.Contains("Right", StringComparison.OrdinalIgnoreCase))   partName = PARTNAME.Right;
            }

            //GLASS FRAME DOOR
            else if (product == PRODUCT.GlassFrame)
            {
                partName = (hand == 1) ? PARTNAME.Left : (hand == 2) ? PARTNAME.Right : PARTNAME.None;
            }

            //DOORS
            else if (product == PRODUCT.Door)
            {
                //Bi-fold doors
                if (CncCode == "BCCDOORL" || CncCode == "BCCDOORR") 
                {
                    if  (hand == 1) partName = PARTNAME.Left_Bifold;
                    else if (hand == 3) partName = PARTNAME.Right_Leaf;
                    else if (hand == 2) partName = PARTNAME.Right_Bifold;
                    else if (hand == 4) partName = PARTNAME.Left_Leaf;
                    else partName = PARTNAME.None;
                }
                //770 Style Bi-fold doors 
                else if (CncCode.Contains("BFDR_", StringComparison.OrdinalIgnoreCase))
                {
                    if (hand == 1 || hand == 2)
                    {
                        if (CncCode == "BFDR_L1") partName = PARTNAME.Left_770;
                        else if (CncCode == "BFDR_L2") partName = PARTNAME.Right_Leaf_770;
                        else if (CncCode == "BFDR_R1") partName = PARTNAME.Right_770;
                        else if (CncCode == "BFDR_R2") partName = PARTNAME.Left_Leaf_770 ;
                        else partName = PARTNAME.None; 
                    }
                }
                //Hamper Doors
                else if (CncCode == "HAMPDOOR")
                {
                    if (hand == 1)
                    {
                        if (partDescription.Contains("Hamper", StringComparison.OrdinalIgnoreCase))     partName = PARTNAME.Top ;
                        else if (partDescription.Contains("Left", StringComparison.OrdinalIgnoreCase))  partName = PARTNAME.Top_Bifold ;
                        else if (partDescription.Contains("Right", StringComparison.OrdinalIgnoreCase)) partName = PARTNAME.Top_Leaf ;
                    }
                }
                //Standard doors
                else
                {
                    partName = (hand == 1) ? PARTNAME.Left : (hand == 2) ? PARTNAME.Right : PARTNAME.None;   
                }
            }

            //PANELS
            else if (product == PRODUCT.Panel)
            {
                //Blind Cabinet Panels
                if (CncCode == "BSBLINDP")
                {
                    partName = (hand == 1) ? PARTNAME.Left_Blind_Panel : (hand == 2) ? PARTNAME.Right_Blind_Panel  : PARTNAME.None;    
                }
            }
            return partName;
        }


        //Workout HingeType
        private static HINGETYPE Workout_HingeType(ICBPart part)
        {
            var hType = HINGETYPE.None;
            var dict_param = HelperMethods.SplitParameter(part.Parameter);
            if (dict_param.Count > 0)
            {
                var hcls = dict_param.TryGetValue("HCLS", out var paramValue) ? (int)paramValue : -1;
                if (hcls == 0) hType = HINGETYPE.Blum;
                else if (hcls == 1) hType = HINGETYPE.Hettich;
            }
            return hType;
        }


        //Workout EdgeLocation
        private static string Workout_EdgeLocation(ICBPart part)
        {
            string edgeLocation = string.Empty;
            edgeLocation += (part.TopEdgeDescription == "")     ? "X" : HelperMethods.IsEdgeMelamineHandle(part.TopEdgeDescription)     ? "H" : "1";
            edgeLocation += (part.BottomEdgeDescription == "")  ? "X" : HelperMethods.IsEdgeMelamineHandle(part.BottomEdgeDescription)  ? "H" : "1";
            edgeLocation += (part.LeftEdgeDescription == "")    ? "X" : HelperMethods.IsEdgeMelamineHandle(part.LeftEdgeDescription)    ? "H" : "1";
            edgeLocation += (part.RightEdgeDescription == "")   ? "X" : HelperMethods.IsEdgeMelamineHandle(part.RightEdgeDescription)   ? "H" : "1";
            return edgeLocation;
        }


        //Workout HandleSystem
        private static string Workout_HandleSystem(ICBPart part)
        {
            string concatEdgeDescription = part.TopEdgeDescription + part.BottomEdgeDescription + part.LeftEdgeDescription + part.RightEdgeDescription;
            if (concatEdgeDescription.Contains("45Deg", StringComparison.OrdinalIgnoreCase))   return "Bevel Edge";
            if (concatEdgeDescription.Contains("FPHA", StringComparison.OrdinalIgnoreCase))    return "Finger Pull Aluminium";
            if (concatEdgeDescription.Contains("FPHBA", StringComparison.OrdinalIgnoreCase))   return "Finger Pull Black Anodised";
            else return "";
        }

        //Workout Material Color and Finish
        private static PolyColor? Workout_MaterialInfo(ICBPart part)
        {
            var material = part.Material;

            //Replace 'WhiteHmrParticleBoard' with Polytec Color 'CarcassTexture'. Replace 'BlackHmrParticleBoard' with polytec Color 'BlackTexture'
            if (string.Equals(material, HMRBOARD.WhiteHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase)) material = "CarcassTexture";
            if (string.Equals(material, HMRBOARD.BlackHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase)) material = "BlackTexture";

            return TablePolytecBoardColors.GetColorInfo(material);
        }


        //Workout Contrasting Edge Color and Finish
        private static PolyColor? Workout_ContrastingEdgeColorAndFinish(ICBPart part)
        {
            var replaceEdgeColor = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"black","BlackTexture" },{"white","CarcassTexture"}
            };

            var edgeColor = HelperMethods.GetEdgeColor(part);

            if (edgeColor.Length > 0)
            {
                if (edgeColor == "matching")
                {
                    edgeColor = string.Empty;
                }
                else if(edgeColor == "black")
                {
                    if(string.Equals(part.Material, HMRBOARD.BlackHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase)) edgeColor = string.Empty;
                    else edgeColor = replaceEdgeColor["black"];
                }
                else if(edgeColor == "white")
                {
                    if (string.Equals(part.Material, HMRBOARD.WhiteHmrParticleBoard.ToString(), StringComparison.OrdinalIgnoreCase)) edgeColor = string.Empty;
                    else edgeColor = replaceEdgeColor["white"];
                }
                else
                {
                    if (string.Equals(part.Material, edgeColor, StringComparison.OrdinalIgnoreCase)) edgeColor = string.Empty;                        
                }
            }

            return TablePolytecBoardColors.GetColorInfo(edgeColor);
        }



    }//End of class
}
