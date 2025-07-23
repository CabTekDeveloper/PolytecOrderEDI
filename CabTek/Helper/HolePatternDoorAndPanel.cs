

namespace PolytecOrderEDI
{

    //Hamper Door Hole Drilling
    public static class HolePatternDoorAndPanel
    {
        private static HolePatternBP Type1 { get; } = new(holePatternOrientation:HOLEPATTERNORIENTATION.Vertical,  numHolesLeft: 4, numHolesRight: 4, gap1: 32, gap2: 32, gap3: 32, holeDepth: 12);
        private static HolePatternBP Type2 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.Vertical, numHolesLeft: 2, numHolesRight: 2, gap1: 32, holeDepth: 12);
        private static HolePatternBP Type1002 { get; } = new(holePatternOrientation: HOLEPATTERNORIENTATION.None, numHolesLeft: 1, numHolesRight: 1, holeDepth: 12);

        public static HolePatternBP GetDrillingInfo(int type)
        {
            switch (type)
            {
                case 1:
                    return Type1;
                case 2:
                    return Type2;
                case 1002:
                    return Type1002;
                default:
                    return new HolePatternBP();   

            }
        }

    }

}
