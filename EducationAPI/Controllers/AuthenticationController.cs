using AutoMapper;
using EducationAPI.Context;
using EducationAPI.DTOs;
using EducationAPI.Entities;
using EducationAPI.Implement.Repositories;
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
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ApplicationDbContext applicationDbContext, IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
        {
            //Check user exist
            var isUserExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (isUserExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists!" });
            }

            //Add the user in the database
            AppUser user = new()
            {
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerUser.Username,
                UserType = role
            };
             await _userManager.CreateAsync(user, registerUser.Password);
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login signinModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signinModel.Username, signinModel.Password, false, false);
            var user = await _userManager.FindByNameAsync(signinModel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, signinModel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
      /*              new Claim(ClaimTypes.NameIdentifier, user.Id),*/
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
                    userId = user.Id,
                    role = user.UserType,
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

       

        [HttpGet]
        [Route("getUserInfo")]
        public IActionResult GetUserInfo(string userId, string role)
        {
/*            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);*/
            if (role == "Admin")
            {
                var user = new Admin();
                user = applicationDbContext.Admins.SingleOrDefault(x => x.UserId == userId);
                return Ok(mapper.Map<AdminDTO>(user));

            }
            else if (role == "Lecture")
            {
                var user = new Lecture();
                user = applicationDbContext.Lectures.SingleOrDefault(x => x.UserId == userId);
                return Ok(mapper.Map<LectureDTO>(user));

            }
            else
            {
                var user = new Student();
                user = applicationDbContext.Students.SingleOrDefault(x => x.UserId == userId);
                return Ok(mapper.Map<StudentDTO>(user));
            }
        }


        [HttpDelete("/api/user/bulkdelete")]
        public async Task<IActionResult> Delete([FromBody] string[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    var user = await applicationDbContext.AppUsers.FindAsync(id);
                    applicationDbContext.Remove(user);
                    await applicationDbContext.SaveChangesAsync();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
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
