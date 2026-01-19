namespace SR.HomeServices.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email)
                   ?? throw new UnauthorizedAccessException("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        var roles = await _roleRepository.GetRolesByUserIdAsync(user.UserId);
        var token = _jwtTokenGenerator.Generate(user, roles);

        return new AuthResponseDto
        {
            Token = token,
            Expiry = DateTime.UtcNow.AddHours(1)
        };
    }

    public async Task RegisterAsync(RegisterRequestDto request)
    {
        var existing = await _userRepository.GetByEmailAsync(request.Email);
        if (existing != null)
            throw new Exception("User already exists");

        var user = new User
        {
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return; // prevent email enumeration

        var token = Guid.NewGuid().ToString("N");
        var hashedToken = BCrypt.Net.BCrypt.HashPassword(token);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null) return; // prevent email enumeration

        var newHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _userRepository.UpdatePasswordAsync(user.UserId, newHash);
    }
}
