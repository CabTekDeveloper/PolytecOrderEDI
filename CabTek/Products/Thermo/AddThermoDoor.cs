
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoDoor
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part; 

            if (Part.PartName == PARTNAME.Pair)
            {
                Part.Quantity /= 2;
                Part.PartName = PARTNAME.Left;  //Add left door
                CreateProduct();

                Part.PartName = PARTNAME.Right; //Add right door
                CreateProduct();

                // Change properties to the initial values
                Part.PartName = PARTNAME.Pair;
                Part.Quantity *=2;
            }
            else CreateProduct();
            
        }
        //End of Method

        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoDoor()
            {
                Quantity =  Part.Quantity,
                Height = (decimal) Part.Height,
                Width = (decimal) Part.Width,
                Thickness = Part.Thickness.ToString(),
                EdgeLocation = Part.EdgeLocation,
                HandleSystem = Part.HandleSystem,
                PressedSides = Part.PressedSides,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                EdgeMould = Part.EdgeMould,
                Profile = Part.FaceProfile,
                Colour = Part.Colour,
                Finish = Part.Finish,
                
            };

            if (Part.Return1Edge != "" || Part.Return2Edge != "")
            {
                ConfiguredProduct.Returns = [];
                if (Part.Return1Edge != "")
                {
                    ConfiguredProduct.Returns.Add(new Return() { Edge = HelperMethods.GetReturnPanelEdge(Part.Return1Edge), Thickness = Part.Return1Thickness, Width = Part.Return1Width });
                }
                if (Part.Return2Edge != "")
                {
                    ConfiguredProduct.Returns.Add(new Return() { Edge = HelperMethods.GetReturnPanelEdge(Part.Return2Edge), Thickness = Part.Return2Thickness, Width = Part.Return2Width });
                }  
            }

            // Added 22-01-2026 : The if block is a temp bock of code to add drillings to Revego Doors. The drilings will be out if the door size is changed
            if (Part.PartName.ToString().Contains("revego", StringComparison.OrdinalIgnoreCase))
            {
                TempRevegoDoorDrillings.AddDrillings(ConfiguredProduct, Part);
            }

            CustomDrillingOnProduct.AddDrillings(configuredProduct: ConfiguredProduct, vinyl_part: Part);  //AddDrillings drilling
            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);   
        }




    }


}


