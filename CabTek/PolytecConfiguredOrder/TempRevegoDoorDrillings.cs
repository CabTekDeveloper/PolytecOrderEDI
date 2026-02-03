
// This is a one off program to add drillings to Revego doors of size 1815*536
// All the drillings are added to suit the above door size

using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class TempRevegoDoorDrillings
    {
        private static Product? ConfiguredProduct { get; set; }
        private static VinylPart? vinylPart { get; set; } = null;


        // All drillings are from Top Left
        public static void AddDrillings(Product configuredProduct, VinylPart vinyl_part )
        {

            ConfiguredProduct = configuredProduct;
            vinylPart = vinyl_part;

            switch (vinylPart.PartName)
            {
                case PARTNAME.None:
                    break;

                case PARTNAME.Revego_Left:
                    RevegoLefFronttDrillings();
                    break;

                case PARTNAME.Revego_Right_Inner:
                    RevegoRightInnerFrontDrillings();
                    break;

                case PARTNAME.Revego_Right_Outer:
                    RevegoRightOuterFrontDrillings();
                    break;
                
                
                
                default:
                    break;
            }

        }

        private static void RevegoLefFronttDrillings()
        {
            // Top holes
            AddHoleFromTopLeft(35, 111);
            AddHoleFromTopLeft(35, 140);
            AddHoleFromTopLeft(35, 356);
            AddHoleFromTopLeft(35, 484);

            // Left holes
            AddHoleFromTopLeft(120, 10);
            AddHoleFromTopLeft(130, 10);
            AddHoleFromTopLeft(1719, 16.5);

            // Bottom holes
            AddHoleFromTopLeft(1780, 16.5);
            AddHoleFromTopLeft(1780, 111);
            AddHoleFromTopLeft(1780, 140);
            AddHoleFromTopLeft(1780, 356);
            AddHoleFromTopLeft(1780, 484);

            //Right holes
            AddBlockOf4Holes(72, 470.5, "right");
            AddBlockOf4Holes(374, 470.5, "right");
            AddBlockOf4Holes(855, 470.5, "right");
            AddBlockOf4Holes(1412, 470.5, "right");
            AddBlockOf4Holes(1638, 470.5, "right");
        }

        private static void RevegoRightInnerFrontDrillings()
        {
            // Left holes
            AddBlockOf6Holes(37, 20.5, "left");
            AddBlockOf6Holes(197, 20.5, "left");
            AddBlockOf6Holes(835.5, 20.5, "left");
            AddBlockOf6Holes(1474, 20.5, "left");
            AddBlockOf6Holes(1634, 20.5, "left");

            // Right holes
            AddBlockOf4Holes(29, 465.5, "right");
            AddHoleFromTopLeft(1690, 519.5);
            AddHoleFromTopLeft(1754, 519.5);
        }

        private static void RevegoRightOuterFrontDrillings()
        {
            // Top holes
            AddBigBlockOf5Holes(37, 275.5, "top");

            // Left holes
            AddBlockOf4Holes(146, 38.5, "left");
            AddBlockOf4Holes(310, 38.5, "left");
            AddBlockOf4Holes(839, 38.5, "left");
            AddBlockOf4Holes(1389, 38.5, "left");
            AddBlockOf4Holes(1553, 38.5, "left");

            // Right Holes
            AddBlockOf5Holes(37, 483.5, "right");
            AddBlockOf5Holes(197, 483.5, "right");

            AddHoleFromTopLeft(535.5, 483.5 , 8); // Make it 8mm deep since 13mm deep will come through on the other side of Casino face profile
            AddHoleFromTopLeft(535.5, 515.5);

            AddBlockOf5Holes(835.5, 483.5, "right");
            AddBlockOf5Holes(1474, 483.5, "right");
            AddBlockOf5Holes(1634, 483.5, "right");
        }


        private static void AddBlockOf4Holes(double topOffset, double leftOffset, string addToSide="")
        {
            if (ConfiguredProduct != null)
            {
                double deepHole = 13;
                double shallowHole = 8;

                double holeDepth = (addToSide == "left") ? deepHole : (addToSide == "right") ? shallowHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset+64, leftOffset, 2.5, holeDepth);

                holeDepth = (addToSide == "left") ? shallowHole : (addToSide == "right") ? deepHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset+32, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset+64, leftOffset+32, 2.5, holeDepth);
            }
        }

        private static void AddBlockOf5Holes(double topOffset, double leftOffset, string addToSide = "")
        {
            if (ConfiguredProduct != null)
            {
                double deepHole = 13;
                double shallowHole = 8;

                double holeDepth = (addToSide == "left") ? deepHole : (addToSide == "right") ? shallowHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset, 2.5, holeDepth);
                
                holeDepth = (addToSide == "left") ? shallowHole : (addToSide == "right") ? deepHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset + 32, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 32, leftOffset + 32, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset + 32, 2.5, holeDepth);
            }
        }

        private static void AddBlockOf6Holes(double topOffset, double leftOffset, string addToSide = "")
        {
            if (ConfiguredProduct != null)
            {
                double deepHole = 13;
                double shallowHole = 8;

                double holeDepth = (addToSide == "left") ? deepHole : (addToSide == "right") ? shallowHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 32, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset, 2.5, holeDepth);
                
                holeDepth = (addToSide == "left") ? shallowHole : (addToSide == "right") ? deepHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset + 32, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 32, leftOffset + 32, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset + 32, 2.5, holeDepth);
            }
        }

        private static void AddBigBlockOf5Holes(double topOffset, double leftOffset , string addToSide = "")
        {
            if (ConfiguredProduct != null)
            {
                double deepHole = 13;
                double shallowHole = 8;

                double holeDepth = (addToSide == "top") ? deepHole : (addToSide == "bottom") ? shallowHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset + 64, 2.5, holeDepth);

                holeDepth = (addToSide == "top") ? shallowHole : (addToSide == "bottom") ? deepHole : deepHole;
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 32, leftOffset + 128, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset, 2.5, holeDepth);
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset + 64, leftOffset + 64, 2.5, holeDepth); 
            }
        }

        private static void AddHoleFromTopLeft(double topOffset, double leftOffset, double depth = 0) 
        {
            double holeDepth = depth > 0 ? depth : 13;
            if (ConfiguredProduct != null)
            {
                ConfiguredProduct.Features.AddHoleFromTopLeft(ApplyTarget.Back, topOffset, leftOffset, 2.5, holeDepth);
            }

        }
    }
}
