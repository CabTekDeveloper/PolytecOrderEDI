using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolytecOrderEDI
{
    public partial class FrmImportPolytecBoardColors : Form
    {
        public FrmImportPolytecBoardColors()
        {
            InitializeComponent();
        }

        private void FrmImportPolytecBoardColors_Load(object sender, EventArgs e)
        {
            LblTitleInfo.Text = string.Empty;
            LblImportMessage.Text = string.Empty;

            LblTitleInfo.Text =  $"* The excel file you are importing the colors from has to be in the template shown below.\n";
            LblTitleInfo.Text += $"* Only the new colours which are not in the EDI database will be imported.";

        }



        private void BtnImportBoardColors_Click(object sender, EventArgs e)
        {
            BtnViewImportedColors.Visible = false;
            LblImportMessage.Text = "Importing Colors";
            LblImportMessage.ForeColor = Color.Blue;
            BtnImportBoardColors.Visible = false;
            BtnClose.Visible = false;

            if (PolytecBoardColours.Import())
            {
                if (PolytecBoardColours.NewColoursAddedCount > 0)
                {
                    int newColorCount = PolytecBoardColours.NewColoursAddedCount;
                    LblImportMessage.Text = $"Added {newColorCount} {(newColorCount > 1 ? "colours" : "colour")} to EDI database.";
                    LblImportMessage.ForeColor = Color.Blue;
                    BtnViewImportedColors.Visible = true;
                }
                else
                {
                    LblImportMessage.Text = "No new colours were imported.";
                    LblImportMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                //MessageBox.Show(FileManager.FileImportMessage, "Import error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LblImportMessage.Text = FileManager.FileImportMessage;
                LblImportMessage.ForeColor = Color.Red;
            }

            BtnImportBoardColors.Visible = true;
            BtnClose.Visible = true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnViewImportedColors_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PolytecBoardColours.NewBoardColorsAddedMsg, "New board colours added to database!");
        }
    }
}
