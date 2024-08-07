using System.Security.Claims;
using System.Threading.Tasks;
using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using ALPHII.Repositories;
using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ALPHII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthRepository _authRepository, UserManager<ApplicationUser> userManager)
        {
            this.authRepository = _authRepository;
            this._userManager = userManager;
        }

        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = await authRepository.RegisterAsync(registerRequestDto);

            if(identityUser != null)
            {
                return Ok("User was registered! Please login.");
            }
            else
            {
                return BadRequest("Something was wrong!");
            }    
        }

        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponse = await authRepository.LoginAsync(loginRequestDto);

            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }
            else
            {
                return BadRequest("Username or password incorrect");
            }
        }

        [HttpPost]
        [Route("ChangePassword ")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
        {
            // Get currently loggedin user Id
            var currentUserId = User.Claims.ToList().FirstOrDefault(x => x.Type == "id").Value;

            //Get Identity User details user user manager
            var user = await _userManager.FindByIdAsync(currentUserId);

            // Change password using user manager

            await _userManager.ChangePasswordAsync(user, changePasswordRequestDto.CurrentPassword, changePasswordRequestDto.NewPassword);

            return Ok();
        }


        [HttpGet("loginGoogle")]
        public async Task LoginAsync()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
            RedirectUri = "/api/auth/signin-google"
            });
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type
            });

            return Ok(JsonConvert.SerializeObject(claims));
        }
    }
}
