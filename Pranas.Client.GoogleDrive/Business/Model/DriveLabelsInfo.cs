using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a group of labels for a file.
    /// </summary>
    [DataContract]
    public class DriveLabelsInfo
    {
        /// <summary>
        /// Gets or sets the whether viewers are prevented from downloading a file.
        /// </summary>
        [DataMember(Name = "restricted")]
        public bool Restricted { get; set; }

        /// <summary>
        /// Gets or sets the whether a file is starred by a user.
        /// </summary>
        [DataMember(Name = "starred")]
        public bool Starred { get; set; }

        /// <summary>
        /// Gets or sets the whether a file has been trashed.
        /// </summary>
        [DataMember(Name = "trashed")]
        public bool Trashed { get; set; }

        /// <summary>
        /// Gets or sets the whether a file has been viewed by a current user.
        /// </summary>
        [DataMember(Name = "viewed")]
        public bool Viewed { get; set; }
    }
}
