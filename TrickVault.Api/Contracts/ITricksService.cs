using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Results;

namespace TrickVault.Api.Contracts
{
    public interface ITricksService
    {
        Task<Result<IEnumerable<GetTricksDto>>> GetTricksAsync();
        Task<Result<GetTrickDto>> GetTrickAsync(int id);
        Task<Result<GetTrickDto>> CreateTrickAsync(CreateTrickDto createTrickDto);
        Task<Result> UpdateTrickAsync(int id, UpdateTrickDto updateTrickDto);
        Task<Result> DeleteTrickAsync(int id);
        Task<bool> TrickExistsAsync(int id);
        Task<bool> TrickExistsAsync(string title);
    }
}