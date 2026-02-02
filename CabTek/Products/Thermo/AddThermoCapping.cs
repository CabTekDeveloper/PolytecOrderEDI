
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoCapping
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }
        
        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoCapping()
            {
                Quantity = Part.Quantity,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
                AdditionalInstructions = Part.AdditionalInstructions,
                Colour = Part.Colour,
                Finish = Part.Finish,
                Length = Part.Height == 2400 ? ThermoCappingLength.l2400mm : ThermoCappingLength.l3000mm,
  
            };

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
