
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BorgEdi.Models;

namespace PolytecOrderEDI
{

    //Hamper Door Hole Drilling
    public static class HolePatternHamperDoor
    {
        private static HolePattern type1 = new HolePattern(numHolesLeft: 4, numHolesRight: 4, gap1: 32, gap2: 32, gap3: 32, holeDepth: 12);
        private static HolePattern type2 = new HolePattern(numHolesLeft: 2, numHolesRight: 2, gap1: 32, holeDepth: 12);

        public static HolePattern GetDrillingInfo(int type)
        {
            switch (type)
            {
                case 1:
                    return type1;
                case 2:
                    return type2;

                default:
                    return new HolePattern();   

            }
        }

    }

}
