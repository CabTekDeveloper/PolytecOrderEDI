********************************************************************************************************************************************************

File added on 20-06-2025 by Wangchuk
Last modified by Wangchuk on 20-06-2025
Last modified by Wangchuk on 23-06-2025

********************************************************************************************************************************************************

The Google API supports 2 types of credentials: 
1.OAuth 2.0
2.API Key.

The Google APIs in this EDI app will use the OAuth 2.0 credentials.

********************************************************************************************************************************************************

To generate new OAuth 2.0 credentials, follow these steps:
 - Go to the Google Cloud Console. Link: https://console.cloud.google.com > Sign in with the Gmail account 'cabtek87@gmail.com'.
 - Select APIs & Services > Slect the project 'Polytec EDI App'.
 - Click Clients > Click '+ Add secret' to create a new OAuth 2.0 client ID.
 - Download the credentials file in JSON format.
 - Rename the downloaded file to 'GoogleOauthCredentials.json' and copy it to the directory CabTek/Api/GoogleApis.

********************************************************************************************************************************************************

To make the 'GoogleOauthCredentials.json' file be available when publishing the app, follow these steps:
 - In the Solution Explorer, right-click on the 'GoogleOauthCredentials.json' file.
 - Select 'Properties'.
 - In the Properties window, set 'Copy to Output Directory' to 'Copy if newer'.
 - Save the changes.
 - When the app is published, the 'GoogleOauthCredentials.json' file will be copied to the output directory and will be available for use by the Google APIs.

********************************************************************************************************************************************************