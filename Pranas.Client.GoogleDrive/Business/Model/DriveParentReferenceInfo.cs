using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a reference to a file's parent.
    /// </summary>
    [DataContract]
    public class DriveParentReferenceInfo : IDriveEntity
    {
        /// <summary>
        /// Gets or sets the ID of the parent.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the whether or not the parent is the root folder.
        /// </summary>
        [DataMember(Name = "isRoot")]
        public bool IsRoot { get; set; }

        /// <summary>
        /// Gets or sets the kind of the object. This is always drive#parentReference.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the link to the parent.
        /// </summary>
        [DataMember(Name = "parentLink")]
        public string ParentLink { get; set; }

        /// <summary>
        /// Gets or sets the link back to this reference.
        /// </summary>
        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }
    }
}
