namespace Net.Pranas.Client.GoogleDrive.Business.Auth
{
    /// <summary>
    /// Represents an application authentication data.
    /// </summary>
    public interface IDriveAuthData
    {
        /// <summary>
        /// Gets the client id.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Gets the client secret string.
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// Gets the client's redirect URI.
        /// </summary>
        string RedirectUri { get; }

        /// <summary>
        /// Gets the client scope.
        /// </summary>
        string Scope { get; }
    }
}
