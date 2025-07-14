
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BorgEdi.Models;

namespace PolytecOrderEDI
{
    public static class HolePatternDrawerFront
    {   
        //For Corner Drawer Cabinets
        private static HolePattern cornerDFtype1 = new(numHolesLeft: 2, numHolesRight: 2, gap1:32, holeDepth:12);
        private static HolePattern cornerDFleftType2 = new(numHolesLeft: 4,numHolesRight: 2,gap1: 32,gap2: 64, gap3:32, rightDefaultINUP: 81.5, holeDepth: 12);
        private static HolePattern cornerDFrightType2 = new(numHolesLeft: 2, numHolesRight: 4, gap1: 32, gap2: 64, gap3: 32, leftDefaultINUP: 81.5, holeDepth: 12);
        
        //For Standard Drawer Cabinets
        private static HolePattern DFdrillingType1 = new(numHolesLeft: 2, numHolesRight: 2, gap1: 32, holeDepth: 12);
        private static HolePattern DFdrillingType2 = new(numHolesLeft: 2, numHolesRight: 2, gap1: 64, holeDepth: 12);
        private static HolePattern DFdrillingType3 = new(numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 64, holeDepth: 12);
        private static HolePattern DFdrillingType4 = new(numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 128, holeDepth: 12);
        private static HolePattern DFdrillingType5 = new(numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 96, holeDepth: 12);
        private static HolePattern DFdrillingType6 = new(numHolesLeft: 4, numHolesRight: 4, gap1: 32, gap2: 64, gap3:32, holeDepth: 12);
        private static HolePattern DFdrillingType7 = new(numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 32, holeDepth: 12);
        private static HolePattern DFdrillingType8 = new(numHolesLeft: 1, numHolesRight: 1, holeDepth: 12);
        private static HolePattern DFdrillingType9 = new(numHolesLeft: 6, numHolesRight: 6, gap1: 32, gap2: 64, gap3: 32, gap4: 32, gap5: 32, holeDepth: 12);


        public static HolePattern GetDrillingInfo(int DTYP,string LorR)
        {
            switch (DTYP)
            {
                case 1: 
                    return cornerDFtype1;

                case 2:
                    if (string.Equals(LorR, "left", StringComparison.OrdinalIgnoreCase)) return cornerDFleftType2;
                    if (string.Equals(LorR, "right", StringComparison.OrdinalIgnoreCase)) return cornerDFrightType2;
                    return new HolePattern();

                case 4:
                case 9:
                case 13:
                case 90:
                case 180:
                case 400:
                case 401:
                case 402:
                case 403:
                case 404:
                case 405:
                    return DFdrillingType1;

                case 5:
                case 6:
                case 7:
                    return DFdrillingType2;

                case 10:
                case 100:
                    return  DFdrillingType3;

                case 11:
                case 110:
                case 406:
                case 407:
                    return DFdrillingType4;

                case 12:
                    return DFdrillingType5;

                case 14:
                case 182:
                case 183:
                    return DFdrillingType6;

                case 17:
                case 181:
                    return DFdrillingType7;

                case 900:
                    return DFdrillingType8;   //Single hole

                case 16:
                    return DFdrillingType9;

                default: 
                    return new HolePattern();
            }
        }


    }
}
