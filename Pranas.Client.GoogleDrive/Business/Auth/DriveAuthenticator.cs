using RestSharp;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents an authenticator of Google Drive service.
    /// </summary>
    public class DriveAuthenticator
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs a Google Drive authenticator.
        /// </summary>
        /// <param name="getTokenInfo">The function to return a token information.</param>
        public DriveAuthenticator(Func<DriveTokenShortInfo> getTokenInfo)
        {
            GetTokenInfo = getTokenInfo;
            RestAuthenticator = new OAuth2HeaderAuthenticator(this);
        }

        #endregion

        /// <summary>
        /// Gets the function to return a token information.
        /// </summary>
        public Func<DriveTokenShortInfo> GetTokenInfo { get; private set; }

        /// <summary>
        /// Gets the token information.
        /// </summary>
        public DriveTokenShortInfo TokenInfo
        {
            get
            {
                var getTokenInfo = GetTokenInfo;
                var result = getTokenInfo == null ? null : getTokenInfo();
                return result;
            }
        }

        /// <summary>
        /// Gets the OAuth2 authenticator.
        /// </summary>
        internal IAuthenticator RestAuthenticator { get; private set; }
    }
}