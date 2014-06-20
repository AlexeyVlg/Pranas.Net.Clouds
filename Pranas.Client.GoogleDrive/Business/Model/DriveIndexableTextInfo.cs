using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents an indexable text attributes for a file.
    /// </summary>
    [DataContract]
    public class DriveIndexableTextInfo
    {
        /// <summary>
        /// Gets or sets the text to be indexed for this file.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
