//using BorgEdi.Enums;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class BuildParameter_Handle
    {
        public bool HasHandle { get; set; }
        public bool IsHandleVertical { get; private set; }       
        public double Hole1TopInset { get; private set; }
        public double Hole2TopInset { get; private set; }
        public double SideInset { get; private set; }
        public double HoleGap {  get; private set; }
        public double HoleDiameter { get; private set; }   

        public BuildParameter_Handle() { }

        public BuildParameter_Handle(CabinetPart part)
        {
            var dict_param = HelperMethods.SplitParameter(part.Parameter);
            if (dict_param.Count > 0)
            {
                int hdlt = dict_param.TryGetValue("HDLT", out double paramValue) ? (int)paramValue : 0;
                if (hdlt==1 || hdlt==2)
                {
                    int hdlo    = dict_param.TryGetValue("HDLO", out paramValue) ? (int)paramValue : 0;     // Orientation, HDLO=0:Vertical, DHLO=1:Horizontal
                    double hdls = dict_param.TryGetValue("HDLS", out paramValue) ? paramValue : 0;          // HoleGap
                    double hdld = dict_param.TryGetValue("HDLD", out paramValue) ? paramValue : 0;          // HoleDiameter
                    double hdlx = dict_param.TryGetValue("HDLX", out paramValue) ? paramValue : 0;          // SideInset
                    double hdly = dict_param.TryGetValue("HDLY", out paramValue) ? paramValue : 0;          // Y_Inset 
                    int hddp    = dict_param.TryGetValue("HDDP", out paramValue) ? (int)paramValue : 0;     // For Drawers, HDDP=0: Distance Front Top (set Y_Inset to HDLY), HDDP=1: Center vertically (set Y_Inset to Height/2)

                    //Set values
                    bool isUpperCabinet = (part.CabinetName.Contains("Upper", StringComparison.OrdinalIgnoreCase));
                    var width = part.Width;
                    var height = part.Height;

                    double h1_topInset;
                    double h2_topInset;

                    //Workout fisrt hole top inset
                    if (part.Product == PRODUCT.DrawerFront)
                    {
                        hdlo = 1; //Set Handle orientation on a DrawerFront to horizontal
                        hdlx = (hdlt == 1) ? (width / 2) : (width / 2 - hdls / 2);
                        h1_topInset = (hddp == 0) ? hdly : (height / 2);
                    }
                    else { h1_topInset = (isUpperCabinet) ? (height - hdly) : (hdly); }

                    //Workout second hole top inset
                    if (hdlt == 2)
                    {
                        if (hdlo == 0) h2_topInset = (isUpperCabinet) ? (h1_topInset - hdls) : (h1_topInset + hdls);
                        else h2_topInset = h1_topInset;
                    }
                    else { h2_topInset = 0; }

                    HasHandle = true;
                    IsHandleVertical = (hdlo == 0);
                    Hole1TopInset = h1_topInset;
                    Hole2TopInset = h2_topInset;
                    SideInset = hdlx;
                    HoleGap = hdls;
                    HoleDiameter = hdld;

                }
            }
            
        }

      
    }
}




//void AddHandle()
//{
//    bool isUpperCabinet = (vinylPart.CabinetName.Contains("Upper", StringComparison.OrdinalIgnoreCase));
//    bool isVerticalHandle = (HandleParams.Orientation == 0);

//    var hole1_xInset = (HandleParams.SideInset);
//    var hole1_yInset = (isUpperCabinet) ? (HandleParams.Y_Inset) : (height - HandleParams.Y_Inset);

//    var hole2_xInset = (isVerticalHandle) ? hole1_xInset : (hole1_xInset + HandleParams.HoleGap);
//    double hole2_yInset = (isUpperCabinet) ? (hole1_yInset + HandleParams.HoleGap) : (hole1_yInset - HandleParams.HoleGap);

//}