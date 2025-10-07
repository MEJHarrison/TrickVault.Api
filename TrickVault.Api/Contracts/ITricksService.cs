using TrickVault.Api.DTOs.Trick;

namespace TrickVault.Api.Contracts
{
    public interface ITricksService
    {
        Task<IEnumerable<GetTricksDto>> GetTricksAsync();
        Task<GetTrickDto?> GetTrickAsync(int id);
        Task<GetTrickDto> CreateTrickAsync(CreateTrickDto createTrickDto);
        Task UpdateTrickAsync(int id, UpdateTrickDto updateTrickDto);
        Task DeleteTrickAsync(int id);
        Task<bool> TrickExistsAsync(int id);
        Task<bool> TrickExistsAsync(string name);
    }
}