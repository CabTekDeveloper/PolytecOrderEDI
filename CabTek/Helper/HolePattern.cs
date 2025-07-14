


namespace PolytecOrderEDI
{
    //Blueprint for drilling patterns  : 
    //All drillling patterns are referenced from back.
    public class HolePattern
    {
        public bool hasDrillingInfo = false;
        public int numHolesLeft;
        public int numHolesRight;
        public int gap1;
        public int gap2;
        public int gap3;
        public int gap4;
        public int gap5;
        public double leftDefaultINUP;
        public double rightDefaultINUP;
        public double holeDepth;

        public HolePattern(int numHolesLeft = 0, int numHolesRight = 0, int gap1 = 0, int gap2 = 0, int gap3 = 0, int gap4 = 0, int gap5 = 0, double leftDefaultINUP = 0, double rightDefaultINUP = 0, double holeDepth = 0)
        {
            hasDrillingInfo = numHolesLeft > 0 && numHolesRight > 0 ? true : false;
            this.numHolesLeft = numHolesLeft;
            this.numHolesRight = numHolesRight;
            this.gap1 = gap1;
            this.gap2 = gap2;
            this.gap3 = gap3;
            this.gap4 = gap4;
            this.gap5 = gap5;
            this.leftDefaultINUP = leftDefaultINUP;
            this.rightDefaultINUP = rightDefaultINUP;
            this.holeDepth = holeDepth;
        }
    }

}
