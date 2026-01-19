namespace SR.HomeServices.Application.Interfaces;

/// <summary>
/// Authentication business operations
/// </summary>
public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task RegisterAsync(RegisterRequestDto request);
    Task ForgotPasswordAsync(string email);
    Task ResetPasswordAsync(ResetPasswordDto request);
}
