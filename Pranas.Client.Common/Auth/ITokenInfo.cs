using System;

namespace Net.Pranas.Client.Common.Auth
{
    /// <summary>
    /// Represents OAuth2 token information.
    /// </summary>
    public interface ITokenInfo
    {
        /// <summary>
        /// Gets the access token.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Gets the token type.
        /// </summary>
        string TokenType { get; }

        /// <summary>
        /// Gets the token created UTC time.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the token expiration time.
        /// </summary>
        DateTime? ExpiresAt { get; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        string RefreshToken { get; }
    }
}
