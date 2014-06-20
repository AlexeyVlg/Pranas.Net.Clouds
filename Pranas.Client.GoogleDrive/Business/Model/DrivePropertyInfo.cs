using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a key-value pair that is either public or private to an applicatio
    /// </summary>
    [DataContract]
    public class DrivePropertyInfo : IDriveEntity
    {
        /// <summary>
        /// Gets or sets the ETag of the property.
        /// </summary>
        [DataMember(Name = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the key of this property.
        /// </summary>
        [DataMember(Name = "key")]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the kind of the object. This is always drive#property.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the link back to this property.
        /// </summary>
        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }

        /// <summary>
        /// Gets or sets the value of this property.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the visibility of this property.
        /// </summary>
        [DataMember(Name = "visibility")]
        public string Visibility { get; set; }
    }
}
