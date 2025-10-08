using TrickVault.Api.DTOs.Auth;
using TrickVault.Api.Results;

namespace TrickVault.Api.Contracts
{
    public interface IUsersService
    {
        Task<Result<string>> LoginAsync(LoginUserDto loginUserDto);
        Task<Result<RegisteredUserDto>> RegisterAsync(RegisterUserDto registerUserDto);
    }
}