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
    public partial class FrmAddAttachments : Form
    {
        public FrmAddAttachments()
        {
            InitializeComponent();
        }

        private void BtnImportAttachment1_Click(object sender, EventArgs e)
        {
            if (AttachmentManager.Import())
            {
                Attachment file = AttachmentManager.Attachments[^1];
                lbl1.Text = file.FileName;
                BtnAddAttachmentsToConfiguredOrder.Visible = true;
                BtnResetAttachments.Visible = true;

                if(sender is Button currentBtn) { currentBtn.Visible = false; } 
            }
        }

        private void BtnImportAttachment2_Click(object sender, EventArgs e)
        {
            if (AttachmentManager.Import())
            {
                Attachment file = AttachmentManager.Attachments[^1];
                lbl2.Text = file.FileName;
                BtnAddAttachmentsToConfiguredOrder.Visible = true;
                BtnResetAttachments.Visible = true;
                if (sender is Button currentBtn) { currentBtn.Visible = false; }
            }
        }

        private void BtnImportAttachment3_Click(object sender, EventArgs e)
        {
            if (AttachmentManager.Import())
            {
                Attachment file = AttachmentManager.Attachments[^1];
                lbl3.Text = file.FileName;
                BtnAddAttachmentsToConfiguredOrder.Visible = true;
                BtnResetAttachments.Visible = true;
                if (sender is Button currentBtn) { currentBtn.Visible = false; }
            }
        }
        
        private void BtnResetAttachments_Click(object sender, EventArgs e)
        {
            lbl1.Text = "";
            lbl2.Text = "";
            lbl3.Text = "";
            BtnImportAttachment1.Visible = true;
            BtnImportAttachment2.Visible = true;
            BtnImportAttachment3.Visible = true;
            BtnAddAttachmentsToConfiguredOrder.Visible = false;
            BtnResetAttachments.Visible = false;
            AttachmentManager.Reset();
        }

        private void BtnAddAttachmentsToConfiguredOrder_Click(object sender, EventArgs e)
        {
            AttachmentManager.AddAttachmentsToConfiguredOrder();
            this.Close();
        }

        private void BtnCancelAttachments_Click(object sender, EventArgs e)
        {
            AttachmentManager.Reset();
            this.Close();
        }
    }
}
