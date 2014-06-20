using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a structure of a file list.
    /// </summary>
    [DataContract]
    public class DriveFileListInfo : IDriveEntity, IDriveData
    {
        /// <summary>
        /// Gets or sets the ETag of the item.
        /// </summary>
        [DataMember(Name = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the link back to this file.
        /// </summary>
        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }

        /// <summary>
        /// Gets or sets page token for the next page of files.
        /// </summary>
        [DataMember(Name = "nextPageToken")]
        public string NextPageToken { get; set; }

        /// <summary>
        /// Gets or sets the link to the next page of files.
        /// </summary>
        [DataMember(Name = "nextLink")]
        public string NextLink { get; set; }

        /// <summary>
        /// Gets or sets the actual list of files.
        /// </summary>
        [DataMember(Name = "items")]
        public List<DriveFileInfo> Items { get; set; }
            
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "errorDescription")]
        public string ErrorDescription { get; set; }
    }
}
