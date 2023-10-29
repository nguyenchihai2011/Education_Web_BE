using AutoMapper;
using EducationAPI.Entities;
using EducationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationAPI.Test
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> LoginAsync(Login request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Login Failed!"
                };
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey,
                    SecurityAlgorithms.HmacSha512Signature)
            );

            return new ApiResponse
            {
                Success = true,
                Message = "Login Successfully!",
                Data = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        /*public async Task<ApiResponse> RegisterAsync(RegistrationRequest request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Email);

            if (userExists != null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "UserName already exist!"
                };
            }

            if (!request.Password.Equals(request.PasswordConfirm))
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Confirm password does not match password"
                };
            }

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                UserType = request.UserType,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User creation failed!"
                };
            }

            var role = request.UserType.ToString();

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return new ApiResponse
            {
                Success = true,
                Message = "User created successfully!"
            };
        }*/

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
