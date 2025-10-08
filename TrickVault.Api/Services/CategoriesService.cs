using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Constants;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.Models;
using TrickVault.Api.Results;

namespace TrickVault.Api.Services
{
    public class CategoriesService(TrickVaultDbContext context, IMapper mapper) : ICategoriesService
    {
        public async Task<Result<IEnumerable<GetCategoriesDto>>> GetCategoriesAsync()
        {
            var catagories = await context.Categories
                .AsNoTracking()
                .ProjectTo<GetCategoriesDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<GetCategoriesDto>>.Success(catagories);
        }

        public async Task<Result<GetCategoryDto>> GetCategoryAsync(int id)
        {
            var category = await context.Categories
                .AsNoTracking()
                .Where(c => c.Id == id)
                .ProjectTo<GetCategoryDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (category is null)
            {
                return Result<GetCategoryDto>.Failure(new Error(ErrorCodes.NotFound, $"Category '{id}' was not found."));
            }

            return Result<GetCategoryDto>.Success(category);
        }

        public async Task<Result<GetCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            if (await CategoryExistsAsync(createCategoryDto.Name))
            {
                return Result<GetCategoryDto>.Failure(new Error(ErrorCodes.Conflict, $"Category with name '{createCategoryDto.Name}' already exists."));
            }

            var category = mapper.Map<Category>(createCategoryDto);

            context.Categories.Add(category);

            await context.SaveChangesAsync();

            var getCategoryDto = mapper.Map<GetCategoryDto>(category);

            return Result<GetCategoryDto>.Success(getCategoryDto);
        }

        public async Task<Result> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id."));
            }

            var category = await context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Category '{id}' was not found."));
            }

            var nameExists = await context.Categories
                .AnyAsync(c => c.Name == updateCategoryDto.Name && c.Id != id);

            if (nameExists)
            {
                return Result.Failure(new Error(ErrorCodes.Conflict, $"Category with name '{updateCategoryDto.Name}' already exists."));
            }

            mapper.Map(updateCategoryDto, category);

            context.Categories.Update(category);

            await context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteCategoryAsync(int id)
        {
            var affected = await context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            if (affected == 0)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Category '{id}' was not found."));
            }

            return Result.Success();
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await context.Categories.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CategoryExistsAsync(string name)
        {
            return await context.Categories.AnyAsync(c => c.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
