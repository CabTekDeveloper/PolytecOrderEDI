using System.Globalization;

namespace PolytecOrderEDI
{
    static class ValidateVinylParts
    {
        private static string ErrorMessage { get; set; } = string.Empty;
        private static bool ExitImportation { get; set; } = false;

        public static void Reset()
        {
            ErrorMessage = string.Empty;
            ExitImportation = false;
        }

        static  ValidateVinylParts()
        {

        }

        public static bool Validate(List<VinylPart> LstProducts)
        {
            try
            {
                Reset(); 

                for (int i = 0; i < LstProducts.Count; i++)
                {
                    ValidateProduct(LstProducts[i], i == 0 );
                    if (ExitImportation) break;
                }

                if (ErrorMessage != "")
                { 
                    MessageBox.Show(ErrorMessage, "Fix the errors and import again.");
                    FileManager.FileImportMessage = $"Fix the errors and import again.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileManager.FileImportMessage = $"Error in validating Vinyl job";
                return false;
            }

        }


        //VALIDATION METHOD
        private static void ValidateProduct(VinylPart CurrentProduct, bool validateFieldsForConfiguredOrder = false)
        {
            string missingValueMessage = "";
            try
            {
                //Line no validation
                if (CurrentProduct.LineNo == 0)
                {
                    ErrorMessage = $"Missing Line No!\nLine No fields cannot be empty in the Excel form.\n";
                    ExitImportation = true;
                    return;
                }

                if (validateFieldsForConfiguredOrder)
                {
                    var RequestedDate = CurrentProduct.RequestedDate;

                    if (HelperMethods.CheckStringDateMatchesFormat(RequestedDate, GlobalVariable.DateFormat))
                    {
                        if (DateTime.ParseExact(RequestedDate, GlobalVariable.DateFormat, CultureInfo.InvariantCulture) <= DateTime.Today)
                        {
                            missingValueMessage += $"Requested date ( {RequestedDate} ) is not greater than today's date.\n";
                        }
                    }
                    else
                    {
                        missingValueMessage += $"Date ( {RequestedDate} ) is not in the format 'dd/mm/yyyy'.\n";
                    }

                    if (string.IsNullOrEmpty(CurrentProduct.PoNumber)) missingValueMessage += $"Missing Purchase order no.\n";
                    if (string.IsNullOrEmpty(CurrentProduct.Contact)) missingValueMessage += $"Missing Contact.\n";

                    GlobalVariable.PoNumber = CurrentProduct.PoNumber;
                    GlobalVariable.RequestedDate = CurrentProduct.RequestedDate;
                    GlobalVariable.Contact = CurrentProduct.Contact;

                    if (missingValueMessage != "")
                    {
                        ErrorMessage = $"LINE NO {CurrentProduct.LineNo} (Excel Form): \n{missingValueMessage}\n";
                        ExitImportation = true;
                        return;
                    }
                }

                if (CurrentProduct.ProductType == PRODUCTTYPE.None) missingValueMessage += $"Missing Product Type.\n";
                missingValueMessage += ProductValidation(CurrentProduct);
                missingValueMessage += QuantityValidation(CurrentProduct);
                missingValueMessage += HeightValidation(CurrentProduct);
                missingValueMessage += WidthValidation(CurrentProduct);
                if (CurrentProduct.Product == PRODUCT.DrawerFront && CurrentProduct.DfHeight == 0) missingValueMessage += $"Missing Drawer front height.\n";
                missingValueMessage += ThicknessValidation(CurrentProduct);
                missingValueMessage += PartNameValidation(CurrentProduct);
                missingValueMessage += EdgeLocationValidation(CurrentProduct);
                missingValueMessage += HandleSystemValidation(CurrentProduct);
                missingValueMessage += StyleProfileValidation(CurrentProduct);
                missingValueMessage += MultiPieceIdValidation(CurrentProduct);
                missingValueMessage += PressedSideValidation(CurrentProduct);
                if (CurrentProduct.EzeNo == 0) missingValueMessage += $"Missing Eze No.\n";

                missingValueMessage += DoorInfoValidation(CurrentProduct);
                missingValueMessage += DrawerInfoValidation(CurrentProduct);
                if (CurrentProduct.Product == PRODUCT.CutOut) missingValueMessage += CutoutMissingValues(CurrentProduct);
                if (CurrentProduct.Product == PRODUCT.RollerFrame) missingValueMessage += RollerFrameMissingValues(CurrentProduct);
                missingValueMessage += ReturnInfoValidation(CurrentProduct);
                missingValueMessage += FaceProfileValidation(CurrentProduct);
                missingValueMessage += EdgeMouldValidation(CurrentProduct);

                missingValueMessage += ColorValidation(CurrentProduct);
                missingValueMessage += FinishValidation(CurrentProduct);

                missingValueMessage += ContrastingEdgeInfoValidation(CurrentProduct);
                missingValueMessage += BarPanelInfoValidation(CurrentProduct);
                missingValueMessage += ProfileAndPanelSizeValidation(CurrentProduct);
                missingValueMessage += CustomHole1InfoValidation(CurrentProduct);

                //FINALLY, SET THE ERROR MESSAGE
                ErrorMessage += (missingValueMessage != "") ? $"LINE NO {CurrentProduct.LineNo} (Excel Form): {CurrentProduct.ProductType.ToString().ToUpper()} {CurrentProduct.Product.ToString().ToUpper()}\n{missingValueMessage}\n" : missingValueMessage;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        //PRODUCT VALIDATION
        private static string ProductValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.Product == PRODUCT.None)
            {
                errorMessage += $"Missing Product.\n";
            }
            else
            {
                if (CurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
                {
                    if (CurrentProduct.Product != PRODUCT.Door && CurrentProduct.Product != PRODUCT.DrawerFront)
                    {
                        errorMessage += $"Compact Laminate is available in Door and Drawers only.\n";
                    }
                }
            }
            return errorMessage;
        }


        //QUANTITY VALIDATION
        private static string QuantityValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.Quantity <= 0)
            {
                errorMessage += $"Quantity should be 1 or more.\n";
            }
            else
            {
                if (CurrentProduct.PartName == PARTNAME.Pair && CurrentProduct.Quantity % 2 != 0) errorMessage += $"For pair products, quantity cannot be odd no.\n";
            }
            return errorMessage;
        }

