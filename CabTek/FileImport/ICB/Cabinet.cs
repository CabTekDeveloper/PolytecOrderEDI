//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    class Cabinet
    {
        public string CabinetName { get; private set; }
        public int CabinetNumber { get; private set; }
        public List<CabinetPart> Parts { get; private set; } = [];
        public List<CabinetPart> StdDrawerBank { get; private set; } = [];
        public List<CabinetPart> LeftDrawerBank { get; private set; } = [];
        public List<CabinetPart> RightDrawerBank { get; private set; } = [];   

        public Cabinet(List<ICBPart> cabinetData)
        {
            CabinetName  = cabinetData[0].CabinetName;
            CabinetNumber = cabinetData[0].CabinetNumber;
           
            foreach (var part in cabinetData)
            {
                var cabinetPart = new CabinetPart(part);
                var product = cabinetPart.Product;
                var partName = cabinetPart.PartName;

                if (product == PRODUCT.DrawerFront)
                {
                    if (partName == PARTNAME.Left) LeftDrawerBank.Add(cabinetPart);
                    else if (partName == PARTNAME.Right) RightDrawerBank.Add(cabinetPart);
                    else StdDrawerBank.Add(cabinetPart);
                }
                else
                {
                    Parts.Add(cabinetPart);
                }
            }

            //Reverse the drawer bank list so that Drawer_Front_1 sits at the bottom of the Bank, Drawer_Front_2 on top of 1 and so on.
            StdDrawerBank.Reverse();
            LeftDrawerBank.Reverse();
            RightDrawerBank.Reverse();
            //End 

        }
        
        public HINGETYPE GetHingeType()
        {
            var hingeType = HINGETYPE.None;

            foreach (var part in Parts)
            {
                if (part.HingeType != HINGETYPE.None)
                {
                    hingeType = part.HingeType;
                    break;
                }
            }

            return hingeType;
        }


    }//End of class
}//End of namespace


