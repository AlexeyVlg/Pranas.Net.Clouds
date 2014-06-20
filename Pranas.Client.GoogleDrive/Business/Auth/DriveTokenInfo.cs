using Net.Pranas.Client.Common.Auth;
using Net.Pranas.Client.GoogleDrive.Business.Model;
using System;
using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents a token information.
    /// </summary>
    [DataContract]
    public class DriveTokenInfo : DriveTokenShortInfo, IDriveData, ITokenInfo
    {
        /// <summary>
        /// Gets or sets the token created UTC time.
        /// </summary>
        [DataMember]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the token expiration time in seconds.
        /// </summary>
        [DataMember]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets the token expiration time.
        /// </summary>
        /// <returns>The token expiration time.</returns>
        public DateTime? ExpiresAt
        {
            get
            {
                var result = CreatedAt.AddSeconds(ExpiresIn);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the token id.
        /// </summary>
        [DataMember]
        public string IdToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        [DataMember]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets an error code string.
        /// </summary>
        [DataMember]
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets an error description text.
        /// </summary>
        [DataMember]
        public string ErrorDescription { get; set; }
    }
}
