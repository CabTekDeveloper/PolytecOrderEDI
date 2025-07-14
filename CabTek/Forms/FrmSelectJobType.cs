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
    public partial class FrmSelectJobType : Form
    {
        public FrmSelectJobType()
        {
            InitializeComponent();
        }

        private void FrmSelectJobType_Load(object sender, EventArgs e)
        {
            BtnVinylJob.BackColor = ColorManager.GetJobTypeBackColor(JOBTYPE.Vinyl);
            BtnVinylJob.ForeColor = ColorManager.GetJobTypeForeColor(JOBTYPE.Vinyl);

            BtnMelamineJob.BackColor = ColorManager.GetJobTypeBackColor(JOBTYPE.Melamine);
            BtnMelamineJob.ForeColor = ColorManager.GetJobTypeForeColor(JOBTYPE.Melamine);
        }

        private void BtnVinylJob_Click(object sender, EventArgs e)
        {
            GlobalVariable.JobType = JOBTYPE.Vinyl;
            this.Close();
        }

        private void BtnMelamineJob_Click(object sender, EventArgs e)
        {
            GlobalVariable.JobType = JOBTYPE.Melamine;
            this.Close();
        }

        
    }
}
