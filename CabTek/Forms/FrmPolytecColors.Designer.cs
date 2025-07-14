namespace PolytecOrderEDI
{
    partial class FrmPolytecColors
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPolytecColors));
            DgvPolytecColors = new DataGridView();
            BtnAddColor = new Button();
            BtnUpdateColor = new Button();
            LblTotalRecords = new Label();
            TxtSearchColor = new TextBox();
            LblSearchColor = new Label();
            TxtColor = new TextBox();
            label1 = new Label();
            GbModifyColor = new GroupBox();
            label5 = new Label();
            TxtMaterialCode = new TextBox();
            CmbGrain = new ComboBox();
            label4 = new Label();
            CmbSide = new ComboBox();
            CmbFinish = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            BtnCancelModify = new Button();
            BtnConfirmModify = new Button();
            BtnDeleteColor = new Button();
            BtnImportPolytecColors = new Button();
            ((System.ComponentModel.ISupportInitialize)DgvPolytecColors).BeginInit();
            GbModifyColor.SuspendLayout();
            SuspendLayout();
            // 
            // DgvPolytecColors
            // 
            DgvPolytecColors.AllowUserToAddRows = false;
            DgvPolytecColors.AllowUserToDeleteRows = false;
            DgvPolytecColors.AllowUserToOrderColumns = true;
            DgvPolytecColors.AllowUserToResizeColumns = false;
            DgvPolytecColors.AllowUserToResizeRows = false;
            DgvPolytecColors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DgvPolytecColors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DgvPolytecColors.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.LightGray;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DgvPolytecColors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DgvPolytecColors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvPolytecColors.Cursor = Cursors.IBeam;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.DarkGray;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DgvPolytecColors.DefaultCellStyle = dataGridViewCellStyle2;
            DgvPolytecColors.EnableHeadersVisualStyles = false;
            DgvPolytecColors.GridColor = Color.DarkGray;
            DgvPolytecColors.Location = new Point(19, 181);
            DgvPolytecColors.Margin = new Padding(10);
            DgvPolytecColors.MultiSelect = false;
            DgvPolytecColors.Name = "DgvPolytecColors";
            DgvPolytecColors.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = Color.IndianRed;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            DgvPolytecColors.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            DgvPolytecColors.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            DgvPolytecColors.RowsDefaultCellStyle = dataGridViewCellStyle4;
            DgvPolytecColors.ScrollBars = ScrollBars.Vertical;
            DgvPolytecColors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvPolytecColors.Size = new Size(899, 477);
            DgvPolytecColors.TabIndex = 0;
            DgvPolytecColors.TabStop = false;
            DgvPolytecColors.SelectionChanged += DgvPolytecColors_SelectionChanged;
            // 
            // BtnAddColor
            // 
            BtnAddColor.BackColor = Color.Green;
            BtnAddColor.BackgroundImageLayout = ImageLayout.Center;
            BtnAddColor.FlatAppearance.BorderColor = Color.DimGray;
            BtnAddColor.FlatAppearance.BorderSize = 0;
            BtnAddColor.FlatStyle = FlatStyle.Flat;
            BtnAddColor.Font = new Font("Segoe UI", 8F);
            BtnAddColor.ForeColor = Color.White;
            BtnAddColor.Location = new Point(19, 10);
            BtnAddColor.Name = "BtnAddColor";
            BtnAddColor.Size = new Size(104, 30);
            BtnAddColor.TabIndex = 39;
            BtnAddColor.TabStop = false;
            BtnAddColor.Text = "Add Color";
            BtnAddColor.UseVisualStyleBackColor = false;
            BtnAddColor.Click += BtnAddNewColor_Click;
            // 
            // BtnUpdateColor
            // 
            BtnUpdateColor.BackColor = Color.Blue;
            BtnUpdateColor.BackgroundImageLayout = ImageLayout.Center;
            BtnUpdateColor.FlatAppearance.BorderColor = Color.DimGray;
            BtnUpdateColor.FlatAppearance.BorderSize = 0;
            BtnUpdateColor.FlatStyle = FlatStyle.Flat;
            BtnUpdateColor.Font = new Font("Segoe UI", 8F);
            BtnUpdateColor.ForeColor = Color.White;
            BtnUpdateColor.Location = new Point(129, 10);
            BtnUpdateColor.Name = "BtnUpdateColor";
            BtnUpdateColor.Size = new Size(104, 30);
            BtnUpdateColor.TabIndex = 40;
            BtnUpdateColor.TabStop = false;
            BtnUpdateColor.Text = "Update Color";
            BtnUpdateColor.UseVisualStyleBackColor = false;
            BtnUpdateColor.Click += BtnEditColor_Click;
            // 
            // LblTotalRecords
            // 
            LblTotalRecords.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LblTotalRecords.ForeColor = Color.Black;
            LblTotalRecords.Location = new Point(17, 662);
            LblTotalRecords.Name = "LblTotalRecords";
            LblTotalRecords.Size = new Size(109, 15);
            LblTotalRecords.TabIndex = 41;
            LblTotalRecords.Text = "Total records:";
            LblTotalRecords.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TxtSearchColor
            // 
            TxtSearchColor.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            TxtSearchColor.Location = new Point(19, 152);
            TxtSearchColor.Name = "TxtSearchColor";
            TxtSearchColor.Size = new Size(211, 23);
            TxtSearchColor.TabIndex = 0;
            TxtSearchColor.TextChanged += TxtSearchColor_TextChanged;
            // 
            // LblSearchColor
            // 
            LblSearchColor.AutoSize = true;
            LblSearchColor.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            LblSearchColor.ForeColor = Color.Black;
            LblSearchColor.Location = new Point(232, 156);
            LblSearchColor.Name = "LblSearchColor";
            LblSearchColor.Size = new Size(135, 15);
            LblSearchColor.TabIndex = 43;
            LblSearchColor.Text = "Search by MaterialCode:";
            LblSearchColor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TxtColor
            // 
            TxtColor.Location = new Point(243, 48);
            TxtColor.Name = "TxtColor";
            TxtColor.Size = new Size(180, 23);
            TxtColor.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(243, 30);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 45;
            label1.Text = "Polytec Color";
            // 
            // GbModifyColor
            // 
            GbModifyColor.BackColor = Color.SkyBlue;
            GbModifyColor.BackgroundImageLayout = ImageLayout.None;
            GbModifyColor.Controls.Add(label5);
            GbModifyColor.Controls.Add(TxtMaterialCode);
            GbModifyColor.Controls.Add(CmbGrain);
            GbModifyColor.Controls.Add(label4);
            GbModifyColor.Controls.Add(CmbSide);
            GbModifyColor.Controls.Add(CmbFinish);
            GbModifyColor.Controls.Add(label3);
            GbModifyColor.Controls.Add(label2);
            GbModifyColor.Controls.Add(BtnCancelModify);
            GbModifyColor.Controls.Add(TxtColor);
            GbModifyColor.Controls.Add(label1);
            GbModifyColor.Controls.Add(BtnConfirmModify);
            GbModifyColor.FlatStyle = FlatStyle.Flat;
            GbModifyColor.Location = new Point(19, 46);
            GbModifyColor.Name = "GbModifyColor";
            GbModifyColor.Size = new Size(899, 90);
            GbModifyColor.TabIndex = 46;
            GbModifyColor.TabStop = false;
            GbModifyColor.Text = "Add/Edit/Delete Color";
            GbModifyColor.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 30);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 56;
            label5.Text = "ICB MaterialCode";
            // 
            // TxtMaterialCode
            // 
            TxtMaterialCode.Location = new Point(8, 48);
            TxtMaterialCode.Name = "TxtMaterialCode";
            TxtMaterialCode.Size = new Size(229, 23);
            TxtMaterialCode.TabIndex = 1;
            // 
            // CmbGrain
            // 
            CmbGrain.FormattingEnabled = true;
            CmbGrain.Location = new Point(642, 48);
            CmbGrain.Name = "CmbGrain";
            CmbGrain.Size = new Size(61, 23);
            CmbGrain.TabIndex = 53;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(642, 30);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 54;
            label4.Text = "Grain:";
            // 
            // CmbSide
            // 
            CmbSide.FormattingEnabled = true;
            CmbSide.Location = new Point(571, 48);
            CmbSide.Name = "CmbSide";
            CmbSide.Size = new Size(61, 23);
            CmbSide.TabIndex = 3;
            // 
            // CmbFinish
            // 
            CmbFinish.BackColor = SystemColors.Window;
            CmbFinish.FormattingEnabled = true;
            CmbFinish.Location = new Point(430, 48);
            CmbFinish.Name = "CmbFinish";
            CmbFinish.Size = new Size(131, 23);
            CmbFinish.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(571, 30);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 52;
            label3.Text = "Side:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(430, 30);
            label2.Name = "label2";
            label2.Size = new Size(80, 15);
            label2.TabIndex = 50;
            label2.Text = "Polytec Finish";
            // 
            // BtnCancelModify
            // 
            BtnCancelModify.BackColor = Color.Black;
            BtnCancelModify.BackgroundImageLayout = ImageLayout.Center;
            BtnCancelModify.FlatAppearance.BorderSize = 0;
            BtnCancelModify.FlatStyle = FlatStyle.Flat;
            BtnCancelModify.Font = new Font("Segoe UI", 7.5F);
            BtnCancelModify.ForeColor = Color.White;
            BtnCancelModify.Location = new Point(811, 47);
            BtnCancelModify.Name = "BtnCancelModify";
            BtnCancelModify.Size = new Size(74, 24);
            BtnCancelModify.TabIndex = 48;
            BtnCancelModify.TabStop = false;
            BtnCancelModify.Text = "Cancel";
            BtnCancelModify.UseVisualStyleBackColor = false;
            BtnCancelModify.Click += BtnCancelModify_Click;
            // 
            // BtnConfirmModify
            // 
            BtnConfirmModify.BackColor = Color.Black;
            BtnConfirmModify.BackgroundImageLayout = ImageLayout.Center;
            BtnConfirmModify.FlatAppearance.BorderSize = 0;
            BtnConfirmModify.FlatStyle = FlatStyle.Flat;
            BtnConfirmModify.Font = new Font("Segoe UI", 7.5F);
            BtnConfirmModify.ForeColor = Color.White;
            BtnConfirmModify.Location = new Point(731, 47);
            BtnConfirmModify.Name = "BtnConfirmModify";
            BtnConfirmModify.Size = new Size(74, 24);
            BtnConfirmModify.TabIndex = 47;
            BtnConfirmModify.TabStop = false;
            BtnConfirmModify.Text = "Confirm";
            BtnConfirmModify.UseVisualStyleBackColor = false;
            BtnConfirmModify.Click += BtnConfirmModify_Click;
            // 
            // BtnDeleteColor
            // 
            BtnDeleteColor.BackColor = Color.Red;
            BtnDeleteColor.BackgroundImageLayout = ImageLayout.Center;
            BtnDeleteColor.FlatAppearance.BorderColor = Color.DimGray;
            BtnDeleteColor.FlatAppearance.BorderSize = 0;
            BtnDeleteColor.FlatStyle = FlatStyle.Flat;
            BtnDeleteColor.Font = new Font("Segoe UI", 8F);
            BtnDeleteColor.ForeColor = Color.White;
            BtnDeleteColor.Location = new Point(239, 10);
            BtnDeleteColor.Name = "BtnDeleteColor";
            BtnDeleteColor.Size = new Size(104, 30);
            BtnDeleteColor.TabIndex = 47;
            BtnDeleteColor.TabStop = false;
            BtnDeleteColor.Text = "Delete Color";
            BtnDeleteColor.UseVisualStyleBackColor = false;
            BtnDeleteColor.Click += BtnDeleteColor_Click;
            // 
            // BtnImportPolytecColors
            // 
            BtnImportPolytecColors.BackColor = Color.RoyalBlue;
            BtnImportPolytecColors.BackgroundImageLayout = ImageLayout.Center;
            BtnImportPolytecColors.FlatAppearance.BorderColor = Color.DimGray;
            BtnImportPolytecColors.FlatAppearance.BorderSize = 0;
            BtnImportPolytecColors.FlatStyle = FlatStyle.Flat;
            BtnImportPolytecColors.Font = new Font("Segoe UI", 8F);
            BtnImportPolytecColors.ForeColor = Color.White;
            BtnImportPolytecColors.Location = new Point(750, 10);
            BtnImportPolytecColors.Name = "BtnImportPolytecColors";
            BtnImportPolytecColors.Size = new Size(168, 30);
            BtnImportPolytecColors.TabIndex = 48;
            BtnImportPolytecColors.TabStop = false;
            BtnImportPolytecColors.Text = "Import Board Colors (XLS)";
            BtnImportPolytecColors.UseVisualStyleBackColor = false;
            BtnImportPolytecColors.Click += BtnImportPolytecColors_Click;
            // 
            // FrmPolytecColors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(937, 696);
            Controls.Add(BtnImportPolytecColors);
            Controls.Add(BtnDeleteColor);
            Controls.Add(GbModifyColor);
            Controls.Add(LblSearchColor);
            Controls.Add(TxtSearchColor);
            Controls.Add(BtnAddColor);
            Controls.Add(LblTotalRecords);
            Controls.Add(BtnUpdateColor);
            Controls.Add(DgvPolytecColors);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPolytecColors";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Polytec Colors";
            Load += FrmPolytecColors_Load;
            ((System.ComponentModel.ISupportInitialize)DgvPolytecColors).EndInit();
            GbModifyColor.ResumeLayout(false);
            GbModifyColor.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView DgvPolytecColors;
        private Button BtnAddColor;
        private Button BtnUpdateColor;
        private Label LblTotalRecords;
        private TextBox TxtSearchColor;
        private Label LblSearchColor;
        private TextBox TxtColor;
        private Label label1;
        private GroupBox GbModifyColor;
        private Button BtnConfirmModify;
        private Button BtnCancelModify;
        private Label label2;
        private Label label3;
        private ComboBox CmbSide;
        private ComboBox CmbFinish;
        private ComboBox CmbGrain;
        private Label label4;
        private Button BtnDeleteColor;
        private TextBox TxtMaterialCode;
        private Label label5;
        private Button BtnImportPolytecColors;
    }
}