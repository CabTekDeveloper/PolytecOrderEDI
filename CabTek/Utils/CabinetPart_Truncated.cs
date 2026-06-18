//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class CabinetPart_Truncated(CabinetPart part)
    {
        //public string CabinetName { get; set; } = part.CabinetName;
        public int CabinetNumber { get; set; } = part.CabinetNumber;
        public int PartNumber { get; set; } = part.PartNumber;
        public string PartDescription { get; set; } = part.PartDescription;
        public string Color {  get; set; } = part.Color;
        public string Finish {  get; set; } = part.Finish;
        public string AdditionalInstructions { get; set; } = part.AdditionalInstructions;
    }
}
