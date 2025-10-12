using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrickVault.Api.Constants;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Auth;
using TrickVault.Api.Models;
using TrickVault.Api.Models.Configuration;
using TrickVault.Api.Results;

namespace TrickVault.Api.Services
{
    public class UsersService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtSettings> jwtOptions) : IUsersService
    {
        public async Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var roleExists = await roleManager.RoleExistsAsync(registerUserDto.Role);

            if (!roleExists)
            {
                return Result<RegisteredUserDto>.Failure(new Error(ErrorCodes.Validation, $"The role '{registerUserDto.Role}' does not exist."));
            }

            var user = new ApplicationUser
            {
                Email = registerUserDto.Email,
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                UserName = registerUserDto.Email
            };

            var result = await userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new Error(ErrorCodes.BadRequest, e.Description)).ToArray();

                return Result<RegisteredUserDto>.BadRequest(errors);
            }

            var roleResult = await userManager.AddToRoleAsync(user, registerUserDto.Role);

            if (!roleResult.Succeeded)
            {
                var errors = roleResult.Errors
                    .Select(e => new Error(ErrorCodes.BadRequest, e.Description))
                    .ToArray();

                await userManager.DeleteAsync(user);

                return Result<RegisteredUserDto>.Failure(errors);
            }

            var registeredUser = new RegisteredUserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                Role = registerUserDto.Role
            };

            // Optional: Send confirmation Email

            return Result<RegisteredUserDto>.Success(registeredUser);
        }

        public async Task<Result<string>> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByEmailAsync(loginUserDto.Email);

            if (user is null)
            {
                return Result<string>.Failure(new Error(ErrorCodes.BadRequest, "Invalid Credentials"));
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isPasswordValid)
            {
                return Result<string>.Failure(new Error(ErrorCodes.BadRequest, "Invalid Credentials"));
            }

            var token = await GenerateToken(user);

            return Result<string>.Success(token);
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            // Set basic user claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName)
            };

            // Set user role claims
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            claims = claims.Union(roleClaims).ToList();

            // Set JWT Key credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create an encoded token
            var token = new JwtSecurityToken(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtOptions.Value.DurationInMinutes)),
                signingCredentials: credentials
            );

            // Return token value
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
