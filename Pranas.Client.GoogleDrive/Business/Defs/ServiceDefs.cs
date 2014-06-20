namespace Net.Pranas.Client.GoogleDrive.Business.Defs
{
    /// <summary>
    /// Represents Google Drive service definitions.
    /// </summary>
    internal static class ServiceDefs
    {
        #region Auth

        /// <summary>
        /// Represents definitions of Google Drive authorization.
        /// https://developers.google.com/accounts/docs/OAuth2WebServer
        /// </summary>
        internal class Auth
        {
            /// <summary>
            /// Accounts API location.
            /// </summary>
            public const string AccountsLocation = "https://accounts.google.com";

            /// <summary>
            /// OAuth2 resource location.
            /// </summary>
            public const string OAuth2Resource = "o/oauth2/auth";

            /// <summary>
            /// The resource location to get token information.
            /// </summary>
            public const string TokenResource = "o/oauth2/token";

            /// <summary>
            /// The resource location to revoke a token.
            /// </summary>
            public const string TokenRevokeLocation = "o/oauth2/revoke";

            public const string TokenParameterName = "token";

            public const string ResponseTypeParameterName = "response_type";
            public const string ResponseTypeCodeParameterValue = "code";

            public const string ClientIdParameterName = "client_id";

            public const string ClientSecretParameterName = "client_secret";

            public const string RedirectUriParameterName = "redirect_uri";

            public const string ScopeParameterName = "scope";

            public const string StateParameterName = "state";

            public const string AccessTypeParameterName = "access_type";
            public const string AccessTypeOnlineParameterValue = "online";
            public const string AccessTypeOfflineParameterValue = "offline";

            public const string ApprovalPromptParameterName = "approval_prompt";
            public const string ApprovalPromptForceParameterValue = "force";
            public const string ApprovalPromptAutoParameterValue = "auto";

            public const string LoginHintParameterName = "login_hint";

            public const string IncludeGrantedScopesParameterName = "include_granted_scopes";
            public const string IncludeGrantedScopesTrueParameterValue = "true";
            public const string IncludeGrantedScopesFalseParameterValue = "false";

            public const string CodeParameterName = "code";

            public const string GrantTypeParameterName = "grant_type";
            public const string GrantTypeAuthCodeParameterValue = "authorization_code";
            public const string GrantTypeRefreshTokenParameterValue = "refresh_token";

            public const string RefreshTokenParameterName = "refresh_token";
        }

        #endregion

        #region Drive

        /// <summary>
        /// Represents definitions of Google Drive service.
        /// </summary>
        internal class Drive
        {
            /// <summary>
            /// The default location of Google Drive service.
            /// </summary>
            public const string ApiLocation = "https://www.googleapis.com";

            /// <summary>
            /// The Drive REST API version.
            /// </summary>
            public const string ApiVersion = "v2";

            /// <summary>
            /// The resource location of information about the current user along with Drive API settings.
            /// </summary>
            public const string DriveAboutResource = "drive/{version}/about";

            /// <summary>
            /// The resource location of files action.
            /// </summary>
            public const string DriveFilesResource = "drive/{version}/files";

            /// <summary>
            /// The resource location of uploading files action.
            /// </summary>
            public const string DriveUploadFilesResource = "upload/drive/{version}/files";

            /// <summary>
            /// The resource location of downloading files action.
            /// </summary>
            public const string DriveFileDownloadResource = "drive/{version}/files/{fileId}";

            /// <summary>
            /// The resouce location of deleting files action.
            /// </summary>
            public const string DriveDeleteFilesResource = "drive/{version}/files/{fileId}";

            /// <summary>
            /// The resource location of trash files action.
            /// </summary>
            public const string DriveTrashFilesResource = "drive/{version}/files/{fileId}/trash";

            /// <summary>
            /// The resource location of untrush files action.
            /// </summary>
            public const string DriveUntrashFilesResource = "drive/{version}/files/{fileId}/untrash";

            /// <summary>
            /// The resource location of touch files action.
            /// </summary>
            public const string DriveTouchFileResource = "drive/{version}/files/{fileId}/touch";

            /// <summary>
            /// The name of parameter with REST API version.
            /// </summary>
            public const string ApiVersionParameterName = "version";

            /// <summary>
            /// The name of parameter with a file id.
            /// </summary>
            public const string FileIdParameterName = "fileId";
        }

        #endregion
    }
}