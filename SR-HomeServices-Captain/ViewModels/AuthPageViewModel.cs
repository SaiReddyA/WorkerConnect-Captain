using SR.HomeServices.Application.DTOs;

namespace SR_HomeServices_Captain.ViewModels
{
    public class AuthPageViewModel
    {
        public LoginRequestDto Login { get; set; } = new();
        public RegisterRequestDto Register { get; set; } = new();
        public ForgotPasswordRequestDto ForgotPassword { get; set; } = new();
    }
}
