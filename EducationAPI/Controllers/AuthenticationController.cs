using EducationAPI.Models;
using EducationAPI.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<AppUserEntity> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUserEntity> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserEntity registerUser, string role)
        {
            //Check user exist
            var isUserExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (isUserExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseEntity { Status = "Error", Message = "User already exists!" });
            }

            //Add the user in the database
            AppUserEntity user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseEntity { Status = "Error", Message = "User failed to create!" });
                }
                await _userManager.AddToRoleAsync(user, role);
                return StatusCode(StatusCodes.Status201Created, new ResponseEntity { Status = "Success", Message = "User created successfully!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseEntity { Status = "Error", Message = "This role does not exist!" });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginEntity signinModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signinModel.Username, signinModel.Password, false, false);
            var user = await _userManager.FindByNameAsync(signinModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, signinModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var jwtToken = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("loginTest")]
        public async Task<IActionResult> LoginTest([FromBody] LoginEntity signinModel)
        {
            var authService = new AuthService(_userManager, _signInManager, _roleManager, _configuration);
         
            return Ok(await authService.LoginAsync(signinModel));
        }

        

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
