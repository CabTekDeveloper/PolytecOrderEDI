//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class PolyColor(string materialCode, string color, string finish, string side, string grain, string materialDescription)
    {
        public string MaterialCode { get; } = materialCode.Trim();
        public string Color { get; } = color.Trim();
        public string Finish { get; } = finish.Trim();
        public string Side { get; } = side.Trim();
        public string Grain { get; } = grain.Trim();
        public string MaterialDescription { get; } = materialDescription.Trim();

    }
}
