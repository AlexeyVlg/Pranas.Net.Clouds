using Net.Pranas.Client.GoogleDrive.Business.Defs;
using Net.Pranas.Client.GoogleDrive.Business.Interaction;
using Net.Pranas.Client.GoogleDrive.Business.Meta.Ext;
using Net.Pranas.Client.GoogleDrive.Business.Service;
using Net.Pranas.Client.GoogleDrive.Resources;
using RestSharp;
using System;
using System.Net;

namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents authorization at Google Drive service.
    /// </summary>
    public class DriveAuthorization
    {
        #region Constructino and Initialization

        /// <summary>
        /// Constructs instance of Google Drive authorization.
        /// </summary>
        /// <param name="authData">The client's authorization data.</param>
        public DriveAuthorization(IDriveAuthData authData)
        {
            _syncRoot = new object();
            AuthData = authData;
        }

        #region Auth Data

        /// <summary>
        /// Gets the client's authorization data.
        /// </summary>
        public IDriveAuthData AuthData { get; private set; }

        #endregion

        #endregion

        /// <summary>
        /// Gets a URI to authorize on Google Drive service with the application.
        /// </summary>
        /// <param name="state">Provides any state that might be useful to the application upon receipt of the response.
        /// The Google Authorization Server roundtrips this parameter, so the application receives the same value it sent.
        /// Possible uses include redirecting the user to the correct resource in the site, nonces, and cross-site-request-forgery mitigations.</param>
        /// <param name="accessType">Indicates whether the application needs to access a Google API when the user is not present at the browser.</param>
        /// <param name="approvalPrompt">Indicates whether the user should be re-prompted for consent.
        /// The default is auto, so a given user should only see the consent page for a given set of scopes the first time through the sequence.
        /// If the value is force, then the user sees a consent page even if they previously gave consent to the application for a given set of scopes.</param>
        /// <returns>A URI.</returns>
        public Uri GetAuthorizationUri(string state, DriveAccessType accessType = DriveAccessType.Online, DriveApprovalPrompt approvalPrompt = DriveApprovalPrompt.Auto)
        {
            var authData = AuthData;
            string accessTypeStr = accessType.GetStringValue(ServiceDefs.Auth.AccessTypeOnlineParameterValue);
            string approvalPromptStr = approvalPrompt.GetStringValue(ServiceDefs.Auth.ApprovalPromptAutoParameterValue);
            var request = new RestRequest(ServiceDefs.Auth.OAuth2Resource, Method.GET);
            request.AddParameter(ServiceDefs.Auth.ClientIdParameterName, authData.ClientId, ParameterType.QueryString)
                .AddParameter(ServiceDefs.Auth.ResponseTypeParameterName, ServiceDefs.Auth.ResponseTypeCodeParameterValue, ParameterType.QueryString)
                .AddParameter(ServiceDefs.Auth.ScopeParameterName, authData.Scope, ParameterType.QueryString)
                .AddParameter(ServiceDefs.Auth.RedirectUriParameterName, authData.RedirectUri, ParameterType.QueryString)
                .AddParameter(ServiceDefs.Auth.AccessTypeParameterName, accessTypeStr, ParameterType.QueryString)
                .AddParameter(ServiceDefs.Auth.ApprovalPromptParameterName, approvalPromptStr, ParameterType.QueryString);

            if (!string.IsNullOrEmpty(state))
            {
                request.AddParameter(ServiceDefs.Auth.StateParameterName, state, ParameterType.QueryString);
            }

            var client = AccountClient;
            var result = client.BuildUri(request);
            return result;
        }

        /// <summary>
        /// Confirms an authorization code.
        /// </summary>
        /// <param name="code">The authorization code.</param>
        /// <returns>A response with token information.</returns>
        public DriveResponse<DriveTokenInfo> ConfirmAuthorization(string code)
        {
            const string codeArgumentName = "code";

            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(string.Format(LocalStrings.ArgumentNullOrEmptyErrorMessage1, codeArgumentName), codeArgumentName);
            }

            var authData = AuthData;

            var request = new RestRequest(ServiceDefs.Auth.TokenResource, Method.POST);
            request.AddParameter(ServiceDefs.Auth.CodeParameterName, code)
                .AddParameter(ServiceDefs.Auth.ClientIdParameterName, authData.ClientId)
                .AddParameter(ServiceDefs.Auth.ClientSecretParameterName, authData.ClientSecret)
                .AddParameter(ServiceDefs.Auth.RedirectUriParameterName, authData.RedirectUri)
                .AddParameter(ServiceDefs.Auth.GrantTypeParameterName, ServiceDefs.Auth.GrantTypeAuthCodeParameterValue);
            var client = AccountClient;
            var response = RequestHandler.Request<DriveTokenInfo>(client, request, HttpStatusCode.OK);
            var result = new DriveResponse<DriveTokenInfo>(response, t => t.CreatedAt = DateTime.UtcNow);
            return result;
        }

        /// <summary>
        /// Refreshes a token.
        /// </summary>
        /// <param name="tokenInfo">The token information.</param>
        /// <returns>A new token information.</returns>
        public DriveResponse<DriveTokenInfo> RefreshToken(DriveTokenInfo tokenInfo)
        {
            const string tokenInfoArgumentName = "tokenInfo";

            if (tokenInfo == null)
            {
                throw new ArgumentNullException(tokenInfoArgumentName);
            }

            DriveResponse<DriveTokenInfo> result = RefreshToken(tokenInfo.RefreshToken);
            return result;
        }

        /// <summary>
        /// Refreshes a token.
        /// </summary>
        /// <param name="refreshToken">The refresh token value.</param>
        /// <returns>A new token information.</returns>
        public DriveResponse<DriveTokenInfo> RefreshToken(string refreshToken)
        {
            const string refreshTokenArgumentName = "refreshToken";

            if (string.IsNullOrEmpty(refreshToken))
            {
                throw new ArgumentException(string.Format(LocalStrings.ArgumentNullOrEmptyErrorMessage1, refreshTokenArgumentName), refreshTokenArgumentName);
            }

            var authData = AuthData;
            var request = new RestRequest(ServiceDefs.Auth.TokenResource, Method.POST);
            request.AddParameter(ServiceDefs.Auth.RefreshTokenParameterName, refreshToken)
                .AddParameter(ServiceDefs.Auth.ClientIdParameterName, authData.ClientId)
                .AddParameter(ServiceDefs.Auth.ClientSecretParameterName, authData.ClientSecret)
                .AddParameter(ServiceDefs.Auth.GrantTypeParameterName, ServiceDefs.Auth.GrantTypeRefreshTokenParameterValue);
            var client = AccountClient;
            var response = RequestHandler.Request<DriveTokenInfo>(client, request, HttpStatusCode.OK);
            var result = new DriveResponse<DriveTokenInfo>(response, t =>
            {
                t.CreatedAt = DateTime.UtcNow;

                if (string.IsNullOrEmpty(t.RefreshToken))
                {
                    t.RefreshToken = refreshToken;
                }
            });

            return result;
        }

        /// <summary>
        /// Revokes an access token.
        /// </summary>
        /// <param name="tokenInfo">The token information.</param>
        public void RevokeToken(DriveTokenShortInfo tokenInfo)
        {
            const string tokenInfoArgumentName = "tokenInfo";

            if (tokenInfo == null)
            {
                throw new ArgumentNullException(tokenInfoArgumentName);
            }

            RevokeToken(tokenInfo.AccessToken);
        }

        /// <summary>
        /// Revokes an access token.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        public void RevokeToken(string accessToken)
        {
            const string accessTokenArgumentName = "accessToken";

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException(string.Format(LocalStrings.ArgumentNullOrEmptyErrorMessage1, accessTokenArgumentName), accessTokenArgumentName);
            }

            var request = new RestRequest(ServiceDefs.Auth.TokenRevokeLocation, Method.GET);
            request.AddParameter(ServiceDefs.Auth.TokenParameterName, accessToken, ParameterType.QueryString);
            var client = AccountClient;
            RequestHandler.Request(client, request, HttpStatusCode.OK);
        }

        #region Account Client

        /// <summary>
        /// Gets the account's REST client.
        /// </summary>
        protected IRestClient AccountClient
        {
            get
            {
                if (_accountClient == null)
                {
                    lock (_syncRoot)
                    {
                        if (_accountClient == null)
                        {
                            _accountClient = new RestClient(ServiceDefs.Auth.AccountsLocation);
                        }
                    }
                }

                return _accountClient;
            }
        }

        /// <summary>
        /// The account's REST client.
        /// </summary>
        private volatile IRestClient _accountClient;

        #endregion

        private readonly object _syncRoot;
    }
}