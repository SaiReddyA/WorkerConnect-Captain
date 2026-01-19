using Microsoft.AspNetCore.Mvc;
using SR.HomeServices.Application.DTOs;
using SR.HomeServices.Application.Interfaces;
using SR_HomeServices_Captain.ViewModels;
using System.Threading.Tasks;

namespace SR_HomeServices_Captain.Controllers
{
    /// <summary>
    /// Handles authentication-related actions such as user login.
    /// </summary>
    /// <remarks>
    /// This controller delegates authentication logic to <see cref="IAuthService"/>.
    /// All login operations should be asynchronous and return appropriate results
    /// for the MVC views or API clients.
    /// </remarks>
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The application service responsible for authentication logic.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="authService"/> is null.</exception>
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Handles user login requests.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing the authentication result.
        /// Typically returns <c>Ok</c> with a token or error information.
        /// </returns>
        [HttpPost("login")]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try 
            { 
                if (!ModelState.IsValid)
                {
                    // Return the view with validation errors if request is invalid
                    return View("Index", request);
                }

                var result = await _authService.LoginAsync(request);

                Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Expiry
                });

                // Redirect to Home page after successful login
                return RedirectToAction("Index", "Home");
            }
            catch (UnauthorizedAccessException)
            {
                ModelState.AddModelError("", "Invalid email or password");
                
                return View("Index", new AuthPageViewModel { Login = request });
            }
        }

        /// <summary>
        /// Displays the login page.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> rendering the login view.</returns>
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            await _authService.RegisterAsync(request);
            ViewBag.Success = "Registration successful. Please login.";
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto request)
        {
            await _authService.ForgotPasswordAsync(request.Email);
            ViewBag.Success = "If email exists, reset link sent.";
            return View("Index");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Index");
        }
    }
}
