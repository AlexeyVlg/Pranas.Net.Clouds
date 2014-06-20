using System;
using System.Collections.Generic;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a Drive file system item with common properties.
    /// </summary>
    internal interface IDriveItemInfo
    {
        /// <summary>
        /// Gets or sets the short description of the file.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the indexable text attributes for the file.
        /// </summary>
        DriveIndexableTextInfo IndexableText { get; set; }

        /// <summary>
        /// Gets or sets the group of labels for the file.
        /// </summary>
        DriveLabelsInfo Labels { get; set; }

        /// <summary>
        /// Gets or sets the last time this file was viewed by the user.
        /// </summary>
        DateTime? LastViewedByMeDate { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the file. This is only mutable on update when uploading new content.
        /// This field can be left blank, and the mimetype will be determined from the uploaded content's MIME type.
        /// </summary>
        string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the time this file was modified by anyone.
        /// This is only mutable on update when the setModifiedDate parameter is set.
        /// </summary>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of parent folders which contain this file.
        /// Setting this field will put the file in all of the provided folders.
        /// On insert, if no folders are provided, the file will be placed in the default root folder.
        /// </summary>
        List<DriveParentReferenceInfo> Parents { get; set; }

        /// <summary>
        /// Gets or sets the list of properties.
        /// </summary>
        List<DrivePropertyInfo> Properties { get; set; }

        /// <summary>
        /// Gets or sets the title of the this file. Used to identify file or folder name.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the whether writers can share the document with other users.
        /// </summary>
        bool WritersCanShare { get; set; }

    }
}
