//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class PolyColor(string materialCode, string color, string finish, string side, string grain, string materialDescription, string in_16mm, string in_18mm)
    {
        public string MaterialCode { get; } = materialCode.Trim();
        public string Color { get; } = color.Trim();
        public string Finish { get; } = finish.Trim();
        public string Side { get; } = side.Trim();
        public string Grain { get; } = grain.Trim();
        public string MaterialDescription { get; } = materialDescription.Trim();
        public string In_16mm { get; } = in_16mm.Trim();
        public string In_18mm { get; } = in_18mm.Trim();
    }
}
