//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class BuildParameter_DrawerFront
    {
        public int DTYP1 { get; private set; }
        public int DTYP2 { get; private set; }
        public double INUP1 { get; private set; }
        public double INUP2 { get; private set; }
        public double LINS {  get; private set; }
        public double RINS { get; private set; }
        public double HDIA { get; private set; }


        //Default instance
        public BuildParameter_DrawerFront() { }


        public BuildParameter_DrawerFront(CabinetPart part)
        {
            PARTNAME partName = part.PartName;
            var dict_param = HelperMethods.SplitParameter(part.Parameter);

            if (dict_param.Count > 0)
            {
                double width = part.Width;
                double paramValue;

                int dtyp1       = dict_param.TryGetValue("DTYP", out paramValue) ? (int) paramValue : 0;
                double inup1    = dict_param.TryGetValue("INUP", out paramValue) ? paramValue : 0;
                double lins     = dict_param.TryGetValue("LINS", out paramValue) ? paramValue : 0;
                double rins     = dict_param.TryGetValue("RINS", out paramValue) ? paramValue : 0;
                double sins     = dict_param.TryGetValue("SINS", out paramValue) ? paramValue : 0;
                double wide     = dict_param.TryGetValue("WIDE", out paramValue) ? paramValue : 0;
                double hdia     = dict_param.TryGetValue("HDIA", out paramValue) ? paramValue : 0;
                double ldia     = dict_param.TryGetValue("LDIA", out paramValue) ? paramValue : 0;

                DTYP1 = dtyp1;
                INUP1 = inup1;
                HDIA  = (partName == PARTNAME.None) ? ldia : hdia;
                
                //Workout Left and Right Insets
                if (partName == PARTNAME.Left)
                {
                    LINS = sins;
                    RINS = width - wide - sins;
                }
                else if (partName == PARTNAME.Right)
                {
                    LINS = width - wide - sins;
                    RINS = sins;
                }
                else
                {
                    LINS = lins;
                    RINS = rins;
                }
            }
            
        }

      
    }
}
