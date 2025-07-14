//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class ICBPart
    {
        public string CabinetName { get; set; } = string.Empty;
        public string CNCCODE { get; set; } = string.Empty;
        public int CabinetNumber { get; set; }
        public int Quantity { get; set; }
        public double Dimx { get; set; }
        public double Dimy { get; set; }
        public double Dimz { get; set; }
        public string OptSetting { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Grain { get; set; } = string.Empty;
        public string LeftEdgeDescription { get; set; } = string.Empty;
        public string RightEdgeDescription { get; set; } = string.Empty;
        public string TopEdgeDescription { get; set; } = string.Empty;
        public string BottomEdgeDescription { get; set; } = string.Empty;
        public string Edge2 { get; set; } = string.Empty;
        public string Edge4 { get; set; } = string.Empty;
        public string Edge3 { get; set; } = string.Empty;
        public string Edge1 { get; set; } = string.Empty;
        public string Parameter { get; set; } = string.Empty;
        public int PartNumber { get; set; }
        public string PartDescription { get; set; } = string.Empty;
        public int UniquePartId { get; set; }
        public string JobNumber { get; set; } = string.Empty;
        public string ClientOrderNumber { get; set; } = string.Empty;
        public string MultiShapeManagement { get; set; } = string.Empty;
        public string ExtraMargin { get; set; } = string.Empty;

        public ICBPart() { }


        public ICBPart(string[] part)
        {
            CabinetName             = part[0].Trim();
            CNCCODE                 = part[1].Trim().ToUpper();
            CabinetNumber           = string.IsNullOrEmpty(part[2].Trim()) ? 0 : Int32.Parse(part[2].Trim());
            Quantity                = string.IsNullOrEmpty(part[3].Trim()) ? 0 : Int32.Parse(part[3].Trim());
            Dimx                    = string.IsNullOrEmpty(part[4].Trim()) ? 0 : double.Parse(part[4].Trim());
            Dimy                    = string.IsNullOrEmpty(part[5].Trim()) ? 0 : double.Parse(part[5].Trim());
            Dimz                    = string.IsNullOrEmpty(part[6].Trim()) ? 0 : double.Parse(part[6].Trim());
            OptSetting              = part[7].Trim();
            Material                = Format_Material(part[8].Trim());
            Grain                   = part[9].Trim();
            LeftEdgeDescription     = part[10].Trim();
            RightEdgeDescription    = part[11].Trim();
            TopEdgeDescription      = part[12].Trim();
            BottomEdgeDescription   = part[13].Trim();
            Edge2                   = part[14].Trim();
            Edge4                   = part[15].Trim();
            Edge3                   = part[16].Trim();
            Edge1                   = part[17].Trim();
            Parameter               = part[18].Trim().Replace(@"""", string.Empty).ToUpper();
            PartNumber              = string.IsNullOrEmpty(part[19].Trim()) ? 0 : Int32.Parse(part[19].Trim());
            PartDescription         = part[20].Trim();
            UniquePartId            = string.IsNullOrEmpty(part[21].Trim()) ? 0 : Int32.Parse(part[21].Trim());
            JobNumber               = part[22].Trim();
            ClientOrderNumber       = part[23].Trim();
            MultiShapeManagement    = part[24].Trim();
            ExtraMargin             = part[25].Trim();
        }

        
        private static string Format_Material(string material)
        {
            string formatted_material = "";

            //Since the colors CafeCream and CafeOak are imported incorrectly, we have to format it to match the color in the EDI Database.
            if (material.StartsWith("caf", StringComparison.OrdinalIgnoreCase))
            {
                formatted_material = material.Remove(0, 4);
                if (material.Contains("cream", StringComparison.OrdinalIgnoreCase) || material.Contains("oak", StringComparison.OrdinalIgnoreCase))
                {
                    formatted_material = "Cafe" + formatted_material;
                }
                else
                {
                    formatted_material = "";
                }
            }
            return (formatted_material.Length > 0) ? formatted_material : material;
        }


        public static ICBPart Clone(ICBPart part)
        {
            return new ICBPart()
            {
                CabinetName             = part.CabinetName,
                CNCCODE                 = part.CNCCODE,
                CabinetNumber           = part.CabinetNumber,
                Quantity                = part.Quantity,
                Dimx                    = part.Dimx,
                Dimy                    = part.Dimy,
                Dimz                    = part.Dimz,
                OptSetting              = part.OptSetting,
                Material                = part.Material,
                Grain                   = part.Grain,
                LeftEdgeDescription     = part.LeftEdgeDescription,
                RightEdgeDescription    = part.RightEdgeDescription,
                TopEdgeDescription      = part.TopEdgeDescription,
                BottomEdgeDescription   = part.BottomEdgeDescription,
                Edge2                   = part.Edge2,
                Edge4                   = part.Edge4,
                Edge3                   = part.Edge3,
                Edge1                   = part.Edge1,
                Parameter               = part.Parameter,
                PartNumber              = part.PartNumber,
                PartDescription         = part.PartDescription,
                UniquePartId            = part.UniquePartId,
                JobNumber               = part.JobNumber,
                ClientOrderNumber       = part.ClientOrderNumber,
                MultiShapeManagement    = part.MultiShapeManagement,
                ExtraMargin             = part.ExtraMargin,
            };
        }

    }


}
