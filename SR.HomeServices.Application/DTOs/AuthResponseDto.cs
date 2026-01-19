namespace SR.HomeServices.Application.DTOs
{
    /// <summary>
    /// Represents the response returned after a successful authentication.
    /// </summary>
    /// <remarks>
    /// This DTO contains the JWT token and its expiry time. It is typically
    /// returned by login endpoints to allow clients to authenticate
    /// subsequent requests.
    /// </remarks>
    public class AuthResponseDto
    {
        /// <summary>
        /// Gets or sets the JSON Web Token (JWT) for authenticated requests.
        /// </summary>
        /// <remarks>
        /// This token should be included in the Authorization header
        /// of subsequent API requests.
        /// </remarks>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expiry date and time of the JWT.
        /// </summary>
        /// <remarks>
        /// Clients should refresh or re-authenticate before this time
        /// to maintain access.
        /// </remarks>
        public DateTime Expiry { get; set; }
    }
}
