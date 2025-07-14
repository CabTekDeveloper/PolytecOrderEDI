
namespace PolytecOrderEDI
{
    static class BuildAndAddProducts_ICB
    {
 
        public static bool Build()
        {
            List<Cabinet> Cabinets = ICB.Cabinets;
            try
            {
                foreach (var cabinet in Cabinets)
                {
                    //AddDrillings Parts
                    foreach (var part in cabinet.Parts)
                    {
                        switch(part.Product)
                        {
                            case PRODUCT.Door:
                            case PRODUCT.Panel:
                                AddDecorativeDoor.Add(part);
                                break;

                            case PRODUCT.GlassFrame:
                                AddDecorativeGlassFrame.Add(part);  
                                break;

                            case PRODUCT.CutOut:
                                AddDecorativeCutout.Add(part);
                                break;

                            case PRODUCT.RollerFrame:
                                AddDecorativeRollerFrame.Add(part);
                                break;

                            default:
                                break;
                        }

                    }

                    //AddDrillings Drawer Bank
                    if (cabinet.StdDrawerBank.Count > 0)
                    {
                        AddDecorativeDrawers.Add(cabinet.StdDrawerBank);
                    }
                    else
                    {
                        if (cabinet.LeftDrawerBank.Count > 0) AddDecorativeDrawers.Add(cabinet.LeftDrawerBank);
                        if (cabinet.RightDrawerBank.Count > 0) AddDecorativeDrawers.Add(cabinet.RightDrawerBank);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }


        }
    }
}
