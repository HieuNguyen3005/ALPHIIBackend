using ALPHII.Models.Domain;
using ALPHII.Models.DTO;
using ALPHII.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALPHII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<ApplicationUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            //var identityUser = new IdentityUser
            //{
            //    UserName = registerRequestDto.UserName,
            //    Email = registerRequestDto.UserName
            //};

            var identityUser = new ApplicationUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName,
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Credit = registerRequestDto.Credit
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            // Add roles to this User
            if(identityResult.Succeeded) {

                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("Something was wrong!");
        }

        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null) {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if(checkPasswordResult)
                {
                    // Get roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            jwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                    return Ok("Login successfully");
                }
            
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
