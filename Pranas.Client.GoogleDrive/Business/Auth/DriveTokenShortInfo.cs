using System.Runtime.Serialization;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents a token short information.
    /// </summary>
    [DataContract]
    public class DriveTokenShortInfo
    {
        /// <summary>
        /// Gets or sets access token.
        /// </summary>
        [DataMember]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        [DataMember]
        public string TokenType { get; set; }
    }
}
