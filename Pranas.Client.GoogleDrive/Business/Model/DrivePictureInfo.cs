using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a user's profile picture.
    /// </summary>
    [DataContract]
    public class DrivePictureInfo
    {
        /// <summary>
        /// Gets or sets the URL that points to a profile picture of this user.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
