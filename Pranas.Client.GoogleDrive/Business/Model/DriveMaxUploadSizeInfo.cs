using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a max upload size for a file type.
    /// </summary>
    [DataContract]
    public class DriveMaxUploadSizeInfo
    {
        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the max upload size.
        /// </summary>
        [DataMember(Name = "Size")]
        public long Size { get; set; }
    }
}
