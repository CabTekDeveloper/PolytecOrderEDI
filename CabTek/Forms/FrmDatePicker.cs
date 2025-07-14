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
    public partial class FrmDatePicker : Form
    {
        private bool IsRequestedDate { get; set; } = false;
        private string DatePickerTitle { get; set; } = string.Empty;


        public FrmDatePicker(string title, bool isRequestedDate = false)
        {
            InitializeComponent();
            IsRequestedDate = isRequestedDate;
            DatePickerTitle = title;
        }


        private void FrmDatePicker_Load(object sender, EventArgs e)
        {
            if (IsRequestedDate)
            {
                var minDate = DateTime.Now.AddDays(5);
                var dayOfWeek = minDate.DayOfWeek;
                if (dayOfWeek == DayOfWeek.Saturday)
                {
                    minDate = minDate.AddDays(2);
                }
                else if (dayOfWeek == DayOfWeek.Sunday)
                {
                    minDate = minDate.AddDays(1);
                }

                MonthCalender1.MinDate = minDate;
                LblCalenderTitle.Text = DatePickerTitle;
            }
        }


        private void MonthCalender1_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateTime datePicked = MonthCalender1.SelectionRange.Start;

            var day = datePicked.Day.ToString();
            var month = datePicked.Month.ToString();
            var year = datePicked.Year.ToString();

            day = (day.Length == 1) ? ("0" + day) : day;
            month = (month.Length == 1) ? ("0" + month) : month;
            year = (day.Length == 1) ? ("0" + year) : year;

            if (IsRequestedDate) GlobalVariable.RequestedDate = $"{day}/{month}/{year}";

            this.Close();
        }

        
    }
}
