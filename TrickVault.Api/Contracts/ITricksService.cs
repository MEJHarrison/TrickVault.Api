using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Results;

namespace TrickVault.Api.Contracts
{
    public interface ITricksService
    {
        Task<Result<IEnumerable<GetTricksDto>>> GetTricksAsync(string userId);
        Task<Result<GetTrickDto>> GetTrickAsync(int id, string userId);
        Task<Result<GetTrickDto>> CreateTrickAsync(CreateTrickDto createTrickDto, string userId);
        Task<Result> UpdateTrickAsync(int id, UpdateTrickDto updateTrickDto, string userId);
        Task<Result> DeleteTrickAsync(int id, string userId);
        Task<bool> TrickExistsAsync(int id, string userId);
        Task<bool> TrickExistsAsync(string title, string userId);
    }
}