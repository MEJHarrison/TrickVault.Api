using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Auth;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController(IUsersService usersService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDto>> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var result = await usersService.RegisterAsync(registerUserDto);

            return ToActionResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await usersService.LoginAsync(loginUserDto);

            return ToActionResult(result);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var result = await usersService.RefreshTokenAsync(refreshTokenRequestDto.RefreshToken);

            return ToActionResult(result);
        }
    }
}
