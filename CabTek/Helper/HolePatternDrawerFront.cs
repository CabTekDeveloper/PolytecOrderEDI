
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
        private static  HolePatternBP CornerDFtype1 { get; }      = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 2, numHolesRight: 2, gap1: 32, holeDepth: 12);
        private static  HolePatternBP CornerDFleftType2 { get; }  = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 4, numHolesRight: 2, gap1: 32, gap2: 64, gap3: 32, rightDefaultINUP: 81.5, holeDepth: 12);
        private static  HolePatternBP CornerDFrightType2 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 2, numHolesRight: 4, gap1: 32, gap2: 64, gap3: 32, leftDefaultINUP: 81.5, holeDepth: 12);
        
        //For Standard Drawer Cabinets
        private static HolePatternBP DFdrillingType1 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 2, numHolesRight: 2, gap1: 32, holeDepth: 12);
        private static readonly HolePatternBP DFdrillingType2 = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 2, numHolesRight: 2, gap1: 64, holeDepth: 12);
        private static HolePatternBP DFdrillingType3 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 64, holeDepth: 12);
        private static HolePatternBP DFdrillingType4 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 128, holeDepth: 12);
        private static HolePatternBP DFdrillingType5 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 96, holeDepth: 12);
        private static HolePatternBP DFdrillingType6 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 4, numHolesRight: 4, gap1: 32, gap2: 64, gap3: 32, holeDepth: 12);
        private static HolePatternBP DFdrillingType7 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 3, numHolesRight: 3, gap1: 32, gap2: 32, holeDepth: 12);
        private static HolePatternBP DFdrillingType8 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.None, numHolesLeft: 1, numHolesRight: 1, holeDepth: 12);
        private static HolePatternBP DFdrillingType9 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 6, numHolesRight: 6, gap1: 32, gap2: 64, gap3: 32, gap4: 32, gap5: 32, holeDepth: 12);
                
                        
        public static HolePatternBP GetDrillingInfo(int DTYP,string LorR)
        {
            switch (DTYP)
            {
                case 1: 
                    return CornerDFtype1;

                case 2:
                    if (string.Equals(LorR, "left", StringComparison.OrdinalIgnoreCase)) return CornerDFleftType2;
                    if (string.Equals(LorR, "right", StringComparison.OrdinalIgnoreCase)) return CornerDFrightType2;
                    return new HolePatternBP();

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
                    return new HolePatternBP();
            }
        }

                
    }
}