        //HEIGHT VALIDATION
        private static string HeightValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.Height < 1)
            {
                if (CurrentProduct.Product != PRODUCT.DrawerFront) errorMessage += $"Missing Height.\n";
            }
            else
            {
                if (CurrentProduct.Product == PRODUCT.RecessedRail)
                {
                    if (CurrentProduct.Height != 2400)
                    {
                        errorMessage += $"This product is available in 2400mm only.\n";
                    }
                }
                else if (CurrentProduct.Product == PRODUCT.HeatDeflectors)
                {
                    if (CurrentProduct.Height != 750 && CurrentProduct.Height != 2400)
                    {
                        errorMessage += $"This product is available in 750mm and 2400mm only.\n";
                    }
                }
            }

            return errorMessage;
        }

        //WIDTH VALIDATION
        private static string WidthValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.Product != PRODUCT.HeatDeflectors && CurrentProduct.Product != PRODUCT.HeatDeflectors)
            {
                if (CurrentProduct.Width == 0) errorMessage += $"Missing Width.\n";
            }
            return errorMessage;
        }

        //THICKNESS VALIDATION
        private static string ThicknessValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.Thickness == 0)
            {
                if (CurrentProduct.Product != PRODUCT.HeatDeflectors && CurrentProduct.Product != PRODUCT.HeatDeflectors) errorMessage += $"Missing Thickness.\n";
            }
            else
            {
                if(CurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
                {
                    if (CurrentProduct.Thickness != 5 && CurrentProduct.Thickness != 13) errorMessage += $"CompactLaminate is available in 5mm and 13mm (Thickness).\n";
                }
            }
            return errorMessage;

        }

        //PART NAME VALIDATION
        private static string PartNameValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.Product == PRODUCT.RecessedRail )
            {
                if (CurrentProduct.PartName != PARTNAME.C_Shaped && CurrentProduct.PartName != PARTNAME.L_Shaped) { errorMessage += $"Set Part Name to C Shapped or L Shapped.\n"; }
            }
        
            if (CurrentProduct.Product == PRODUCT.HeatDeflectors)
            {
                if (CurrentProduct.PartName != PARTNAME.Angled && CurrentProduct.PartName != PARTNAME.Straight) { errorMessage += $"Set Part Name to Angled or Straight.\n"; }
            }

            return errorMessage;
        }

        //EDGE LOCATION VALIDATION
        private static string EdgeLocationValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.EdgeLocation.Length == 0)
            {
                errorMessage += $"Set edge location to XXXX if it is not required.\n";
            }
            else if (CurrentProduct.EdgeLocation.Length < 4)
            {
                errorMessage += $"Edge Location ({CurrentProduct.EdgeLocation}) should have 4 characters.\n";
            }
            else if (CurrentProduct.EdgeLocation.Length > 4)
            {
                errorMessage += $"Edge Location ({CurrentProduct.EdgeLocation}) cannot have more than 4 characters.\n";
            }
            else 
            {
                if (!("THX".Contains(CurrentProduct.EdgeLocation[0]))) errorMessage += $"Top edge location is invalid. \n";
                if (!("BHX".Contains(CurrentProduct.EdgeLocation[1]))) errorMessage += $"Bottom edge location is invalid. \n";
                if (!("LHX".Contains(CurrentProduct.EdgeLocation[2]))) errorMessage += $"Left edge location is invalid. \n";
                if (!("RHX".Contains(CurrentProduct.EdgeLocation[3]))) errorMessage += $"Right edge location is invalid. \n";

                if (CurrentProduct.EdgeLocation.Contains('H') && CurrentProduct.HandleSystem == "None") errorMessage += $"Handle location provided in Edge Location {CurrentProduct.EdgeLocation} but missing handle system.\n";

                if (string.Equals(CurrentProduct.EdgeMould, "Square", StringComparison.OrdinalIgnoreCase))
                {
                    string edgeLocation = CurrentProduct.EdgeLocation.ToUpper();
                    if (edgeLocation.Contains('T') || edgeLocation.Contains('B') || edgeLocation.Contains('L') || edgeLocation.Contains('R'))
                    {
                        errorMessage += $"If edge mould is 'Square', set edge location to 'XXXX' or if you require handle set edge location to 'HXXX', 'XHXX', etc.\n";
                    }
                }
            }

            return errorMessage;
        }

        //HANDLE SYSTEM VALIDATION
        private static string HandleSystemValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (!string.Equals(CurrentProduct.HandleSystem, "None", StringComparison.OrdinalIgnoreCase))
            {
           
                if (!CurrentProduct.EdgeLocation.Contains('H'))
                {
                    errorMessage += $"You have selected {CurrentProduct.HandleSystem} handle but its location is not provided.\n";
                }
                else
                {
                    if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo|| CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
                    {
                        if (!string.Equals(CurrentProduct.EdgeMould, "Square", StringComparison.OrdinalIgnoreCase) && !string.Equals(CurrentProduct.EdgeMould, "3mm Pencil", StringComparison.OrdinalIgnoreCase))
                        {
                            errorMessage += $"Handle is available in edge moulds Square and 3mm Pencil only.\n";
                        }
                        else
                        {
                            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo)
                            {
                                if (!CustomValidation.IsThermoHandle(CurrentProduct.HandleSystem))
                                {
                                    errorMessage += $"Thermo products can only have Bronte handle.\n";
                                }
                            }

                            if (CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
                            {
                                if (!CustomValidation.IsCutAndRoutHandle(CurrentProduct.HandleSystem)) errorMessage += $"Cut&Rout products cannot have {CurrentProduct.HandleSystem} handle.\n"; 
                            }

                            if (CurrentProduct.Product == PRODUCT.GlassFrame)
                            {
                                if (!CurrentProduct.AdditionalInstructions.Contains(CurrentProduct.HandleSystem, StringComparison.OrdinalIgnoreCase))
                                {
                                    errorMessage += $"If you require handle on Galssframe doors, add it to the Additional Instructions field.\n";
                                }
                            }

                        }
                    }

                    if (CurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
                    {
                        if (!string.Equals(CurrentProduct.HandleSystem, "Shark Nose", StringComparison.OrdinalIgnoreCase))
                        {
                            errorMessage += $"Compact Laminate products can only have Shark Nose for handle system.\n";
                        }
                    }
                }
                

            }
            return errorMessage;
        }

        //STYLE PROFILE VALIDATION
        private static string StyleProfileValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo || CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
            {
                if (CurrentProduct.Product == PRODUCT.GlassFrame )
                { 
                    if (!(CustomValidation.IsValidGlassFrameDoorStyleProfile(CurrentProduct.StyleProfile))) errorMessage += $"Pick a valid Glass frame door style profile.\n";
                }
                else if (CurrentProduct.Product == PRODUCT.DrawerFront)
                {
                    if (!CustomValidation.IsDrawerBankStyleProfile(CurrentProduct.StyleProfile)) errorMessage += $"Pick a valid Drawer Bank style profile. \n";
                }

                //else if (Part.Product == PRODUCT.BarPanel)
                //{
                //    if (!CustomValidation.IsBarPanelStyleProfile(Part.StyleProfile)) errorMessage += $"Pick a valid Bar Panel style profile. \n";
                //}
            }
        
            return errorMessage;
        }

        //MULTIPIECE ID VALIDATION
        private static string MultiPieceIdValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.Product == PRODUCT.DrawerFront  && CurrentProduct.MultiPieceID == 0) errorMessage += $"Drawers should have Multi piece ID.\n";

            return errorMessage;
        }

        //PRESSED SIDE VALIDATION
        private static string PressedSideValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo)
            {
                if (CurrentProduct.Product != PRODUCT.HeatDeflectors && CurrentProduct.Product != PRODUCT.RecessedRail)
                {
                    if (CurrentProduct.PressedSides < 1)        errorMessage += $"Missing Pressed Side.\n";
                    else if (CurrentProduct.PressedSides > 2)   errorMessage += $"Pressed Side can only be 1 or 2.\n";
                }
            }
            else
            {
                if (CurrentProduct.PressedSides != 0) errorMessage += $"Leave the pressed side field empty.\n";
            }
            return errorMessage;
        }

        //DOOR INFO VALIDATION
        private static string DoorInfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.NumHoles > 0)
            {
                if (CurrentProduct.HingeCupInset == 0 && CurrentProduct.HingeBlockInset == 0 && CurrentProduct.BifoldHingeCupInset == 0)
                {
                    errorMessage += $"Hole positions are provided but missing insets.\n";
                }

                if(CurrentProduct.PartName != PARTNAME.Left_Leaf_770 && CurrentProduct.PartName != PARTNAME.Right_Leaf_770)
                {
                    if ((CurrentProduct.HingeCupInset > 0 || CurrentProduct.BifoldHingeCupInset > 0) && CurrentProduct.HingeType == HINGETYPE.None)
                    {
                        errorMessage += $"Hinge hole position and inset provided, but missing Hinge type (Blum or Hettich).\n";
                    }
                }

                if (CurrentProduct.PartName == PARTNAME.Left_Bifold || CurrentProduct.PartName == PARTNAME.Right_Bifold)
                {
                    if (CurrentProduct.HingeCupInset == 0) errorMessage += $"Hinge hole position provided, but missing Hinge Cup Inset.\n";
                    if (CurrentProduct.BifoldHingeCupInset == 0) errorMessage += $"Bifold door is missig Bifold Hinge Cup Inset.\n";
                }
            }
            else if (CurrentProduct.NumHoles == 0)
            {
                if (CurrentProduct.HingeCupInset != 0 || CurrentProduct.HingeBlockInset != 0 || CurrentProduct.BifoldHingeCupInset != 0)
                {
                    errorMessage += $"Hinge inset provided but missing hole positions.\n";
                }
            }

            if (CurrentProduct.Product == PRODUCT.PantryDoor )
            {
                if (CurrentProduct.MidRailHeight == 0 && (TableDoorStyles.GetDoorStyleNo(CurrentProduct.FaceProfile) > 1))
                {
                    errorMessage += $"Missing midrail height. (Or change it to a Door).\n";
                }
            }

            if (CurrentProduct.Product == PRODUCT.Door)
            {
                if (CurrentProduct.KickHeight > 0)      errorMessage += $"If you need Kick height, add that into Additional Instructions field.\n";
                if (CurrentProduct.MidRailHeight > 0)   errorMessage += $"Remove midrail height info or change Product to a PantryDoor";
            }

            return errorMessage;
        }

        //DRAWER INFO VALIDATION
        private static string DrawerInfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.DTYP1 != 0 || CurrentProduct.DTYP2 != 0)
            {
                if (CurrentProduct.HDIA == 0)   errorMessage += $"Missing HDIA.\n";
                if (CurrentProduct.INUP1 == 0)  errorMessage += $"Missing INUP.\n";
                if (CurrentProduct.LINS == 0)   errorMessage += $"Missing LINS.\n";
                if (CurrentProduct.RINS == 0)   errorMessage += $"Missing RINS.\n";
                if (CurrentProduct.DTYP2 != 0 && CurrentProduct.INUP2 == 0) errorMessage += $"2nd DTYP provided but missing 2nd INUP.\n";
            }

            return errorMessage;
        }

        //EDGE MOULD VALIDATION
        private static string EdgeMouldValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo || CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
            {
                if (CurrentProduct.Product == PRODUCT.RecessedRail  || CurrentProduct.Product == PRODUCT.HeatDeflectors )
                {
                    if (CurrentProduct.EdgeMould != "") errorMessage += $"This product cannot have Edge mould.\n";
                }
                else
                {
                    if (CurrentProduct.EdgeMould == "")
                    {
                        errorMessage += $"Missing Edge mould.\n";
                    }
                    else if (TableDoorStyles.GetDoorStyleNo(CurrentProduct.FaceProfile) == 1)
                    {
                        var edgeMould_db = TableDoorStyles.GetEdgeByStyleName(CurrentProduct.FaceProfile);
                        if (!string.Equals(CurrentProduct.EdgeMould, edgeMould_db, StringComparison.OrdinalIgnoreCase))
                        {
                            errorMessage += $"Face profile '{CurrentProduct.FaceProfile}' can only have '{edgeMould_db}' edge mould.\n";
                        }
                    }
                }
            }

            if (CurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
            {
                if (CurrentProduct.EdgeMould == "")
                {
                    errorMessage += $"Missing Edge mould.\n";
                }
                else if (!CustomValidation.IsValidCompactLaminateEdgeProfile(CurrentProduct.EdgeMould))
                {
                    errorMessage += $"{CurrentProduct.EdgeMould} is not a valid Compact Laminate edge profile.\n";
                }
            }

            return errorMessage;
        }

        //FACE PROFILE VALIDATION
        private static string FaceProfileValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo || CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
            {
                if (string.Equals(CurrentProduct.FaceProfile, "Bronte", StringComparison.OrdinalIgnoreCase))
                {
                    errorMessage += $"Bronte is not available as a face profile.\n";
                }
                else
                {
                    if (CurrentProduct.Product == PRODUCT.Panel || CurrentProduct.Product == PRODUCT.RecessedRail || CurrentProduct.Product == PRODUCT.HeatDeflectors )
                    {
                        if (CurrentProduct.FaceProfile != "") errorMessage += $"This product cannot have Face Profile.\n";
                    }
                    else
                    {
                        if (CurrentProduct.FaceProfile == "")
                        {
                            errorMessage += $"Missing Face Profile.\n";
                        }
                        else
                        {
                            if (!TableDoorStyles.CheckStyleNameExists(CurrentProduct.FaceProfile))
                            {
                                errorMessage += $"Face profile '{CurrentProduct.FaceProfile}' is not valid.\n";
                            }
                        }
                    }
                }
            }
            else
            {
                if (CurrentProduct.FaceProfile != "") errorMessage += $"This product cannot have Face Profile.\n";
            }

            return errorMessage;
        }

        //COLOR VALIDATION
        private static string ColorValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.Colour == "")
            {
                if (CurrentProduct.Product != PRODUCT.HeatDeflectors)
                {
                    errorMessage += $"Missing Colour.\n";
                }

            }
            else
            {
                if (CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
                {
                    if ((!string.Equals(CurrentProduct.Colour, "Cut and Rout S/S", StringComparison.OrdinalIgnoreCase)) && (!string.Equals(CurrentProduct.Colour, "Cut and Rout D/S", StringComparison.OrdinalIgnoreCase)))
                    {
                        errorMessage += $"Set color to 'Cut and Rout S/S' or 'Cut and Rout D/S'.\n";
                    }
                }
                else
                {
                    if ((string.Equals(CurrentProduct.Colour, "Cut and Rout S/S", StringComparison.OrdinalIgnoreCase)) || (string.Equals(CurrentProduct.Colour, "Cut and Rout D/S", StringComparison.OrdinalIgnoreCase)))
                    {
                        errorMessage += $"Color cannot be 'Cut and Rout S/S' or 'Cut and Rout D/S'.\n";
                    }
                }
            }

            return errorMessage;
        }

        //FINISH VALIDATION
        private static string FinishValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.Finish == "")
            {

                if (CurrentProduct.Product != PRODUCT.HeatDeflectors)
                {
                    errorMessage += $"Missing Finish.\n";
                }
            }
            else
            {
                if (CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout )
                {
                    if (!string.Equals(CurrentProduct.Finish, "None", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage += $"Set finish to 'None'.\n";
                    }
                }
                else
                {
                    if (string.Equals(CurrentProduct.Finish, "None", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage += $"Finish cannot be 'None'.\n";
                    }
                }
            }
            return errorMessage;
        }

        //CUTOUT PRODUCT VALIDATION
        private static string CutoutMissingValues(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.CutoutTopBorder == 0) errorMessage += $"Missing Top Border of the Cutout.\n";
            if (CurrentProduct.CutoutBottomBorder == 0) errorMessage += $"Missing Bottom Border of the Cutout.\n";
            if (CurrentProduct.CutoutLeftBorder == 0) errorMessage += $"Missing Left Border of the Cutout.\n";
            if (CurrentProduct.CutoutRightBorder == 0) errorMessage += $"Missing Right Border of the Cutout.\n";

            if ((CurrentProduct.CutoutTopBorder > 0 && CurrentProduct.CutoutTopBorder < 40) || (CurrentProduct.CutoutBottomBorder > 0 && CurrentProduct.CutoutBottomBorder < 40) || (CurrentProduct.CutoutLeftBorder > 0 && CurrentProduct.CutoutLeftBorder < 40) || (CurrentProduct.CutoutRightBorder > 0 && CurrentProduct.CutoutRightBorder < 40))
            {
                errorMessage += $"The width of the 1st Cutout borders cannot be less than 40mm.\n";
            }

            if ((CurrentProduct.Width - CurrentProduct.CutoutLeftBorder - CurrentProduct.CutoutRightBorder) < 125) errorMessage += $"The internal width of the 1st Cutout cannot be less than 125mm.\n";

            if (!CurrentProduct.HasCutout2)
            {
                if ((CurrentProduct.Height - CurrentProduct.CutoutTopBorder - CurrentProduct.CutoutBottomBorder) < 125) errorMessage += $"The internal height of the 1st Cutout cannot be less than 125mm.\n";
            }
            else
            {
                if (CurrentProduct.CutoutInternalHeight1 < 125) errorMessage += $"The internal height of the 1st Cutout cannot be less than 125mm.\n";

                if (CurrentProduct.CutoutLeftBorder2 == 0) errorMessage += $"Missing Left Border of the 2nd Cutout.\n";
                if (CurrentProduct.CutoutRightBorder2 == 0) errorMessage += $"Missing Right Border of the 2nd Cutout.\n";
                if (CurrentProduct.CutoutBottomBorder2 == 0) errorMessage += $"Missing Bottom Border of the 2nd Cutout.\n";

                if ((CurrentProduct.CutoutBottomBorder2 > 0 && CurrentProduct.CutoutBottomBorder2 < 40) || (CurrentProduct.CutoutLeftBorder2 > 0 && CurrentProduct.CutoutLeftBorder2 < 40) || (CurrentProduct.CutoutRightBorder2 > 0 && CurrentProduct.CutoutRightBorder2 < 40))
                {
                    errorMessage += $"The width of the 2nd Cutout borders cannot be less than 40mm.\n";
                }

                if ((CurrentProduct.Height - CurrentProduct.CutoutTopBorder - CurrentProduct.CutoutBottomBorder - CurrentProduct.CutoutInternalHeight1 - CurrentProduct.CutoutBottomBorder2) < 125) errorMessage += $"The internal height of the 2nd Cutout cannot be less than 125mm.\n";
                if ((CurrentProduct.Width - CurrentProduct.CutoutLeftBorder2 - CurrentProduct.CutoutRightBorder2) < 125) errorMessage += $"The internal width of the 2nd Cutout cannot be less than 125mm.\n";
            }
            return errorMessage;
        }

        //ROLERFRAME PRODUCT VALIDATION
        private static string RollerFrameMissingValues(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.CutoutTopBorder == 0) errorMessage += $"Missing Top Border of Roller Frame.\n";
            if (CurrentProduct.CutoutLeftBorder == 0) errorMessage += $"Missing Left Border of Roller Frame.\n";
            if (CurrentProduct.CutoutRightBorder == 0) errorMessage += $"Missing Right Border of Roller Frame.\n";

            if ((CurrentProduct.CutoutTopBorder > 0 && CurrentProduct.CutoutTopBorder < 40) || (CurrentProduct.CutoutLeftBorder > 0 && CurrentProduct.CutoutLeftBorder < 40) || (CurrentProduct.CutoutRightBorder > 0 && CurrentProduct.CutoutRightBorder < 40))
            {
                errorMessage += $"The width of the Roller Frame borders cannot be less than 40mm.\n";
            }

            if ((CurrentProduct.Height - CurrentProduct.CutoutTopBorder) < 125) errorMessage += $"The internal height of the Roller Frame cannot be less than 125mm.\n";
            if ((CurrentProduct.Width - CurrentProduct.CutoutLeftBorder - CurrentProduct.CutoutRightBorder) < 125) errorMessage += $"The internal width of Roller Frame cannot be less than 125mm.\n";

            return errorMessage;
        }

        //RETURN INFO VALIDATION
        private static string ReturnInfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.EdgeMould != "" && CurrentProduct.Thickness != 0)
            {
                if (CurrentProduct.Return1Edge != "" || CurrentProduct.Return2Edge != "")
                {
                    if (CurrentProduct.ProductType != PRODUCTTYPE.Thermo)
                    {
                        errorMessage += $"Returns are only available in Thermo Products.\n";
                    }
                    else if (CurrentProduct.Product !=PRODUCT.Panel && CurrentProduct.Product != PRODUCT.Door)
                    {
                        errorMessage += $"Returns are only available in Doors and Panels.\n";
                    }
                    else if (!string.Equals(CurrentProduct.EdgeMould, "Square", StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage += $"Returns are only available in Square Edge Profile.\n";
                    }
                    else if (string.Equals(CurrentProduct.Return1Edge, CurrentProduct.Return2Edge, StringComparison.OrdinalIgnoreCase))
                    {
                        errorMessage += $"Return 1 and Return 2 cannot be applied to same edge ({CurrentProduct.Return1Edge}).\n";
                    }
                    else
                    {
                        if (CurrentProduct.Return1Edge != "")
                        {
                            if ((CurrentProduct.Return1Thickness != CurrentProduct.Thickness) && (CurrentProduct.Return1Thickness != CurrentProduct.Thickness * 2)) errorMessage += $"For a {CurrentProduct.Thickness}mm {CurrentProduct.Product}, return 1 Thickness should be {CurrentProduct.Thickness} or {2 * CurrentProduct.Thickness}.\n";
                            if (CurrentProduct.Return1Width < 100) errorMessage += $"Minimum Return 1 Width is 100mm.\n";
                        }

                        if (CurrentProduct.Return2Edge != "")
                        {
                            if ((CurrentProduct.Return2Thickness != CurrentProduct.Thickness) && (CurrentProduct.Return2Thickness != CurrentProduct.Thickness * 2)) errorMessage += $"For a {CurrentProduct.Thickness}mm {CurrentProduct.Product}, return 2 Thickness should be {CurrentProduct.Thickness} or {2 * CurrentProduct.Thickness}.\n";
                            if (CurrentProduct.Return2Width < 100) errorMessage += $"Minimum Return 2 Width is 100mm.\n";

                            if ((CurrentProduct.Return1Edge == "left" || CurrentProduct.Return1Edge == "right") && (CurrentProduct.Return2Edge == "top" || CurrentProduct.Return2Edge == "bottom"))
                            {
                                errorMessage += $"{CurrentProduct.Return1Edge} Return cannot have {CurrentProduct.Return2Edge} Return.\n";
                            }
                            else if ((CurrentProduct.Return1Edge == "top" || CurrentProduct.Return1Edge == "bottom") && (CurrentProduct.Return2Edge == "left" || CurrentProduct.Return2Edge == "right"))
                            {
                                errorMessage += $"{CurrentProduct.Return1Edge} Return cannot have {CurrentProduct.Return2Edge} Return.\n";
                            }
                        }
                    }
                }


            }
            return errorMessage;
        }

        //CONTRASTING EDGE INFO VALIDATION
        private static string ContrastingEdgeInfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo || CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout || CurrentProduct.ProductType == PRODUCTTYPE.CompactLaminate)
            {
                if (CurrentProduct.ContrastingEdgeColour != "") errorMessage += $"This product cannot have Contrasting Edge Colour.\n";
                if (CurrentProduct.ContrastingEdgeFinish != "") errorMessage += $"This product cannot have Contrasting Edge Finish.\n";
            }
            else
            {
                if (CurrentProduct.ContrastingEdgeColour == "" && CurrentProduct.ContrastingEdgeFinish != "") errorMessage += $"Contrasting Edge Finish provided but missing Contrasting Edge Colour.\n";
                if (CurrentProduct.ContrastingEdgeColour != "" && CurrentProduct.ContrastingEdgeFinish == "") errorMessage += $"Contrasting Edge Colour provided but missing Contrasting Edge Finish.\n";
            }

            return errorMessage;
        }


        


        //BAR PANEL INFO VALIDATION
        private static string BarPanelInfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";

            double[] barPanelSizes = 
            [
                CurrentProduct.Profile1Size, CurrentProduct.Profile2Size, CurrentProduct.Profile3Size, CurrentProduct.Profile4Size, 
                CurrentProduct.Profile5Size, CurrentProduct.Profile6Size, CurrentProduct.Profile7Size, CurrentProduct.Profile8Size
            ];

            if (CurrentProduct.Product == PRODUCT.BarPanel)
            {
                if( CurrentProduct.NumberOfPanels == 0) errorMessage += $"Missing Number of Panels.\n";
                if( CurrentProduct.NumberOfPanels == 1) errorMessage += $"Number of Panels must be 2 or more.\n";

                if (CurrentProduct.EvenlySizedProfiles)
                {
                    if (!barPanelSizes.All(size => size == 0)) errorMessage += $"Remove the bar panel profile sizes if you want Evenly Sized Profiles.\n";
                }
                else
                {
                    double totalBarPanelSize = 0;
                    string missingSizeMsg = string.Empty;

                    for (int i = 0; i < CurrentProduct.NumberOfPanels; i++)
                    {
                        totalBarPanelSize += barPanelSizes[i];
                        if (barPanelSizes[i] == 0) missingSizeMsg += $"Missing Profile {i+1} Size. You opted to have different sized profiles.\n";
                    }

                    if (missingSizeMsg == string.Empty && totalBarPanelSize != CurrentProduct.Width) errorMessage += $"The Width ({CurrentProduct.Width}) of Bar Panel does not match the sum of bar panel sizes ({totalBarPanelSize}).\n";
     
                    errorMessage += missingSizeMsg;

                    for (int i = CurrentProduct.NumberOfPanels; i < barPanelSizes.Length; i++)
                    {
                        if (barPanelSizes[i] != 0) errorMessage += $"Remove Profile {i+1} Size. Number of Panels picked is {CurrentProduct.NumberOfPanels}.\n";
                    }

                }
            }


            return errorMessage;
        }

        //PROFILE AND PANEL SIZE VALIDATION
        private static string ProfileAndPanelSizeValidation(VinylPart CurrentProduct)
        {
            string errorMessage = "";
            if (CurrentProduct.ProductType == PRODUCTTYPE.Thermo || CurrentProduct.ProductType == PRODUCTTYPE.CutAndRout)
            {
                if (!string.IsNullOrEmpty(CurrentProduct.FaceProfile))
                {
                    DoorStyleDetails? doorStyleDetails = TableDoorStyles.GetDoorStyleDetails(CurrentProduct.FaceProfile);
                    if (doorStyleDetails != null)
                    {
                        if (CurrentProduct.Product == PRODUCT.DrawerFront)
                        {
                            if (CurrentProduct.DfHeight < doorStyleDetails.MinHeight) errorMessage += $"The min height for Face Profile {CurrentProduct.FaceProfile} is {doorStyleDetails.MinHeight}mm\n";
                        }
                        else
                        {
                            if (CurrentProduct.Height < doorStyleDetails.MinHeight) errorMessage += $"The min height for Face Profile {CurrentProduct.FaceProfile} is {doorStyleDetails.MinHeight}mm.\n";
                        }

                        if (CurrentProduct.Width < doorStyleDetails.MinWidth) errorMessage += $"The min width for Face Profile {CurrentProduct.FaceProfile} is {doorStyleDetails.MinWidth}mm.\n";
                    }
                }
            }

            return errorMessage;
        }


        //CUSTOM HOLE1 INFO VALIDATION
        private static string CustomHole1InfoValidation(VinylPart CurrentProduct)
        {
            string errorMessage = string.Empty;
            double[] customHole1Values = [CurrentProduct.CustomHole1LeftInset, CurrentProduct.CustomHole1TopInset, CurrentProduct.CustomHole1HDIA, CurrentProduct.CustomHole1Depth];
            if (customHole1Values.Any(value=>value>0))
            {
                if (customHole1Values.Any(value => value == 0)) errorMessage += $"You haven't provided all the values of Custom Hole1 Info.\n";
            }

            return errorMessage;
        }


    }
}


