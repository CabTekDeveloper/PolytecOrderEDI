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
    public partial class FrmImportedCabinetParts : Form
    {
        private List<Cabinet> Cabinets { get; set; } = [];
        private int TotalCabinets { get; set; } =0;
        private int TotalParts { get; set; } = 0;
        private int SelectedCabinetNumber { get; set; } = 0;
        private int SelectedCabinetTotalParts { get; set; } = 0;
        private int SelectedCabinetIndex { get; set; } = 0;
        private List<CabinetPart_Truncated> DgvDataSource { get; set; } = [];

        public FrmImportedCabinetParts()
        {
            InitializeComponent();
        }


        private void FrmAddAdditionalInstructions_Load(object sender, EventArgs e)
        {   
            TotalCabinets = ICB.Cabinets.Count;
            TotalParts = Workout_TotalParts(ICB.Cabinets);
            LblJobImportedJobStats.Text = $"Total cabinets: {TotalCabinets}     Total parts: {TotalParts}";

            Cabinets = ICB.Cabinets;
            CmbCabinetName.DataSource = Workout_CabinetNames();
            CmbCabinetName.SelectedIndex = SelectedCabinetIndex;
            BtnPrevious.Text = "\u2190";
            BtnNext.Text = "\u2192";
            LoadDataGridView();
        }


        private void CmbCabinetName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCabinetIndex = CmbCabinetName.SelectedIndex;
            LoadDataGridView();
        }


        private void DgvCabinetParts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var updatedPartInfo = GetSelectedPartInfoFromDGV();
            ApplyUpdateToICBList_Cabinets(updatedPartInfo);
        }


        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (SelectedCabinetIndex > 0)
            {
                SelectedCabinetIndex -= 1;
                CmbCabinetName.SelectedIndex = SelectedCabinetIndex;
                LoadDataGridView();
            }

        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (SelectedCabinetIndex < Cabinets.Count - 1)
            {
                SelectedCabinetIndex += 1;
                CmbCabinetName.SelectedIndex = SelectedCabinetIndex;
                LoadDataGridView();
            }
        }

        //Helper Methods
        private void LoadDataGridView()
        {
            Workout_DgvDataSource();
            DgvCabinetParts.DataSource = null;
            DgvCabinetParts.DataSource = DgvDataSource;
            //DgvCabinetParts.EditMode = DataGridViewEditMode.EditOnEnter;

            if (DgvCabinetParts.ColumnCount > 0)
            {
                //Make the last colum fill the rest of the control's viewport
                DgvCabinetParts.Columns[^1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DgvCabinetParts.Columns[^1].ReadOnly = true;

                //Make AdditionalInstructions column editable
                foreach (DataGridViewColumn col in DgvCabinetParts.Columns)
                {
                    col.ReadOnly = !string.Equals(col.Name, "AdditionalInstructions", StringComparison.OrdinalIgnoreCase);
                }
            }

            LblCabinetStats.Text = $"Cabinet: {SelectedCabinetNumber}    Toal parts: {SelectedCabinetTotalParts}";

            this.ActiveControl = DgvCabinetParts;
            DgvCabinetParts.ClearSelection();
        }

        private CabinetPart_Truncated GetSelectedPartInfoFromDGV()
        {
            var selectedRowIndex = DgvCabinetParts.CurrentCell.RowIndex;
            var selectedRow = DgvCabinetParts.Rows[selectedRowIndex];
            var partInfo = (CabinetPart_Truncated)selectedRow.DataBoundItem;

            return partInfo;
        }

        private List<string> Workout_CabinetNames()
        {
            List<string> cabinetNames = [];
            foreach (var cabinet in Cabinets) cabinetNames.Add(cabinet.CabinetName);

            return cabinetNames;
        }

        private int Workout_TotalParts(List<Cabinet> cabs)
        {
            int totalParts = 0;
            foreach (var cabinet in cabs)
            {
                foreach (var part in cabinet.Parts) totalParts++;
                foreach (var part in cabinet.StdDrawerBank) totalParts++;
                foreach (var part in cabinet.LeftDrawerBank) totalParts++;
                foreach (var part in cabinet.RightDrawerBank) totalParts++;
            }

            return totalParts;
        }

        private void Workout_DgvDataSource()
        {
            var selectedCabinet = Cabinets[SelectedCabinetIndex];
            List<CabinetPart_Truncated> tempParts = [];

            foreach (var part in selectedCabinet.Parts) tempParts.Add(new CabinetPart_Truncated(part));
            foreach (var part in selectedCabinet.StdDrawerBank) tempParts.Add(new CabinetPart_Truncated(part));
            foreach (var part in selectedCabinet.LeftDrawerBank) tempParts.Add(new CabinetPart_Truncated(part));
            foreach (var part in selectedCabinet.RightDrawerBank) tempParts.Add(new CabinetPart_Truncated(part));

            SelectedCabinetNumber = selectedCabinet.CabinetNumber;
            SelectedCabinetTotalParts = tempParts.Count;
            DgvDataSource = tempParts;
        }

        private void ApplyUpdateToICBList_Cabinets(CabinetPart_Truncated updatedpart)
        {
            var selectedCabinet = Cabinets[SelectedCabinetIndex];
            bool updatedVal = false;

            for (int i = 0; i < selectedCabinet.Parts.Count; i++)
            {
                var part = selectedCabinet.Parts[i];
                if (part.PartNumber == updatedpart.PartNumber)
                {
                    selectedCabinet.Parts[i].AdditionalInstructions = updatedpart.AdditionalInstructions;
                    updatedVal = true;
                    break;
                }
            }

            if (!updatedVal)
            {
                for (int i = 0; i < selectedCabinet.StdDrawerBank.Count; i++)
                {
                    var part = selectedCabinet.StdDrawerBank[i];
                    if (part.PartNumber == updatedpart.PartNumber)
                    {
                        selectedCabinet.StdDrawerBank[i].AdditionalInstructions = updatedpart.AdditionalInstructions;
                        updatedVal = true;
                        break;
                    }
                }
            }

            if (!updatedVal)
            {
                for (int i = 0; i < selectedCabinet.LeftDrawerBank.Count; i++)
                {
                    var part = selectedCabinet.LeftDrawerBank[i];
                    if (part.PartNumber == updatedpart.PartNumber)
                    {
                        selectedCabinet.LeftDrawerBank[i].AdditionalInstructions = updatedpart.AdditionalInstructions;
                        updatedVal = true;
                        break;
                    }
                }
            }

            if (!updatedVal)
            {
                for (int i = 0; i < selectedCabinet.RightDrawerBank.Count; i++)
                {
                    var part = selectedCabinet.RightDrawerBank[i];
                    if (part.PartNumber == updatedpart.PartNumber)
                    {
                        selectedCabinet.RightDrawerBank[i].AdditionalInstructions = updatedpart.AdditionalInstructions;
                        updatedVal = true;
                        break;
                    }
                }
            }

            //Update the ICB Cabinets List and the Local Cabinets List
            ICB.Cabinets[SelectedCabinetIndex] = selectedCabinet;
            Cabinets = ICB.Cabinets;

            //Rebuild the Configured Order.
            PolytecConfiguredOrder.BuildAndAddProducts();
        }

        
    }
}
