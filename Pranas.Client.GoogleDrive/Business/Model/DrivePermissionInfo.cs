using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Model
{
    /// <summary>
    /// Represents a permission for a file
    /// </summary>
    [DataContract]
    public class DrivePermissionInfo : IDriveEntity
    {
        /// <summary>
        /// Gets or sets additional roles for this user.
        /// </summary>
        [DataMember(Name = "additionalRoles")]
        public List<string> AdditionalRoles { get; set; }

        /// <summary>
        /// Gets or sets the authkey parameter required for this permission.
        /// </summary>
        [DataMember(Name = "authKey")]
        public string AuthKey { get; set; }

        /// <summary>
        /// Gets or sets the domain name of the entity this permission refers to.
        /// This is an output-only field which is populated when the permission type is user, group, or domain.
        /// </summary>
        [DataMember(Name = "domain")]
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user this permission refers to.
        /// This is an output-only field which is populated when the permission type is user
        /// and the given user's Google+ profile privacy settings allow exposing their email address.
        /// </summary>
        [DataMember(Name = "emailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the ETag of the permission.
        /// </summary>
        [DataMember(Name = "etag")]
        public string ETag { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user this permission refers to, and identical to the permissionId in the About and Files resources.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the kind of the object. This is always drive#permission.
        /// </summary>
        [DataMember(Name = "kind")]
        public string Kind { get; set; }

        /// <summary>
        /// Gets or sets the name for this permission.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the link to the profile photo, if available.
        /// </summary>
        [DataMember(Name = "photoLink")]
        public string PhotoLink { get; set; }

        /// <summary>
        /// Gets or sets primary role for this user. Allowed values are:
        /// <list type="bullet"><item>owner</item><item>reader</item><item>writer</item></list>
        /// Acceptable values are:
        /// <list type="bullet"><item>owner</item><item>reader</item><item>writer</item></list>
        /// </summary>
        [DataMember(Name = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the link back to this permission.
        /// </summary>
        [DataMember(Name = "selfLink")]
        public string SelfLink { get; set; }

        /// <summary>
        /// Gets or sets the account type. Allowed values are:
        /// <list type="bullet"><item>user</item><item>group</item><item>domain</item><item>anyone</item></list>
        /// Acceptable values are:
        /// <list type="bullet"><item>anyone</item><item>domain</item><item>group</item><item>user</item></list>
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the email address or domain name for the entity. This is not populated in responses.
        /// You can use the alias me as the value for this property to refer to the current authorized user.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the whether the link is required for this permission.
        /// </summary>
        [DataMember(Name = "withLink")]
        public bool WithLink { get; set; }
    }
}
