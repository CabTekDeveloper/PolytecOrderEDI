using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PolytecOrderEDI
{
    public partial class FrmPolytecColors : Form
    {
        private List<PolyColor> List_PolytecColors { get; set; } = [];
        private List<string> List_Finish { get; } = ["", "Ashgrain", "Createc", "Finegrain", "Gloss", "Legato", "Matera", "Matt", "Metallic", "Natura", "Ravine", "Raw", "Sanded", "Satin", "Sheen", "Smooth", "Texture", "Ultramatt", "Venette", "Woodgrain", "Woodmatt"];
        private List<string> List_Side { get; } = ["", "SS", "DS"];
        private List<string> List_Grain { get; } = ["", "0", "1"];


        public FrmPolytecColors()
        {
            InitializeComponent();
        }


        private void FrmPolytecColors_Load(object sender, EventArgs e)
        {
            List_PolytecColors = TablePolytecBoardColors.GetAllRecords();
            LoadDataGridView(List_PolytecColors);
            DgvPolytecColors.ClearSelection();
        }


        private void BtnAddNewColor_Click(object sender, EventArgs e)
        {
            LoadGroupBox(showGb: true, GbText: "Add");
        }


        private void BtnEditColor_Click(object sender, EventArgs e)
        {
            LoadGroupBox(showGb: true, GbText: "Update");
        }


        private void BtnDeleteColor_Click(object sender, EventArgs e)
        {
            LoadGroupBox(showGb: true, GbText: "Delete");
        }


        private void BtnConfirmModify_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;

            var materialCode = TxtMaterialCode.Text.Trim();
            var color = HelperMethods.TitleCaseString(TxtColor.Text.Trim());
            color = CustomRegex.WhiteSpaces().Replace(color, " ");

            var finish = CmbFinish.GetItemText(CmbFinish.SelectedItem) ?? "";
            var side = CmbSide.GetItemText(CmbSide.SelectedItem) ?? "";
            var grain = CmbGrain.GetItemText(CmbGrain.SelectedItem) ?? "";

            if (materialCode == "") errorMsg += $"MaterialCode cannot be empty.\n";
            if (CustomRegex.WhiteSpaces().IsMatch(materialCode)) errorMsg += $"MaterialCode cannot have spaces between.\n";
            if (color == "") errorMsg += $"Color cannot be empty.\n";
            if (finish == "") errorMsg += $"Finish cannot be empty.\n";
            if (grain == "") errorMsg += $"Grain cannot be empty.\n";

            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg, "Provide the missing details");
            }
            else
            {
                var newMaterialCode = materialCode;
                var newColorInfo = new PolyColor(newMaterialCode, color, finish, side, grain, $"{color} {finish} {side}");
                var modifyType = BtnConfirmModify.Text.Trim().ToLower();

                if (modifyType.Contains("Add", StringComparison.OrdinalIgnoreCase))
                {
                    if (TablePolytecBoardColors.CheckRecordExists(newMaterialCode))
                    {
                        MessageBox.Show("MaterialCode exists already in the EDI database.", "New Material color not added");
                    }
                    else
                    {
                        TablePolytecBoardColors.InsertRecord(newColorInfo);
                        List_PolytecColors = TablePolytecBoardColors.GetAllRecords();

                        LoadGroupBox(showGb: false);
                        TxtSearchColor.Text = newMaterialCode;
                        LoadDataGridView(FilterList_PolytecColors(newMaterialCode));
                        MessageBox.Show("Color added to EDI database.", "New Material color added");
                    }
                }
                else
                {
                    var selectedColorInfo = GetSelectedColorInfoFromDGV();
                    if (selectedColorInfo == null)
                    {
                        MessageBox.Show($"You haven't selected a color to {modifyType}.", "Select color.");
                    }
                    else
                    {
                        var originalMaterialCode = selectedColorInfo.MaterialCode;

                        if (modifyType.Contains("update", StringComparison.OrdinalIgnoreCase))
                        {
                            if (string.Equals(originalMaterialCode, newMaterialCode, StringComparison.OrdinalIgnoreCase))
                            {
                                TablePolytecBoardColors.UpdateRecord(originalMaterialCode, newColorInfo);
                                List_PolytecColors = TablePolytecBoardColors.GetAllRecords();
                                LoadGroupBox(showGb: false);
                                TxtSearchColor.Text = newMaterialCode;
                                LoadDataGridView(FilterList_PolytecColors(newMaterialCode));
                                MessageBox.Show("Material Color updated successfully in the EDI database.", "Material Color updated");
                            }
                            else
                            {
                                if (TablePolytecBoardColors.CheckRecordExists(newMaterialCode))
                                {
                                    MessageBox.Show("MaterialCode exists already in the EDI database.", "Material Color not updated");
                                }
                                else
                                {
                                    TablePolytecBoardColors.UpdateRecord(originalMaterialCode, newColorInfo);
                                    List_PolytecColors = TablePolytecBoardColors.GetAllRecords();
                                    LoadGroupBox(showGb: false);
                                    TxtSearchColor.Text = newMaterialCode;
                                    LoadDataGridView(FilterList_PolytecColors(newMaterialCode));
                                    MessageBox.Show("Material Color updated successfully in the EDI database.", "Material Color updated");
                                }
                            }
                        }

                        if (modifyType.Contains("delete", StringComparison.OrdinalIgnoreCase))
                        {
                            TablePolytecBoardColors.DeleteRecord(originalMaterialCode);
                            List_PolytecColors = TablePolytecBoardColors.GetAllRecords();
                            LoadGroupBox(showGb: false);
                            LoadDataGridView(FilterList_PolytecColors(TxtSearchColor.Text));
                            MessageBox.Show("Material Color deleted from EDI database.", "Color deleted");
                        }
                    }
                }


            }
        }


        private void BtnCancelModify_Click(object sender, EventArgs e)
        {
            LoadGroupBox(showGb: false);
            //ShowButtons(true);

        }


        private void TxtSearchColor_TextChanged(object sender, EventArgs e)
        {
            string searchText = TxtSearchColor.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText)) LoadDataGridView(List_PolytecColors);
            else LoadDataGridView(FilterList_PolytecColors(searchText));
        }


        private void DgvPolytecColors_SelectionChanged(object sender, EventArgs e)
        {
            if (GbModifyColor.Visible)
            {
                if (GbModifyColor.Text.Contains("update", StringComparison.OrdinalIgnoreCase) || GbModifyColor.Text.Contains("delete", StringComparison.OrdinalIgnoreCase))
                {
                    AddSelectedColorDetailsToGroupBoxControls();
                }
            }
        }


        private void LoadDataGridView(List<PolyColor> lstColors)
        {
            DgvPolytecColors.DataSource = null;
            DgvPolytecColors.DataSource = lstColors;
            //DgvPolytecColors.EditMode = DataGridViewEditMode.EditOnEnter;

            //Make the last colum fill the rest of the control's viewport
            if (DgvPolytecColors.ColumnCount > 0)
                DgvPolytecColors.Columns[^1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            LblTotalRecords.Text = $"Total records: {lstColors.Count}";
            this.ActiveControl = TxtSearchColor;
        }


        private void LoadGroupBox(bool showGb = false, string GbText = "")
        {
            try
            {
                GbModifyColor.Visible = showGb;
                GbModifyColor.Text = HelperMethods.TitleCaseString(GbText) + " Color";
                BtnConfirmModify.Text = HelperMethods.TitleCaseString(GbText);

                TxtMaterialCode.Text = "";
                TxtColor.Text = "";
                CmbFinish.DataSource = List_Finish;
                CmbSide.DataSource = List_Side;
                CmbGrain.DataSource = List_Grain;
                CmbFinish.SelectedIndex = 0;
                CmbSide.SelectedIndex = 0;
                CmbGrain.SelectedIndex = 0;

                //Enable/Disable controls
                TxtMaterialCode.Enabled = !GbText.Contains("delete", StringComparison.OrdinalIgnoreCase);
                TxtColor.Enabled = !GbText.Contains("delete", StringComparison.OrdinalIgnoreCase);
                CmbFinish.Enabled = !GbText.Contains("delete", StringComparison.OrdinalIgnoreCase);
                CmbSide.Enabled = !GbText.Contains("delete", StringComparison.OrdinalIgnoreCase);
                CmbGrain.Enabled = !GbText.Contains("delete", StringComparison.OrdinalIgnoreCase);

                if (GbModifyColor.Text.Contains("update", StringComparison.OrdinalIgnoreCase) || GbModifyColor.Text.Contains("delete", StringComparison.OrdinalIgnoreCase))
                {
                    AddSelectedColorDetailsToGroupBoxControls();
                }


                ShowButtons(!showGb);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private PolyColor? GetSelectedColorInfoFromDGV()
        {
            if (DgvPolytecColors.SelectedRows.Count > 0)
            {
                var selectedRowIndex = DgvPolytecColors.CurrentCell.RowIndex;
                var selectedRow = DgvPolytecColors.Rows[selectedRowIndex];
                var colorInfo = (PolyColor)selectedRow.DataBoundItem;
                return colorInfo;
            }
            else
            {
                return null;
            }
        }


        private void AddSelectedColorDetailsToGroupBoxControls()
        {
            var colorInfo = GetSelectedColorInfoFromDGV();
            if (colorInfo != null)
            {
                TxtMaterialCode.Text = colorInfo.MaterialCode;
                TxtColor.Text = colorInfo.Color;
                CmbFinish.SelectedIndex = CmbFinish.Items.IndexOf(colorInfo.Finish);
                CmbSide.SelectedIndex = CmbSide.Items.IndexOf(colorInfo.Side);
                CmbGrain.SelectedIndex = CmbGrain.Items.IndexOf(colorInfo.Grain);
            }
        }


        private void ShowButtons(bool showBtn = false)
        {
            BtnAddColor.Visible = showBtn;
            BtnUpdateColor.Visible = showBtn;
            BtnDeleteColor.Visible = showBtn;

            //BtnAddColor.Visible = showBtn;
            //BtnUpdateColor.Visible = showBtn;
            //BtnDeleteColor.Visible = showBtn;
        }


        private List<PolyColor> FilterList_PolytecColors(string filterString)
        {
            List<PolyColor> filteredList = [];
            foreach (var color in List_PolytecColors.ToList())
            {
                if (color.MaterialCode.StartsWith(filterString, StringComparison.OrdinalIgnoreCase))
                {
                    filteredList.Add(color);
                }

            }

            return filteredList;
        }

        private void BtnImportPolytecColors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form frmImportBoardColors = new FrmImportPolytecBoardColors();
            frmImportBoardColors.ShowDialog();
            List_PolytecColors = TablePolytecBoardColors.GetAllRecords();
            LoadDataGridView(List_PolytecColors);
            this.Show();

            //if (PolytecBoardColours.Import())
            //{
            //    MessageBox.Show(PolytecBoardColours.NewBoardColorsAddedMsg, "New board colours added to database!");
            //}
            //else
            //{
            //    MessageBox.Show(FileManager.FileImportMessage, "Import error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }
    }
}
