


namespace PolytecOrderEDI
{
    //Blueprint for drilling patterns  : 
    //All drillling patterns are referenced from back.
    public class HolePatternBP
    {
        public bool HasDrillingInfo { get; private set; } = false;
        public HOLEPATTERNORIENTATION HolePatternOrientation { get; private set; }
        public int NumHolesLeft { get; private set; }
        public int NumHolesRight { get; private set; }
        public int Gap1 { get; private set; }
        public int Gap2 { get; private set; }
        public int Gap3 { get; private set; }
        public int Gap4 { get; private set; }
        public int Gap5 { get; private set; }
        public double LeftDefaultINUP { get; private set; }
        public double RightDefaultINUP { get; private set; }
        public double HoleDepth { get; private set; }


        public HolePatternBP(HOLEPATTERNORIENTATION holePatternOrientation=HOLEPATTERNORIENTATION.None, int numHolesLeft = 0, int numHolesRight = 0, int gap1 = 0, int gap2 = 0, int gap3 = 0, int gap4 = 0, int gap5 = 0, double leftDefaultINUP = 0, double rightDefaultINUP = 0, double holeDepth = 0)
        {
            this.HolePatternOrientation = holePatternOrientation;
            this.HasDrillingInfo = numHolesLeft > 0 && numHolesRight > 0;
            this.NumHolesLeft = numHolesLeft;
            this.NumHolesRight = numHolesRight;
            this.Gap1 = gap1;
            this.Gap2 = gap2;
            this.Gap3 = gap3;
            this.Gap4 = gap4;
            this.Gap5 = gap5;
            this.LeftDefaultINUP = leftDefaultINUP;
            this.RightDefaultINUP = rightDefaultINUP;
            this.HoleDepth = holeDepth;
        }
    }

}
