using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.Models;

namespace TrickVault.Api.Services
{
    public class CategoriesService(TrickVaultDbContext context) : ICategoriesService
    {
        public async Task<IEnumerable<GetCategoriesDto>> GetCategoriesAsync()
        {
            return await context.Categories
                .AsNoTracking()
                .Select(c => new GetCategoriesDto(c.Id, c.Name, c.Description))
                .ToListAsync();
        }

        public async Task<GetCategoryDto?> GetCategoryAsync(int id)
        {
            var category = await context.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => new GetCategoryDto(c.Id, c.Name, c.Description))
                .SingleOrDefaultAsync();

            return category ?? null;
        }

        public async Task<GetCategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (await CategoryExistsAsync(createCategoryDto.Name))
            {
                throw new InvalidOperationException($"A category with the name '{createCategoryDto.Name}' already exists.");
            }

            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            context.Categories.Add(category);
            
            await context.SaveChangesAsync();

            return new GetCategoryDto(
                category.Id,
                category.Name,
                category.Description
            );
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            var nameExists = await context.Categories
                .AnyAsync(c => c.Name == updateCategoryDto.Name && c.Id != id);

            if (nameExists)
            {
                throw new InvalidOperationException($"A category with the name '{updateCategoryDto.Name}' already exists.");
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            await context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await context.Categories.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await context.Categories.AnyAsync(e => e.Name == name);
        }
    }
}
