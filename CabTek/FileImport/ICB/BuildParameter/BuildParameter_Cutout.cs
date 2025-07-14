//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class BuildParameter_Cutout
    {
        public int STTP { get; private set; }        //0: No cutout, 1: Roller Frame Panel, 2: Cutout
        public double CutoutLeftBorder { get; private set; }
        public double CutoutRightBorder { get; private set; }
        public double CutoutTopBorder { get; private set; }
        public double CutoutBottomBorder { get; private set; }

        //public bool HasCutout2;
        //public double cutoutInternalHeight1;
        //public bool hasCutout2;
        //public double cutoutLeftBorder2;
        //public double cutoutRightBorder2;
        //public double cutoutBottomBorder2;


        //Default instance
        public BuildParameter_Cutout() { }


        public BuildParameter_Cutout(CabinetPart part)
        {
            var dict_param = HelperMethods.SplitParameter(part.Parameter);

            if (dict_param.Count > 0)
            {
                double paramValue;
                int sttp        = dict_param.TryGetValue("STTP", out paramValue) ? (int) paramValue : 0;
                double stla     = dict_param.TryGetValue("STLA", out paramValue) ? paramValue : 0;
                double stlb     = dict_param.TryGetValue("STLB", out paramValue) ? paramValue : 0;
                double stlc     = dict_param.TryGetValue("STLC", out paramValue) ? paramValue : 0;
                double stld     = dict_param.TryGetValue("STLD", out paramValue) ? paramValue : 0;

                //Set values
                STTP = sttp;
                CutoutLeftBorder = stla;
                CutoutRightBorder = stlc;
                CutoutTopBorder = stlb;
                CutoutBottomBorder = stld;
            }
            
        }

    }
}
