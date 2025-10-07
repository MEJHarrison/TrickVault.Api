using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.Contracts
{
    public interface ICategoriesService
    {
        Task<IEnumerable<GetCategoriesDto>> GetCategoriesAsync();
        Task<GetCategoryDto?> GetCategoryAsync(int id);
        Task<GetCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteCategoryAsync(int id);
        Task<bool> CategoryExistsAsync(int id);
        Task<bool> CategoryExistsAsync(string name);
    }
}