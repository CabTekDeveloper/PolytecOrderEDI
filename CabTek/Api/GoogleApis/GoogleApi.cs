

// File added on 23-06-2025 by Wangchuk
// Last modified by Wangchuk on 11-07-2025


using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Sheets.v4;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace PolytecOrderEDI
{
    static class GoogleApi
    {
        private static string[] Scopes { get; } = [DriveService.Scope.Drive, DriveService.Scope.DriveFile, DriveService.Scope.DriveAppsReadonly, SheetsService.Scope.Spreadsheets, SheetsService.Scope.SpreadsheetsReadonly, SheetsService.Scope.DriveFile];
        private static string ApplicationName { get { return "Polytec Order EDI"; } }
        private static UserCredential? _userCredential { get; set; } = null!; // Initialize to null, will be set in the method
        private static DriveService? _driveService { get; set; } = null!; // Initialize to null, will be set in the method
        private static SheetsService? _sheetsService { get; set; } = null!;



        // Property to access the Google Drive service
        public static DriveService DriveService
        {
            get
            {
                _driveService ??= BuildGoogleDriveService().GetAwaiter().GetResult()!;
                return _driveService;
            }
        }

        // Property to access the Google Sheets service
        public static SheetsService SheetsService
        {
            get
            {
                _sheetsService ??= BuildGoogleSheetsService().GetAwaiter().GetResult()!;
                return _sheetsService;
            }
        }



        // Method to build the Google Drive service
        private static async Task<DriveService?> BuildGoogleDriveService()
        {
            try
            {
                _userCredential ??= await BuildOAuthCredentials();
                DriveService service = new(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _userCredential,
                    ApplicationName = ApplicationName
                });
                return service;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building Google Drive Service!\n\n{ex.Message}");
                return null;
            }
        }

        //Method to build Google Sheets service
        private static async Task<SheetsService?> BuildGoogleSheetsService()
        {
            try
            {
                _userCredential ??= await BuildOAuthCredentials();
                SheetsService service = new(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _userCredential,
                    ApplicationName = ApplicationName
                });
                return service;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building Google Sheeets Service!\n\n{ex.Message}");
                return null;
            }
        }


        // Method to build the user credentials for Google API
        private static async Task<UserCredential?> BuildOAuthCredentials()
        {
            try
            {
                var CredentialFilePath = FileAndDirectory.GoogleApiCredentialsFile;
                using (var stream = new FileStream(CredentialFilePath, FileMode.Open, FileAccess.Read))
                {
                    _userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore("GoogleApiToken", true));
                }
                return _userCredential;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building Google Oauth Credentials!\n\n{ex.Message}");
                return null;
            }
        }

    }
}

