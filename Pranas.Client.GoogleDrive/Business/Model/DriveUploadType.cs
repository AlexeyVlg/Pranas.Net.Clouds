using Net.Pranas.Client.GoogleDrive.Business.Meta;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents Google Drive upload types.
    /// </summary>
    public enum DriveUploadType
    {
        [StringValue("media")]
        Media,

        [StringValue("multipart")]
        Multipart,

        [StringValue("resumable")]
        Resumable
    }
}
