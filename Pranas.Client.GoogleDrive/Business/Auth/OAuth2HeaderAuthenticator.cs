using Net.Pranas.Client.GoogleDrive.Resources;
using RestSharp;
using System;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents OAuth2 authenticator with HTTP headers.
    /// </summary>
    internal class OAuth2HeaderAuthenticator : IAuthenticator
    {
        #region Construction and Initialization

        /// <summary>
        /// Constructs an OAuth2 authenticator with HTTP headers.
        /// </summary>
        /// <param name="googleDriveAuthenticator">The Google Drive authenticator.</param>
        internal OAuth2HeaderAuthenticator(DriveAuthenticator googleDriveAuthenticator)
        {
            GoogleDriveAuthenticator = googleDriveAuthenticator;
        }

        #endregion

        /// <summary>
        /// Gets the Google Drive authenticator.
        /// </summary>
        public DriveAuthenticator GoogleDriveAuthenticator { get; private set; }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            const string headerFormat = "{0} {1}";
            var authenticator = GoogleDriveAuthenticator;
            var tokenInfo = authenticator.TokenInfo;

            if (tokenInfo == null)
            {
                throw new InvalidOperationException(LocalStrings.AuthenticationErrorMessage, new NullReferenceException(LocalStrings.TokenNullReferenceErrorMessage));
            }

            string headerValue = string.Format(headerFormat, tokenInfo.TokenType, tokenInfo.AccessToken);
            request.AddParameter(AuthorizationHeaderName, headerValue, ParameterType.HttpHeader);
        }

        /// <summary>
        /// The HTTP header name with authorization data.
        /// </summary>
        public const string AuthorizationHeaderName = "Authorization";
    }
}
