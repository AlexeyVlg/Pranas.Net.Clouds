using Net.Pranas.Client.GoogleDrive.Business.Auth;

namespace Pranas.Client.GoogleDrive.Test.Resources
{
    internal class DriveAuthData : IDriveAuthData
    {
        public string ClientId
        {
            get { return AppClientId; }
        }

        public string ClientSecret
        {
            get { return AppClientSecret; }
        }

        public string RedirectUri
        {
            get { return AppClientRedirectUri; }
        }

        public string Scope
        {
            get
            {
                var result = string.Join(" ", UserInfoEmailScope, DriveFileScope, DriveAllFilesScope);
                return result;
            }
        }

        // TODO Set your Client ID
        private const string AppClientId = "";

        // TODO Set your Client secret
        private const string AppClientSecret = "";

        // TODO Set your Redirect URI
        private const string AppClientRedirectUri = "";

        /// <summary>
        /// The scope of user's email.
        /// </summary>
        internal const string UserInfoEmailScope = "https://www.googleapis.com/auth/userinfo.email";

        /// <summary>
        /// The scope of application's files at Google Drive service.
        /// </summary>
        internal const string DriveFileScope = "https://www.googleapis.com/auth/drive.file";

        /// <summary>
        /// The scope to view and manage the files and documents in your Google Drive.
        /// </summary>
        internal const string DriveAllFilesScope = "https://www.googleapis.com/auth/drive";
    }
}
