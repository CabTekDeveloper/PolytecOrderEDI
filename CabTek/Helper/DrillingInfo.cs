//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace PolytecOrderEDI
{
    static class SpotHole
    {
        public static double Inup { get; } = 10;
        public static double Radius { get; } = 2.5;
        public static double Inset { get; } = 20;
        public static double Depth { get; } = 1;
    }

    static class CustomHingeBlock
    {
        public static double HoleGap { get; } = 32;
        public static double HoleDepth { get; }  = 12;
        public static double HoleRadius { get; } = 2.5;
    }

    static class DoorStyle770
    {
        public static double CupHoleGap { get; } = 64;
        public static double CupHoleDepth { get; } = 13;
        public static double CupHoleRadius { get; } = 17.5;
    }

    static class CompactDoorHingeBlockHole
    {
        public static double Radius { get; } = 5;
        public static double Depth { get; } = 6;
        public static double Gap { get; } = 32;
    }

    static class CompactDrawerHoleDepth
    { 
        public static double HoleDepth { get; } = 6;
    }


}
