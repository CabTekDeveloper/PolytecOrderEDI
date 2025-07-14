namespace PolytecOrderEDI
{
    partial class FrmDatePicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatePicker));
            MonthCalender1 = new MonthCalendar();
            LblCalenderTitle = new Label();
            SuspendLayout();
            // 
            // MonthCalender1
            // 
            MonthCalender1.BackColor = SystemColors.Window;
            MonthCalender1.Font = new Font("Segoe UI", 1F);
            MonthCalender1.Location = new Point(19, 29);
            MonthCalender1.Margin = new Padding(10);
            MonthCalender1.MaxSelectionCount = 1;
            MonthCalender1.Name = "MonthCalender1";
            MonthCalender1.ShowToday = false;
            MonthCalender1.ShowTodayCircle = false;
            MonthCalender1.TabIndex = 0;
            MonthCalender1.TabStop = false;
            MonthCalender1.DateSelected += MonthCalender1_DateSelected;
            // 
            // LblCalenderTitle
            // 
            LblCalenderTitle.FlatStyle = FlatStyle.Flat;
            LblCalenderTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LblCalenderTitle.Location = new Point(19, 8);
            LblCalenderTitle.Name = "LblCalenderTitle";
            LblCalenderTitle.Size = new Size(227, 18);
            LblCalenderTitle.TabIndex = 1;
            LblCalenderTitle.Text = "Calender Title";
            LblCalenderTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmDatePicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(268, 197);
            Controls.Add(LblCalenderTitle);
            Controls.Add(MonthCalender1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FrmDatePicker";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDatePicker";
            Load += FrmDatePicker_Load;
            ResumeLayout(false);
        }

        #endregion

        private MonthCalendar MonthCalender1;
        private Label LblCalenderTitle;
    }
}