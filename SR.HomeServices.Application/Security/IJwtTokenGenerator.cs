namespace SR.HomeServices.Application.Security
{
    /// <summary>
    /// Defines a contract for generating JSON Web Tokens (JWT)
    /// used for authenticating and authorizing users.
    /// </summary>
    /// <remarks>
    /// Implementations are responsible for creating signed tokens
    /// that contain user identity information and role-based claims.
    /// </remarks>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates a JWT for the specified user and assigned roles.
        /// </summary>
        /// <param name="user">
        /// The user for whom the token is being generated.
        /// </param>
        /// <param name="roles">
        /// The roles assigned to the user.
        /// </param>
        /// <returns>
        /// A serialized JWT string containing identity and role claims.
        /// </returns>
        string Generate(User user, List<string> roles);
    }
}
