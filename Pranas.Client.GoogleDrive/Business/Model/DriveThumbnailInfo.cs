using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a thumbnail for a file.
    /// </summary>
    [DataContract]
    public class DriveThumbnailInfo
    {
        /// <summary>
        /// Gets or sets the URL-safe Base64 encoded bytes of the thumbnail image.
        /// </summary>
        [DataMember(Name = "Image")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the thumbnail.
        /// </summary>
        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }
    }
}
