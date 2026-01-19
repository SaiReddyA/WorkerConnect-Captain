namespace SR.HomeServices.Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object for user login requests.
    /// </summary>
    /// <remarks>
    /// This DTO is used to transfer login credentials from
    /// the client to the application layer. Validation attributes
    /// ensure that the email and password meet basic requirements.
    /// </remarks>
    public class LoginRequestDto
    {
        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <remarks>
        /// This property is required and must be a valid email address.
        /// </remarks>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <remarks>
        /// This property is required and will be rendered as a password
        /// input in forms to mask user input.
        /// </remarks>
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty ;
    }
}
