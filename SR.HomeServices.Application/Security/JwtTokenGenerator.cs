namespace SR.HomeServices.Application.Security
{
    /// <summary>
    /// Responsible for generating JSON Web Tokens (JWT) used for authenticating users.
    /// </summary>
    /// <remarks>
    /// This implementation reads JWT configuration values (Key, Issuer, Audience)
    /// from the application configuration and embeds user claims and roles
    /// into the generated token.
    /// </remarks>
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
        /// </summary>
        /// <param name="configuration">
        /// Application configuration used to retrieve JWT settings.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="configuration"/> is null.
        /// </exception>
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Generates a signed JWT for the specified user and assigned roles.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <param name="roles">The roles assigned to the user.</param>
        /// <returns>
        /// A serialized JWT string containing user claims and role claims.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="user"/> or <paramref name="roles"/> is null.
        /// </exception>
        public string Generate(User user, List<string> roles)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (roles is null)
                throw new ArgumentNullException(nameof(roles));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email)

            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var signingCredentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
