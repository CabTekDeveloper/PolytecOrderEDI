
//using BorgEdi.Models;
//using System.IO;

namespace PolytecOrderEDI
{
    static class AttachmentManager
    {
        public static List<Attachment> Attachments { get; set; } = [];
        public static bool AttachmentsAddedToConfiguredOrder { get; set; } = false;

        public static void Reset()
        {
            Attachments.Clear();
            AttachmentsAddedToConfiguredOrder = false ;
        }

        public static bool Import()
        {
            try
            {
                var opf = new OpenFileDialog { InitialDirectory = FileAndDirectory.Downloads };

                if (FileManager.Import(FileAndDirectory.Downloads))
                {
                    Attachments.Add(new Attachment(FileManager.FileName, FileManager.FilePath));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
                return false;
            }
        }


        public static void AddAttachmentsToConfiguredOrder()
        {
            if (Attachments.Count > 0)
            {
                foreach (var file in Attachments)
                {
                    PolytecConfiguredOrder.Order.Attachments.Add(BorgEdi.Models.Attachment.ConstructFromFile(file.FilePath));
                }
                PolytecConfiguredOrder.OutputOrderToXMLfile();
                AttachmentsAddedToConfiguredOrder = true;
            }
        }
    }

    
}
