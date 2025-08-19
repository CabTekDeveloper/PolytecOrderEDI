namespace PolytecOrderEDI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            BtnImportFile = new Button();
            LblFileImportMsg = new Label();
            BtnViewOrderXML = new Button();
            ChkTesting = new CheckBox();
            BtnSendOrder = new Button();
            LblSendFileMsg = new Label();
            LblCopyright = new Label();
            LblPoNumber = new Label();
            lbl3 = new Label();
            lbl4 = new Label();
            lbl1 = new Label();
            LblRequestedDate = new Label();
            Lbl0 = new Label();
            Lbl2 = new Label();
            LblCurrentUserName = new Label();
            BtnInfo = new Button();
            lblAttachmentName3 = new Label();
            lblAttachmentName2 = new Label();
            lblAttachmentName1 = new Label();
            label1 = new Label();
            BtnOpenAddAttachmentForm = new Button();
            LblJobType = new Label();
            BtnChangeJobType = new Button();
            BtnEditPoNumber = new Button();
            BtnPickRequestedDate = new Button();
            BtnOpenFrmPolytecColors = new Button();
            BtnViewImportedCabinetParts = new Button();
            SuspendLayout();
            // 
            // BtnImportFile
            // 
            BtnImportFile.BackColor = Color.Navy;
            BtnImportFile.FlatAppearance.BorderColor = Color.White;
            BtnImportFile.FlatAppearance.BorderSize = 0;
            BtnImportFile.FlatAppearance.MouseDownBackColor = Color.Blue;
            BtnImportFile.FlatAppearance.MouseOverBackColor = Color.Blue;
            BtnImportFile.FlatStyle = FlatStyle.Flat;
            BtnImportFile.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            BtnImportFile.ForeColor = Color.White;
            BtnImportFile.Location = new Point(694, 160);
            BtnImportFile.Name = "BtnImportFile";
            BtnImportFile.Size = new Size(128, 35);
            BtnImportFile.TabIndex = 0;
            BtnImportFile.Text = "Import File";
            BtnImportFile.UseVisualStyleBackColor = false;
            BtnImportFile.Click += BtnImportFile_Click;
            // 
            // LblFileImportMsg
            // 
            LblFileImportMsg.BackColor = Color.WhiteSmoke;
            LblFileImportMsg.BorderStyle = BorderStyle.Fixed3D;
            LblFileImportMsg.FlatStyle = FlatStyle.Flat;
            LblFileImportMsg.Font = new Font("Segoe UI", 9F);
            LblFileImportMsg.ForeColor = Color.Blue;
            LblFileImportMsg.Location = new Point(265, 230);
            LblFileImportMsg.Margin = new Padding(3);
            LblFileImportMsg.Name = "LblFileImportMsg";
            LblFileImportMsg.Size = new Size(405, 53);
            LblFileImportMsg.TabIndex = 4;
            LblFileImportMsg.Text = "For file import stats message";
            // 
            // BtnViewOrderXML
            // 
            BtnViewOrderXML.BackColor = Color.Black;
            BtnViewOrderXML.FlatAppearance.BorderColor = Color.White;
            BtnViewOrderXML.FlatAppearance.BorderSize = 0;
            BtnViewOrderXML.FlatAppearance.MouseDownBackColor = Color.DimGray;
            BtnViewOrderXML.FlatAppearance.MouseOverBackColor = Color.DimGray;
            BtnViewOrderXML.FlatStyle = FlatStyle.Flat;
            BtnViewOrderXML.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            BtnViewOrderXML.ForeColor = Color.White;
            BtnViewOrderXML.Location = new Point(694, 204);
            BtnViewOrderXML.Name = "BtnViewOrderXML";
            BtnViewOrderXML.Size = new Size(128, 35);
            BtnViewOrderXML.TabIndex = 5;
            BtnViewOrderXML.Text = "View Order (.xml)";
            BtnViewOrderXML.UseVisualStyleBackColor = false;
            BtnViewOrderXML.Click += BtnViewOrderXML_Click;
            // 
            // ChkTesting
            // 
            ChkTesting.AutoSize = true;
            ChkTesting.BackColor = Color.WhiteSmoke;
            ChkTesting.CheckAlign = ContentAlignment.MiddleRight;
            ChkTesting.FlatAppearance.BorderSize = 0;
            ChkTesting.FlatStyle = FlatStyle.Flat;
            ChkTesting.Font = new Font("Segoe UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ChkTesting.ForeColor = Color.Black;
            ChkTesting.Location = new Point(12, 38);
            ChkTesting.Name = "ChkTesting";
            ChkTesting.Padding = new Padding(0, 2, 3, 2);
            ChkTesting.Size = new Size(59, 20);
            ChkTesting.TabIndex = 7;
            ChkTesting.Text = "Testing :";
            ChkTesting.UseVisualStyleBackColor = false;
            ChkTesting.CheckedChanged += ChkTesting_CheckedChanged;
            // 
            // BtnSendOrder
            // 
            BtnSendOrder.BackColor = Color.Black;
            BtnSendOrder.FlatAppearance.BorderColor = Color.White;
            BtnSendOrder.FlatAppearance.BorderSize = 0;
            BtnSendOrder.FlatAppearance.MouseDownBackColor = Color.DimGray;
            BtnSendOrder.FlatAppearance.MouseOverBackColor = Color.DimGray;
            BtnSendOrder.FlatStyle = FlatStyle.Flat;
            BtnSendOrder.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            BtnSendOrder.ForeColor = Color.White;
            BtnSendOrder.Location = new Point(694, 248);
            BtnSendOrder.Name = "BtnSendOrder";
            BtnSendOrder.Size = new Size(128, 35);
            BtnSendOrder.TabIndex = 8;
            BtnSendOrder.Text = "Send Order";
            BtnSendOrder.UseVisualStyleBackColor = false;
            BtnSendOrder.Click += BtnSendOrder_Click;
            // 
            // LblSendFileMsg
            // 
            LblSendFileMsg.BackColor = Color.WhiteSmoke;
            LblSendFileMsg.BorderStyle = BorderStyle.Fixed3D;
            LblSendFileMsg.Font = new Font("Segoe UI", 9F);
            LblSendFileMsg.ForeColor = Color.Blue;
            LblSendFileMsg.Location = new Point(265, 418);
            LblSendFileMsg.Margin = new Padding(3);
            LblSendFileMsg.Name = "LblSendFileMsg";
            LblSendFileMsg.Size = new Size(405, 171);
            LblSendFileMsg.TabIndex = 11;
            LblSendFileMsg.Text = "For order sent stats message";
            // 
            // LblCopyright
            // 
            LblCopyright.Font = new Font("Times New Roman", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LblCopyright.ForeColor = Color.Black;
            LblCopyright.Location = new Point(321, 657);
            LblCopyright.Name = "LblCopyright";
            LblCopyright.Size = new Size(300, 15);
            LblCopyright.TabIndex = 13;
            LblCopyright.Text = "© 2024 - 2025 | CabTek Industries";
            LblCopyright.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LblPoNumber
            // 
            LblPoNumber.BackColor = Color.WhiteSmoke;
            LblPoNumber.BorderStyle = BorderStyle.Fixed3D;
            LblPoNumber.FlatStyle = FlatStyle.Flat;
            LblPoNumber.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            LblPoNumber.ForeColor = Color.Blue;
            LblPoNumber.Location = new Point(265, 160);
            LblPoNumber.Margin = new Padding(3);
            LblPoNumber.Name = "LblPoNumber";
            LblPoNumber.Size = new Size(405, 35);
            LblPoNumber.TabIndex = 14;
            LblPoNumber.Text = "PO Number";
            LblPoNumber.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl3
            // 
            lbl3.Font = new Font("Segoe UI", 9F);
            lbl3.ForeColor = Color.Black;
            lbl3.Location = new Point(148, 230);
            lbl3.Margin = new Padding(3);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(116, 18);
            lbl3.TabIndex = 15;
            lbl3.Text = "Import Stats :";
            lbl3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbl4
            // 
            lbl4.Font = new Font("Segoe UI", 9F);
            lbl4.ForeColor = Color.Black;
            lbl4.Location = new Point(148, 416);
            lbl4.Margin = new Padding(3);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(116, 18);
            lbl4.TabIndex = 16;
            lbl4.Text = "Send Stats :";
            lbl4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lbl1
            // 
            lbl1.Font = new Font("Segoe UI", 9F);
            lbl1.ForeColor = Color.Black;
            lbl1.Location = new Point(148, 203);
            lbl1.Margin = new Padding(3);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(116, 18);
            lbl1.TabIndex = 18;
            lbl1.Text = "Requested Date :";
            lbl1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LblRequestedDate
            // 
            LblRequestedDate.BackColor = Color.WhiteSmoke;
            LblRequestedDate.BorderStyle = BorderStyle.Fixed3D;
            LblRequestedDate.FlatStyle = FlatStyle.Flat;
            LblRequestedDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            LblRequestedDate.ForeColor = Color.Blue;
            LblRequestedDate.Location = new Point(265, 200);
            LblRequestedDate.Margin = new Padding(3);
            LblRequestedDate.Name = "LblRequestedDate";
            LblRequestedDate.Size = new Size(152, 24);
            LblRequestedDate.TabIndex = 17;
            LblRequestedDate.Text = "Requested date";
            LblRequestedDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Lbl0
            // 
            Lbl0.Font = new Font("Segoe UI", 9F);
            Lbl0.ForeColor = Color.Black;
            Lbl0.Location = new Point(148, 170);
            Lbl0.Margin = new Padding(3);
            Lbl0.Name = "Lbl0";
            Lbl0.Size = new Size(116, 18);
            Lbl0.TabIndex = 21;
            Lbl0.Text = "Purchase Order No :";
            Lbl0.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Lbl2
            // 
            Lbl2.Font = new Font("Segoe UI", 9F);
            Lbl2.ForeColor = Color.Black;
            Lbl2.Location = new Point(148, 595);
            Lbl2.Name = "Lbl2";
            Lbl2.Size = new Size(116, 18);
            Lbl2.TabIndex = 23;
            Lbl2.Text = "Current User :";
            Lbl2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LblCurrentUserName
            // 
            LblCurrentUserName.BackColor = Color.WhiteSmoke;
            LblCurrentUserName.BorderStyle = BorderStyle.Fixed3D;
            LblCurrentUserName.FlatStyle = FlatStyle.Flat;
            LblCurrentUserName.Font = new Font("Segoe UI", 9F);
            LblCurrentUserName.ForeColor = Color.Black;
            LblCurrentUserName.Location = new Point(265, 595);
            LblCurrentUserName.Margin = new Padding(3);
            LblCurrentUserName.Name = "LblCurrentUserName";
            LblCurrentUserName.Size = new Size(405, 18);
            LblCurrentUserName.TabIndex = 22;
            LblCurrentUserName.Text = "Current user name";
            LblCurrentUserName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtnInfo
            // 
            BtnInfo.BackColor = Color.WhiteSmoke;
            BtnInfo.FlatAppearance.BorderSize = 0;
            BtnInfo.FlatStyle = FlatStyle.Flat;
            BtnInfo.Font = new Font("Segoe UI", 7F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnInfo.Location = new Point(12, 12);
            BtnInfo.Name = "BtnInfo";
            BtnInfo.Size = new Size(20, 20);
            BtnInfo.TabIndex = 24;
            BtnInfo.Text = "?";
            BtnInfo.UseVisualStyleBackColor = false;
            BtnInfo.Click += BtnInfo_Click;
            // 
            // lblAttachmentName3
            // 
            lblAttachmentName3.BackColor = Color.WhiteSmoke;
            lblAttachmentName3.BorderStyle = BorderStyle.Fixed3D;
            lblAttachmentName3.FlatStyle = FlatStyle.Flat;
            lblAttachmentName3.Font = new Font("Segoe UI", 8F);
            lblAttachmentName3.ForeColor = Color.Black;
            lblAttachmentName3.Location = new Point(265, 385);
            lblAttachmentName3.Margin = new Padding(3);
            lblAttachmentName3.Name = "lblAttachmentName3";
            lblAttachmentName3.Size = new Size(353, 18);
            lblAttachmentName3.TabIndex = 34;
            lblAttachmentName3.Text = "Attachment 3";
            lblAttachmentName3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAttachmentName2
            // 
            lblAttachmentName2.BackColor = Color.WhiteSmoke;
            lblAttachmentName2.BorderStyle = BorderStyle.Fixed3D;
            lblAttachmentName2.FlatStyle = FlatStyle.Flat;
            lblAttachmentName2.Font = new Font("Segoe UI", 8F);
            lblAttachmentName2.ForeColor = Color.Black;
            lblAttachmentName2.Location = new Point(265, 364);
            lblAttachmentName2.Margin = new Padding(3);
            lblAttachmentName2.Name = "lblAttachmentName2";
            lblAttachmentName2.Size = new Size(353, 18);
            lblAttachmentName2.TabIndex = 33;
            lblAttachmentName2.Text = "Attachment 2";
            lblAttachmentName2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAttachmentName1
            // 
            lblAttachmentName1.BackColor = Color.WhiteSmoke;
            lblAttachmentName1.BorderStyle = BorderStyle.Fixed3D;
            lblAttachmentName1.FlatStyle = FlatStyle.Flat;
            lblAttachmentName1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAttachmentName1.ForeColor = Color.Black;
            lblAttachmentName1.Location = new Point(265, 343);
            lblAttachmentName1.Margin = new Padding(3);
            lblAttachmentName1.Name = "lblAttachmentName1";
            lblAttachmentName1.Size = new Size(353, 18);
            lblAttachmentName1.TabIndex = 32;
            lblAttachmentName1.Text = "Attachment 1";
            lblAttachmentName1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(148, 344);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(116, 18);
            label1.TabIndex = 35;
            label1.Text = "Attachments :";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // BtnOpenAddAttachmentForm
            // 
            BtnOpenAddAttachmentForm.BackColor = Color.Black;
            BtnOpenAddAttachmentForm.FlatAppearance.BorderColor = Color.LightGray;
            BtnOpenAddAttachmentForm.FlatAppearance.BorderSize = 8;
            BtnOpenAddAttachmentForm.FlatAppearance.MouseDownBackColor = Color.Black;
            BtnOpenAddAttachmentForm.FlatAppearance.MouseOverBackColor = Color.DimGray;
            BtnOpenAddAttachmentForm.FlatStyle = FlatStyle.Flat;
            BtnOpenAddAttachmentForm.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnOpenAddAttachmentForm.ForeColor = Color.WhiteSmoke;
            BtnOpenAddAttachmentForm.Location = new Point(624, 343);
            BtnOpenAddAttachmentForm.Name = "BtnOpenAddAttachmentForm";
            BtnOpenAddAttachmentForm.Size = new Size(46, 60);
            BtnOpenAddAttachmentForm.TabIndex = 36;
            BtnOpenAddAttachmentForm.Text = "+";
            BtnOpenAddAttachmentForm.UseVisualStyleBackColor = false;
            BtnOpenAddAttachmentForm.Visible = false;
            BtnOpenAddAttachmentForm.Click += BtnOpenAddAttachmentForm_Click;
            // 
            // LblJobType
            // 
            LblJobType.BackColor = Color.Silver;
            LblJobType.FlatStyle = FlatStyle.Flat;
            LblJobType.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblJobType.ForeColor = Color.Black;
            LblJobType.Location = new Point(-4, 1);
            LblJobType.Margin = new Padding(3);
            LblJobType.Name = "LblJobType";
            LblJobType.Size = new Size(954, 129);
            LblJobType.TabIndex = 37;
            LblJobType.Text = "Job Type";
            LblJobType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnChangeJobType
            // 
            BtnChangeJobType.AutoSize = true;
            BtnChangeJobType.BackColor = Color.White;
            BtnChangeJobType.BackgroundImageLayout = ImageLayout.Center;
            BtnChangeJobType.FlatAppearance.BorderSize = 0;
            BtnChangeJobType.FlatStyle = FlatStyle.Flat;
            BtnChangeJobType.Font = new Font("Segoe UI", 7F);
            BtnChangeJobType.ForeColor = Color.Black;
            BtnChangeJobType.Location = new Point(694, 58);
            BtnChangeJobType.Name = "BtnChangeJobType";
            BtnChangeJobType.Size = new Size(128, 26);
            BtnChangeJobType.TabIndex = 38;
            BtnChangeJobType.Text = "Change";
            BtnChangeJobType.UseVisualStyleBackColor = false;
            BtnChangeJobType.Click += BtnChangeJobType_Click;
            // 
            // BtnEditPoNumber
            // 
            BtnEditPoNumber.BackColor = Color.WhiteSmoke;
            BtnEditPoNumber.FlatAppearance.BorderSize = 0;
            BtnEditPoNumber.FlatStyle = FlatStyle.Flat;
            BtnEditPoNumber.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtnEditPoNumber.ForeColor = Color.Blue;
            BtnEditPoNumber.Location = new Point(631, 169);
            BtnEditPoNumber.Name = "BtnEditPoNumber";
            BtnEditPoNumber.Size = new Size(35, 19);
            BtnEditPoNumber.TabIndex = 39;
            BtnEditPoNumber.Text = "Edit";
            BtnEditPoNumber.UseVisualStyleBackColor = false;
            BtnEditPoNumber.Visible = false;
            BtnEditPoNumber.Click += BtnEditPoNumber_Click;
            // 
            // BtnPickRequestedDate
            // 
            BtnPickRequestedDate.BackgroundImage = Properties.Resources.calendar;
            BtnPickRequestedDate.BackgroundImageLayout = ImageLayout.Stretch;
            BtnPickRequestedDate.FlatAppearance.BorderSize = 0;
            BtnPickRequestedDate.FlatStyle = FlatStyle.Flat;
            BtnPickRequestedDate.Location = new Point(419, 202);
            BtnPickRequestedDate.Name = "BtnPickRequestedDate";
            BtnPickRequestedDate.Size = new Size(20, 20);
            BtnPickRequestedDate.TabIndex = 41;
            BtnPickRequestedDate.UseVisualStyleBackColor = true;
            BtnPickRequestedDate.Visible = false;
            BtnPickRequestedDate.Click += BtnPickRequestedDate_Click;
            // 
            // BtnOpenFrmPolytecColors
            // 
            BtnOpenFrmPolytecColors.BackColor = Color.White;
            BtnOpenFrmPolytecColors.BackgroundImageLayout = ImageLayout.Center;
            BtnOpenFrmPolytecColors.FlatAppearance.BorderSize = 0;
            BtnOpenFrmPolytecColors.FlatStyle = FlatStyle.Flat;
            BtnOpenFrmPolytecColors.Font = new Font("Segoe UI", 7.5F);
            BtnOpenFrmPolytecColors.ForeColor = Color.Black;
            BtnOpenFrmPolytecColors.Location = new Point(12, 64);
            BtnOpenFrmPolytecColors.Name = "BtnOpenFrmPolytecColors";
            BtnOpenFrmPolytecColors.Size = new Size(115, 20);
            BtnOpenFrmPolytecColors.TabIndex = 42;
            BtnOpenFrmPolytecColors.Text = "Polytec Board Colors";
            BtnOpenFrmPolytecColors.UseVisualStyleBackColor = false;
            BtnOpenFrmPolytecColors.Click += BtnOpenFrmPolytecColors_Click;
            // 
            // BtnViewImportedCabinetParts
            // 
            BtnViewImportedCabinetParts.BackColor = Color.Black;
            BtnViewImportedCabinetParts.FlatAppearance.BorderColor = Color.White;
            BtnViewImportedCabinetParts.FlatAppearance.BorderSize = 0;
            BtnViewImportedCabinetParts.FlatAppearance.MouseDownBackColor = Color.DimGray;
            BtnViewImportedCabinetParts.FlatAppearance.MouseOverBackColor = Color.DimGray;
            BtnViewImportedCabinetParts.FlatStyle = FlatStyle.Flat;
            BtnViewImportedCabinetParts.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            BtnViewImportedCabinetParts.ForeColor = Color.White;
            BtnViewImportedCabinetParts.Location = new Point(265, 289);
            BtnViewImportedCabinetParts.Name = "BtnViewImportedCabinetParts";
            BtnViewImportedCabinetParts.Size = new Size(136, 34);
            BtnViewImportedCabinetParts.TabIndex = 43;
            BtnViewImportedCabinetParts.Text = "View Imported Parts";
            BtnViewImportedCabinetParts.UseVisualStyleBackColor = false;
            BtnViewImportedCabinetParts.Visible = false;
            BtnViewImportedCabinetParts.Click += BtnViewImportedCabinetParts_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(945, 692);
            Controls.Add(BtnViewImportedCabinetParts);
            Controls.Add(BtnOpenFrmPolytecColors);
            Controls.Add(BtnPickRequestedDate);
            Controls.Add(BtnEditPoNumber);
            Controls.Add(ChkTesting);
            Controls.Add(BtnInfo);
            Controls.Add(BtnChangeJobType);
            Controls.Add(BtnOpenAddAttachmentForm);
            Controls.Add(label1);
            Controls.Add(lblAttachmentName3);
            Controls.Add(lblAttachmentName2);
            Controls.Add(lblAttachmentName1);
            Controls.Add(Lbl2);
            Controls.Add(LblCurrentUserName);
            Controls.Add(Lbl0);
            Controls.Add(lbl1);
            Controls.Add(LblRequestedDate);
            Controls.Add(lbl4);
            Controls.Add(lbl3);
            Controls.Add(LblCopyright);
            Controls.Add(LblSendFileMsg);
            Controls.Add(BtnSendOrder);
            Controls.Add(BtnViewOrderXML);
            Controls.Add(LblFileImportMsg);
            Controls.Add(BtnImportFile);
            Controls.Add(LblJobType);
            Controls.Add(LblPoNumber);
            Font = new Font("Segoe UI", 9.75F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Polytec Order EDI (V.2025.08.19)";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button BtnImportFile;
        private Label LblFileImportMsg;
        private Button BtnViewOrderXML;
        private CheckBox ChkTesting;
        private Button BtnSendOrder;
        private Label LblSendFileMsg;
        //private Label xxx;
        private Label LblCopyright;
        private Label LblPoNumber;
        private Label lbl3;
        private Label lbl4;
        private Label lbl1;
        private Label LblRequestedDate;
        private Label Lbl0;
        private Label Lbl2;
        private Label LblCurrentUserName;
        private Button BtnInfo;
        private Label lblAttachmentName3;
        private Label lblAttachmentName2;
        private Label lblAttachmentName1;
        private Label label1;
        private Button BtnOpenAddAttachmentForm;
        private Label LblJobType;
        private Button BtnChangeJobType;
        private Button BtnEditPoNumber;
        private Button BtnPickRequestedDate;
        private Button BtnOpenFrmPolytecColors;
        private Button BtnViewImportedCabinetParts;
    }
}