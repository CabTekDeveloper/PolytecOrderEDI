
namespace PolytecOrderEDI
{
    class DoorStyleDetails
    {
        public string StyleName { get; private set; } = string.Empty;
        public int StyleNo { get; private set; } 
        public string Edge { get; private set; } = string.Empty;
        public double MinHeight { get; private set; }
        public double MinWidth { get; private set; } 

        public DoorStyleDetails() { }

        public DoorStyleDetails(string stylName, int styleNo, string edge, double minHeight, double minWidth)
        {
            StyleName = stylName;
            StyleNo = styleNo;
            Edge = edge;
            MinHeight = minHeight;
            MinWidth = minWidth;
        }


    }
}
