using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a user information and settings.
    /// </summary>
    [DataContract]
    public class DriveAboutInfo : IDriveData, IDriveEntity
    {
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the ETag.
        /// </summary>
        [DataMember(Name = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the name of the current user.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the authenticated user.
        /// </summary>
        [DataMember(Name = "user")]
        public DriveUserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the user's ID as visible in the permissions collection.
        /// </summary>
        [DataMember(Name = "permissionId")]
        public string PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the id of the root folder.
        /// </summary>
        [DataMember(Name = "rootFolderId")]
        public string RootFolderId { get; set; }

        /// <summary>
        /// Gets or sets the total number of quota bytes.
        /// </summary>
        [DataMember(Name = "quotaBytesTotal")]
        public long QuotaBytesTotal { get; set; }

        /// <summary>
        /// Gets or sets the number of quota bytes used by Google Drive.
        /// </summary>
        [DataMember(Name = "quotaBytesUsed")]
        public long QuotaBytesUsed { get; set; }

        /// <summary>
        /// Gets or sets the number of quota bytes used by all Google apps (Drive, Picasa, etc.).
        /// </summary>
        [DataMember(Name = "quotaBytesUsedAggregate")]
        public long QuotaBytesUsedAggregate { get; set; }

        /// <summary>
        /// Gets or sets the number of quota bytes used by trashed items.
        /// </summary>
        [DataMember(Name = "quotaBytesUsedInTrash")]
        public long QuotaBytesUsedInTrash { get; set; }

        /// <summary>
        /// Gets or sets the number of remaining change ids.
        /// </summary>
        [DataMember(Name = "remainingChangeIds")]
        public long RemainingChangeIds { get; set; }

        /// <summary>
        /// Gets or sets the list of of max upload sizes for each file type.
        /// The most specific type takes precedence.
        /// </summary>
        [DataMember(Name = "maxUploadSizes")]
        public List<DriveMaxUploadSizeInfo> MaxUploadSizes { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }
    }
}
