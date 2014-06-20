using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a short information of a remote Google Drive folder.
    /// </summary>
    [DataContract]
    public class DriveFolderShortInfo : DriveItemShortInfo
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a short information of a remote Google Drive folder.
        /// </summary>
        public DriveFolderShortInfo()
        {
            MimeType = FolderMimeType;
        }

        #endregion

        /// <summary>
        /// The default MIME type.
        /// </summary>
        public const string FolderMimeType = "application/vnd.google-apps.folder";
    }
}
