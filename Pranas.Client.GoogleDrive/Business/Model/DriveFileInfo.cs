using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents an information about a remote Google Drive file.
    /// </summary>
    [DataContract]
    public class DriveFileInfo : DriveFileShortInfo, IDriveData, IDriveEntity
    {
        /// <summary>
        /// Gets or sets the file id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the ETag of the item.
        /// </summary>
        [DataMember(Name = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the link for opening the file in using a relevant Google editor or viewer.
        /// </summary>
        [DataMember(Name = "alternateLink")]
        public string AlternateLink { get; set; }

        /// <summary>
        /// Gets or sets the whether this file is in the appdata folder.
        /// </summary>
        [DataMember(Name = "appDataContents")]
        public bool AppDataContents { get; set; }

        /// <summary>
        /// Gets or sets the whetehr the file can be copied by the current user.
        /// </summary>
        [DataMember(Name = "copyable")]
        public bool Copyable { get; set; }

        /// <summary>
        /// Gets or sets the time for this file.
        /// </summary>
        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the link to open this file with the user's default app for this file.
        /// Only populated when the drive.apps.readonly scope is used.
        /// </summary>
        [DataMember(Name = "defaultOpenWithLink")]
        public string DefaultOpenWithLink { get; set; }

        /// <summary>
        /// Gets or sets the lived download URL for the file. This is only populated for files with content stored in Drive.
        /// </summary>
        [DataMember(Name = "downloadUrl")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// Gets or sets the whether the file can be edited by the current user.
        /// </summary>
        [DataMember(Name = "editable")]
        public bool Editable { get; set; }

        /// <summary>
        /// Gets or sets the link for embedding the file.
        /// </summary>
        [DataMember(Name = "embedLink")]
        public string EmbedLink { get; set; }

        /// <summary>
        /// Gets or sets the whether this file has been explicitly trashed, as opposed to recursively trashed.
        /// This will only be populated if the file is trashed.
        /// </summary>
        [DataMember(Name = "explicitlyTrashed")]
        public bool ExplicitlyTrashed { get; set; }

        /// <summary>
        /// Gets or sets the links for exporting Google Docs to specific formats.
        /// </summary>
        [DataMember(Name = "exportLinks")]
        public Dictionary<string, string> ExportLinks { get; set; }

        /// <summary>
        /// Gets or sets the file extension used when downloading this file.
        /// </summary>
        [DataMember(Name = "fileExtension")]
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the size of the file in bytes. This is only populated for files with content stored in Drive.
        /// </summary>
        [DataMember(Name = "fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// Gets or sets the ID of the file's head revision. This will only be populated for files with content stored in Drive.
        /// </summary>
        [DataMember(Name = "headRevisionId")]
        public string HeadRevisionId { get; set; }

        /// <summary>
        /// Gets or sets the link to the file's icon.
        /// </summary>
        [DataMember(Name = "iconLink")]
        public string IconLink { get; set; }

        /// <summary>
        /// Gets or sets the metadata about image media.
        /// This will only be present for image types, and its contents will depend on what can be parsed from the image content.
        /// </summary>
        [DataMember(Name = "imageMediaMetadata")]
        public DriveImageMediaMetadataInfo ImageMediaMetadata { get; set; }

        /// <summary>
        /// Gets or sets the last user to modify this file.
        /// </summary>
        [DataMember(Name = "lastModifyingUser")]
        public DriveUserInfo LastModifyingUser { get; set; }

        /// <summary>
        /// Gets the name of the last user to modify this file.
        /// </summary>
        [DataMember(Name = "lastModifyingUserName")]
        public string LastModifyingUserName { get; set; }

        /// <summary>
        /// Gets or sets an MD5 checksum for the content of this file. This is populated only for files with content stored in Drive.
        /// </summary>
        [DataMember(Name = "md5Checksum")]
        public string Md5Checksum { get; set; }

        /// <summary>
        /// Gets or sets the last time this file was modified by the user.
        /// Note that setting modifiedDate will also update the modifiedByMe date for the user which set the date.
        /// </summary>
        [DataMember(Name = "modifiedByMeDate")]
        public DateTime? ModifiedByMeDate { get; set; }

        /// <summary>
        /// Gets or sets the map of the id of each of the user's apps to a link to open this file with that app.
        /// Only populated when the drive.apps.readonly scope is used.
        /// </summary>
        [DataMember(Name = "openWithLinks")]
        public Dictionary<string, string> OpenWithLinks { get; set; }

        /// <summary>
        /// Gets or sets the original filename if the file was uploaded manually, or the original title if the file was inserted through the API.
        /// Note that renames of the title will not change the original filename. This will only be populated on files with content stored in Drive.
        /// </summary>
        [DataMember(Name = "originalFilename")]
        public string OriginalFilename { get; set; }

        /// <summary>
        /// Gets or sets name(s) of the owner(s) of this file.
        /// </summary>
        [DataMember(Name = "ownerNames")]
        public List<string> OwnerNames { get; set; }

        /// <summary>
        /// Gets or sets owner(s) of this file.
        /// </summary>
        [DataMember(Name = "owners")]
        public List<DriveUserInfo> Owners { get; set; }

        /// <summary>
        /// Gets or sets the number of quota bytes used by this file.
        /// </summary>
        [DataMember(Name = "quotaBytesUsed")]
        public long QuotaBytesUsed { get; set; }

        /// <summary>
        /// Gets or sets the link back to this file.
        /// </summary>
        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }

        /// <summary>
        /// Gets or sets the whether the file has been shared.
        /// </summary>
        [DataMember(Name = "shared")]
        public bool Shared { get; set; }

        /// <summary>
        /// Gets or sets the time at which this file was shared with the user.
        /// </summary>
        [DataMember(Name = "sharedWithMeDate")]
        public DateTime? SharedWithMeDate { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail for the file. Only accepted on upload and for files that are not already thumbnailed by Google.
        /// </summary>
        [DataMember(Name = "thumbnail")]
        public DriveThumbnailInfo Thumbnail { get; set; }

        /// <summary>
        /// Gets or sets the link to the file's thumbnail.
        /// </summary>
        [DataMember(Name = "thumbnailLink")]
        public string ThumbnailLink { get; set; }

        /// <summary>
        /// Gets or sets the permissions for the authenticated user on this file.
        /// </summary>
        [DataMember(Name = "userPermission")]
        public DrivePermissionInfo UserPermission { get; set; }

        /// <summary>
        /// Gets or sets the link for downloading the content of the file in a browser using cookie based authentication.
        /// In cases where the content is shared publicly, the content can be downloaded without any credentials.
        /// </summary>
        [DataMember(Name = "webContentLink")]
        public string WebContentLink { get; set; }

        /// <summary>
        /// Gets or sets the link only available on public folders for viewing their static web assets (HTML, CSS, JS, etc) via Google Drive's Website Hosting.
        /// </summary>
        [DataMember(Name = "webViewLink")]
        public string WebViewLink { get; set; }

        /// <summary>
        /// Gets or sets the kind of the object. This is always drive#file.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }
    }
}