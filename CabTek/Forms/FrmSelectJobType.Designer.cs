namespace PolytecOrderEDI
{
    partial class FrmSelectJobType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectJobType));
            label1 = new Label();
            BtnVinylJob = new Button();
            BtnMelamineJob = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(215, 67);
            label1.Name = "label1";
            label1.Size = new Size(141, 28);
            label1.TabIndex = 0;
            label1.Text = "Pick Job Type";
            // 
            // BtnVinylJob
            // 
            BtnVinylJob.BackColor = Color.DimGray;
            BtnVinylJob.FlatAppearance.BorderSize = 0;
            BtnVinylJob.FlatAppearance.MouseDownBackColor = Color.DeepSkyBlue;
            BtnVinylJob.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
            BtnVinylJob.FlatStyle = FlatStyle.Flat;
            BtnVinylJob.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            BtnVinylJob.ForeColor = Color.White;
            BtnVinylJob.Location = new Point(86, 126);
            BtnVinylJob.Name = "BtnVinylJob";
            BtnVinylJob.Size = new Size(185, 78);
            BtnVinylJob.TabIndex = 1;
            BtnVinylJob.Text = "Vinyl";
            BtnVinylJob.UseVisualStyleBackColor = false;
            BtnVinylJob.Click += BtnVinylJob_Click;
            // 
            // BtnMelamineJob
            // 
            BtnMelamineJob.BackColor = Color.DimGray;
            BtnMelamineJob.FlatAppearance.BorderSize = 0;
            BtnMelamineJob.FlatStyle = FlatStyle.Flat;
            BtnMelamineJob.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            BtnMelamineJob.ForeColor = Color.White;
            BtnMelamineJob.Location = new Point(300, 126);
            BtnMelamineJob.Name = "BtnMelamineJob";
            BtnMelamineJob.Size = new Size(185, 78);
            BtnMelamineJob.TabIndex = 2;
            BtnMelamineJob.Text = "Melamine";
            BtnMelamineJob.UseVisualStyleBackColor = false;
            BtnMelamineJob.Click += BtnMelamineJob_Click;
            // 
            // FrmSelectJobType
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightGray;
            ClientSize = new Size(591, 282);
            Controls.Add(BtnMelamineJob);
            Controls.Add(BtnVinylJob);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSelectJobType";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Loading..";
            Load += FrmSelectJobType_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button BtnVinylJob;
        private Button BtnMelamineJob;
    }
}