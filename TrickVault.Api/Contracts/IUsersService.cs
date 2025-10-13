using TrickVault.Api.DTOs.Auth;
using TrickVault.Api.Results;

namespace TrickVault.Api.Contracts
{
    public interface IUsersService
    {
        Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
        Task<Result<AuthResponseDto>> LoginAsync(LoginUserDto loginUserDto);
        Task<Result<AuthResponseDto>> RefreshTokenAsync(string refreshToken);
    }
}