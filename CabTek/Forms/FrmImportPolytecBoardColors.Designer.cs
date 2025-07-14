namespace PolytecOrderEDI
{
    partial class FrmImportPolytecBoardColors
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportPolytecBoardColors));
            label1 = new Label();
            LblTitleInfo = new Label();
            BtnClose = new Button();
            BtnImportBoardColors = new Button();
            pictureBox1 = new PictureBox();
            LblImportMessage = new Label();
            BtnViewImportedColors = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 24);
            label1.Name = "label1";
            label1.Size = new Size(10, 15);
            label1.TabIndex = 0;
            label1.Text = " ";
            // 
            // LblTitleInfo
            // 
            LblTitleInfo.Font = new Font("Segoe UI", 10F);
            LblTitleInfo.ForeColor = Color.Blue;
            LblTitleInfo.Location = new Point(12, 9);
            LblTitleInfo.Name = "LblTitleInfo";
            LblTitleInfo.Size = new Size(1106, 62);
            LblTitleInfo.TabIndex = 1;
            LblTitleInfo.Text = "* Title Info";
            LblTitleInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BtnClose
            // 
            BtnClose.BackColor = Color.Crimson;
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnClose.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnClose.FlatAppearance.MouseOverBackColor = Color.Red;
            BtnClose.FlatStyle = FlatStyle.Flat;
            BtnClose.Font = new Font("Segoe UI", 9F);
            BtnClose.ForeColor = Color.WhiteSmoke;
            BtnClose.Location = new Point(607, 485);
            BtnClose.Margin = new Padding(3, 4, 3, 4);
            BtnClose.Name = "BtnClose";
            BtnClose.Size = new Size(114, 34);
            BtnClose.TabIndex = 48;
            BtnClose.Text = "Close";
            BtnClose.UseVisualStyleBackColor = false;
            BtnClose.Click += BtnClose_Click;
            // 
            // BtnImportBoardColors
            // 
            BtnImportBoardColors.BackColor = Color.RoyalBlue;
            BtnImportBoardColors.FlatAppearance.BorderSize = 0;
            BtnImportBoardColors.FlatAppearance.MouseDownBackColor = Color.MidnightBlue;
            BtnImportBoardColors.FlatAppearance.MouseOverBackColor = Color.MediumBlue;
            BtnImportBoardColors.FlatStyle = FlatStyle.Flat;
            BtnImportBoardColors.Font = new Font("Segoe UI", 9F);
            BtnImportBoardColors.ForeColor = Color.WhiteSmoke;
            BtnImportBoardColors.Location = new Point(374, 485);
            BtnImportBoardColors.Margin = new Padding(3, 4, 3, 4);
            BtnImportBoardColors.Name = "BtnImportBoardColors";
            BtnImportBoardColors.Size = new Size(209, 34);
            BtnImportBoardColors.TabIndex = 49;
            BtnImportBoardColors.Text = "Import";
            BtnImportBoardColors.UseVisualStyleBackColor = false;
            BtnImportBoardColors.Click += BtnImportBoardColors_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.PolytecBoardColourImportTemplate;
            pictureBox1.Location = new Point(12, 84);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1106, 191);
            pictureBox1.TabIndex = 50;
            pictureBox1.TabStop = false;
            // 
            // LblImportMessage
            // 
            LblImportMessage.BackColor = Color.Transparent;
            LblImportMessage.Font = new Font("Segoe UI", 10F);
            LblImportMessage.ForeColor = Color.Black;
            LblImportMessage.Location = new Point(12, 279);
            LblImportMessage.Name = "LblImportMessage";
            LblImportMessage.Size = new Size(1106, 182);
            LblImportMessage.TabIndex = 51;
            LblImportMessage.Text = "Import Message";
            LblImportMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BtnViewImportedColors
            // 
            BtnViewImportedColors.AutoSize = true;
            BtnViewImportedColors.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnViewImportedColors.BackColor = Color.LightSkyBlue;
            BtnViewImportedColors.FlatAppearance.BorderSize = 0;
            BtnViewImportedColors.FlatAppearance.MouseDownBackColor = Color.DeepSkyBlue;
            BtnViewImportedColors.FlatAppearance.MouseOverBackColor = Color.DeepSkyBlue;
            BtnViewImportedColors.FlatStyle = FlatStyle.Flat;
            BtnViewImportedColors.Font = new Font("Segoe UI", 7F);
            BtnViewImportedColors.ForeColor = Color.Black;
            BtnViewImportedColors.Location = new Point(506, 395);
            BtnViewImportedColors.Margin = new Padding(3, 4, 3, 4);
            BtnViewImportedColors.Name = "BtnViewImportedColors";
            BtnViewImportedColors.Size = new Size(115, 22);
            BtnViewImportedColors.TabIndex = 52;
            BtnViewImportedColors.Text = "View Imported Colours";
            BtnViewImportedColors.UseVisualStyleBackColor = false;
            BtnViewImportedColors.Visible = false;
            BtnViewImportedColors.Click += BtnViewImportedColors_Click;
            // 
            // FrmImportPolytecBoardColors
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1130, 532);
            ControlBox = false;
            Controls.Add(BtnViewImportedColors);
            Controls.Add(LblImportMessage);
            Controls.Add(pictureBox1);
            Controls.Add(BtnImportBoardColors);
            Controls.Add(BtnClose);
            Controls.Add(LblTitleInfo);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmImportPolytecBoardColors";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Import Polytec board colors.";
            Load += FrmImportPolytecBoardColors_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label LblTitleInfo;
        private Button BtnClose;
        private Button BtnImportBoardColors;
        private PictureBox pictureBox1;
        private Label LblImportMessage;
        private Button BtnViewImportedColors;
    }
}