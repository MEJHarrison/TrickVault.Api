using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task<Result<AuthResponseDto>> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await userManager.FindByEmailAsync(loginUserDto.Email);

            if (user is null)
            {
                return Result<AuthResponseDto>.Failure(new Error(ErrorCodes.Unauthorized, "Invalid Credentials"));
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isPasswordValid)
            {
                return Result<AuthResponseDto>.Failure(new Error(ErrorCodes.Unauthorized, "Invalid Credentials"));
            }

            var accessToken = await GenerateToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDurationInDays);

            await userManager.UpdateAsync(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        public async Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken)
        {
            var user = userManager
                .Users
                .FirstOrDefault(u => u.RefreshToken == refreshToken);

            if (user is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Result<AuthResponseDto>.Failure(new Error(ErrorCodes.Unauthorized, "Invalid or expired refresh token"));
            }

            var newAccessToken = await GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenDurationInDays);

            await userManager.UpdateAsync(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
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

        private static string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

    }
}
