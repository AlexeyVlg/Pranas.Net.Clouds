using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a user information.
    /// </summary>
    [DataContract]
    public class DriveUserInfo : IDriveEntity
    {
        /// <summary>
        /// Gets or sets the kind of the object.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the plain text displayable name for this user.
        /// </summary>
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the whether this user is the same as the authenticated user for whom the request was made.
        /// </summary>
        [DataMember(Name = "isAuthenticatedUser")]
        public bool IsAuthenticatedUser { get; set; }

        /// <summary>
        /// Gets or sets the user's ID as visible in the permissions collection.
        /// </summary>
        [DataMember(Name = "permissionId")]
        public string PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the user's profile picture.
        /// </summary>
        [DataMember(Name = "picture")]
        public DrivePictureInfo Picture { get; set; }
    }
}
