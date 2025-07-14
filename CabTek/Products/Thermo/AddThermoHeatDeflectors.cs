
using BorgEdi.Enums;
using BorgEdi.Models;

namespace PolytecOrderEDI
{
    static class AddThermoHeatDeflectors
    {
        private static VinylPart Part { get; set; } = new();

        public static void Add(VinylPart part)
        {
            Part = part;
            CreateProduct();
        }
        
        private static void CreateProduct( )
        {
            var ConfiguredProduct = new ThermoHeatDeflector()
            {
                Quantity = Part.Quantity,
                Length = (int)Part.Height,
                Profile = Part.PartName.ToString(), //The partname is the profile of this product: Angled or Straight
                AdditionalInstructions = Part.AdditionalInstructions,
                LabelReference = new LabelReference() { Style = LabelStyle.Text, Reference = $"EzeNo: {Part.EzeNo}" },
            };

            PolytecConfiguredOrder.Order.AddProduct(ConfiguredProduct);
        }

    }
}
