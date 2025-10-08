using TrickVault.Api.DTOs.Category;
using TrickVault.Api.Results;

namespace TrickVault.Api.Contracts
{
    public interface ICategoriesService
    {
        Task<Result<IEnumerable<GetCategoriesDto>>> GetCategoriesAsync();
        Task<Result<GetCategoryDto>> GetCategoryAsync(int id);
        Task<Result<GetCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task<Result> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task<Result> DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<bool> CategoryExistsAsync(string name);
    }
}