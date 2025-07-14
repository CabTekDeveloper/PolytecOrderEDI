

namespace PolytecOrderEDI
{
    static class BuildAndAddProducts_Vinyl
    {
        public static bool Build()
        {
            try
            {
                List<VinylPart> LstProducts = VinylJob.LstProducts;
                List<VinylPart> DrawerBankPieces = [];

                for (int i = 0; i < LstProducts.Count; i++)
                {
                    var objCurrentProduct = LstProducts[i];
                    PRODUCT currentProduct = objCurrentProduct.Product;

                    //Compact Laminate
                    if (objCurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
                    {
                        switch (currentProduct)
                        {
                            case PRODUCT.Door:
                                AddCompactLaminateDoor.Add(objCurrentProduct);
                                break;

                            case PRODUCT.DrawerFront:
                                DrawerBankPieces.Add(objCurrentProduct);

                                if (i == LstProducts.Count - 1)
                                {
                                    AddCompactLaminateDrawers.Add(DrawerBankPieces);
                                    DrawerBankPieces.Clear();
                                }
                                else
                                {
                                    int curMultiPieceID = objCurrentProduct.MultiPieceID;

                                    var objNextProduct = LstProducts[i + 1];
                                    int nextMultiPieceID = objNextProduct.MultiPieceID;
                                    PRODUCT nextProduct = objNextProduct.Product;

                                    if (curMultiPieceID != nextMultiPieceID || currentProduct != nextProduct)
                                    {
                                        AddCompactLaminateDrawers.Add(DrawerBankPieces);
                                        DrawerBankPieces.Clear();
                                    }
                                }
                                break;
                        }
                    }

                    // Thermno and Cut&Rout products
                    else if (objCurrentProduct.ProductType == PRODUCTTYPE.Thermo || objCurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
                    {

                        switch (currentProduct)
                        {
                            case PRODUCT.HeatDeflectors:
                                AddThermoHeatDeflectors.Add(objCurrentProduct); 
                                break;

                            case PRODUCT.BarPanel:
                                AddThermoBarPanel.Add(objCurrentProduct);
                                break;

                            case PRODUCT.Door:
                                AddThermoDoor.Add(objCurrentProduct);
                                break;

                            case PRODUCT.Panel:
                                AddThermoPanel.Add(objCurrentProduct);
                                break;

                            case PRODUCT.DrawerFront:
                                DrawerBankPieces.Add(objCurrentProduct);

                                if (i == LstProducts.Count - 1)
                                {
                                    AddThermoDrawers.Add(DrawerBankPieces);
                                    DrawerBankPieces.Clear();
                                }
                                else
                                {
                                    int curMultiPieceID = objCurrentProduct.MultiPieceID;

                                    var objNextProduct = LstProducts[i + 1];
                                    int nextMultiPieceID = objNextProduct.MultiPieceID;
                                    PRODUCT nextProduct = objNextProduct.Product;

                                    if (curMultiPieceID != nextMultiPieceID || currentProduct != nextProduct)
                                    {
                                        AddThermoDrawers.Add(DrawerBankPieces);
                                        DrawerBankPieces.Clear();
                                    }
                                }
                                break;

                            case PRODUCT.GlassFrame:
                                AddThermoGlassFrame.Add(objCurrentProduct);
                                break;

                            case PRODUCT.CutOut:
                                AddThermoCutout.Add(objCurrentProduct);
                                break;

                            case PRODUCT.RollerFrame:
                                AddThermoRollerFrame.Add(objCurrentProduct);
                                break;

                            case PRODUCT.RecessedRail:
                                AddThermoRecessedRail.Add(objCurrentProduct);
                                break;

                            case PRODUCT.PantryDoor:
                                AddThermoPantryDoor.Add(objCurrentProduct);
                                break;

                            default:
                                break;
                        }
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


