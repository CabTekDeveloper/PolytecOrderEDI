namespace PolytecOrderEDI
{
    partial class FrmImportedCabinetParts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportedCabinetParts));
            CmbCabinetName = new ComboBox();
            DgvCabinetParts = new DataGridView();
            cabinetPartBindingSource = new BindingSource(components);
            BtnPrevious = new Button();
            BtnNext = new Button();
            LblJobImportedJobStats = new Label();
            LblCabinetStats = new Label();
            ((System.ComponentModel.ISupportInitialize)DgvCabinetParts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cabinetPartBindingSource).BeginInit();
            SuspendLayout();
            // 
            // CmbCabinetName
            // 
            CmbCabinetName.BackColor = Color.WhiteSmoke;
            CmbCabinetName.FlatStyle = FlatStyle.System;
            CmbCabinetName.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CmbCabinetName.ForeColor = Color.Black;
            CmbCabinetName.FormattingEnabled = true;
            CmbCabinetName.Location = new Point(12, 24);
            CmbCabinetName.Name = "CmbCabinetName";
            CmbCabinetName.Size = new Size(422, 25);
            CmbCabinetName.TabIndex = 2;
            CmbCabinetName.TabStop = false;
            CmbCabinetName.SelectedIndexChanged += CmbCabinetName_SelectedIndexChanged;
            // 
            // DgvCabinetParts
            // 
            DgvCabinetParts.AllowUserToAddRows = false;
            DgvCabinetParts.AllowUserToDeleteRows = false;
            DgvCabinetParts.AllowUserToResizeRows = false;
            DgvCabinetParts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DgvCabinetParts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvCabinetParts.BackgroundColor = SystemColors.Control;
            DgvCabinetParts.BorderStyle = BorderStyle.None;
            DgvCabinetParts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DgvCabinetParts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DgvCabinetParts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvCabinetParts.Cursor = Cursors.IBeam;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DgvCabinetParts.DefaultCellStyle = dataGridViewCellStyle2;
            DgvCabinetParts.EnableHeadersVisualStyles = false;
            DgvCabinetParts.GridColor = Color.DarkGray;
            DgvCabinetParts.Location = new Point(12, 85);
            DgvCabinetParts.Margin = new Padding(10);
            DgvCabinetParts.MultiSelect = false;
            DgvCabinetParts.Name = "DgvCabinetParts";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.IndianRed;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            DgvCabinetParts.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DgvCabinetParts.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.Window;
            DgvCabinetParts.RowsDefaultCellStyle = dataGridViewCellStyle4;
            DgvCabinetParts.RowTemplate.Height = 27;
            DgvCabinetParts.ScrollBars = ScrollBars.Vertical;
            DgvCabinetParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvCabinetParts.Size = new Size(1210, 403);
            DgvCabinetParts.TabIndex = 42;
            DgvCabinetParts.TabStop = false;
            DgvCabinetParts.CellValueChanged += DgvCabinetParts_CellValueChanged;
            // 
            // cabinetPartBindingSource
            // 
            cabinetPartBindingSource.DataSource = typeof(CabinetPart);
            // 
            // BtnPrevious
            // 
            BtnPrevious.FlatAppearance.BorderColor = Color.White;
            BtnPrevious.FlatAppearance.BorderSize = 0;
            BtnPrevious.FlatStyle = FlatStyle.Popup;
            BtnPrevious.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            BtnPrevious.Location = new Point(450, 24);
            BtnPrevious.Name = "BtnPrevious";
            BtnPrevious.Size = new Size(48, 25);
            BtnPrevious.TabIndex = 43;
            BtnPrevious.Text = "Pre";
            BtnPrevious.UseVisualStyleBackColor = true;
            BtnPrevious.Click += BtnPrevious_Click;
            // 
            // BtnNext
            // 
            BtnNext.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            BtnNext.FlatAppearance.BorderSize = 0;
            BtnNext.FlatStyle = FlatStyle.Popup;
            BtnNext.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);
            BtnNext.Location = new Point(503, 24);
            BtnNext.Name = "BtnNext";
            BtnNext.Size = new Size(48, 25);
            BtnNext.TabIndex = 44;
            BtnNext.Text = "Next";
            BtnNext.UseVisualStyleBackColor = true;
            BtnNext.Click += BtnNext_Click;
            // 
            // LblJobImportedJobStats
            // 
            LblJobImportedJobStats.AutoSize = true;
            LblJobImportedJobStats.BackColor = Color.LightGray;
            LblJobImportedJobStats.Font = new Font("Segoe UI", 9F);
            LblJobImportedJobStats.ForeColor = Color.Blue;
            LblJobImportedJobStats.Location = new Point(12, 498);
            LblJobImportedJobStats.Name = "LblJobImportedJobStats";
            LblJobImportedJobStats.Padding = new Padding(0, 2, 2, 2);
            LblJobImportedJobStats.Size = new Size(54, 19);
            LblJobImportedJobStats.TabIndex = 45;
            LblJobImportedJobStats.Text = "Job stats";
            // 
            // LblCabinetStats
            // 
            LblCabinetStats.AutoSize = true;
            LblCabinetStats.BackColor = Color.LightGray;
            LblCabinetStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblCabinetStats.ForeColor = Color.Black;
            LblCabinetStats.Location = new Point(12, 63);
            LblCabinetStats.Name = "LblCabinetStats";
            LblCabinetStats.Size = new Size(78, 15);
            LblCabinetStats.TabIndex = 46;
            LblCabinetStats.Text = "Cabinet stats";
            // 
            // FrmImportedCabinetParts
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(1241, 526);
            Controls.Add(LblCabinetStats);
            Controls.Add(LblJobImportedJobStats);
            Controls.Add(BtnNext);
            Controls.Add(BtnPrevious);
            Controls.Add(DgvCabinetParts);
            Controls.Add(CmbCabinetName);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmImportedCabinetParts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cabinet Parts";
            Load += FrmAddAdditionalInstructions_Load;
            ((System.ComponentModel.ISupportInitialize)DgvCabinetParts).EndInit();
            ((System.ComponentModel.ISupportInitialize)cabinetPartBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox CmbCabinetName;
        private DataGridView DgvCabinetParts;
        private BindingSource cabinetPartBindingSource;
        private Button BtnPrevious;
        private Button BtnNext;
        private Label LblJobImportedJobStats;
        private Label LblCabinetStats;
    }
}