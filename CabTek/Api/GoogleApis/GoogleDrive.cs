
// File added on 18-06-2025 by Wangchuk
// Last modified by Wangchuk on 11-07-2025


using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;

namespace PolytecOrderEDI
{
    static class GoogleDrive
    {
        private static DriveService? Service { get; } = GoogleApi.DriveService;
        public static string ErrorMessage { get; private set; } = string.Empty;


        // Method to delete file
        public static async Task<bool> DeleteFile(string fileId)
        {
            try
            {
                if (Service == null) throw new Exception("Google Drive Service is null.");

                await Service.Files.Delete(fileId).ExecuteAsync();
                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error deleting file from google drive.\n\nError details:\n{ex.Message}");
                return false;
            }
        }

        //Method to copy google drive files
        public static async Task<string> CopyFile(string fileToCopyId)
        {
            try
            {
                if (Service == null) throw new Exception("Google Drive Service is null.");

                var copyRequest = Service.Files.Copy(new Google.Apis.Drive.v3.Data.File(), fileToCopyId);
                var copiedFile = await copyRequest.ExecuteAsync();

                return copiedFile.Id;
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show($"Error making copy of file in Goolge Drive.\n\nError details:\n{ex.Message}");
                    return string.Empty;
                }
            }
        }

        public static async Task<bool> ChangeFileAccessToAnyoneWithLink(string fileId)
        {
            try
            {
                if (Service == null) throw new Exception("Google Drive Service is null.");
                var newPermission = new Permission
                {
                    Type = "anyone", // Setting the type to 'anyone' for public access
                    Role = "reader"  // Setting the role to 'reader' for read-only access
                };
                await Service.Permissions.Create(newPermission, fileId).ExecuteAsync();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing file permission!\n\nError details:\n{ex.Message}");
                return false;
            }
        }

        public static async Task<string> GetParentFolderId(string fileId)
        {
            try
            {
                if (Service == null) throw new Exception("Google Drive Service is null.");

                var file =  await Service.Files.Get(fileId).ExecuteAsync();
                return (file.Parents != null && file.Parents.Count > 0) ? file.Parents[0] : string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting parent folder id of a Google drive file!\n\nError details:\n{ex.Message}");
                return string.Empty; 
            }
        }

        public static async Task<bool> MoveFile(string fileId, string oldParentFolderId, string newParentFolderId)
        {
            try
            {
                if (Service == null) throw new Exception("Google Drive Service is null.");

                // Create a request to move the file
                var request = Service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
                request.AddParents = newParentFolderId;
                request.RemoveParents = oldParentFolderId;
                request.Fields = "id, parents";

                // Execute the request
                var file = await request.ExecuteAsync();

                // Check if the file was moved successfully
                if (file.Parents != null && file.Parents.Contains(newParentFolderId))
                {
                    ErrorMessage = string.Empty; 
                    return true;
                }
                else
                {
                    throw new Exception("File not moved successfully.");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error moving file to Google Drive folder\n\nError details:\n{ex.Message}";
                return false;
            }
        }




    }
}

