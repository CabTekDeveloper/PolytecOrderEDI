
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoRecessedRail
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }
        
        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoRecessedRail()
            {
                Quantity = Part.Quantity,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.Colour,
                Finish = Part.Finish,
                Length = (int)Part.Height,
                Profile = Part.PartName == PARTNAME.C_Shaped ? "C Shaped" : "L Shaped"  
            };

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
