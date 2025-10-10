using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Constants;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;
using TrickVault.Api.Results;

namespace TrickVault.Api.Services
{
    public class TricksService(TrickVaultDbContext context, IMapper mapper) : ITricksService
    {
        public async Task<Result<IEnumerable<GetTricksDto>>> GetTricksAsync()
        {
            var tricks = await context.Tricks
                .AsNoTracking()
                .ProjectTo<GetTricksDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<GetTricksDto>>.Success(tricks);
        }

        public async Task<Result<GetTrickDto>> GetTrickAsync(int id)
        {
            var trick = await context.Tricks
                .Include(t => t.TrickCategories)
                .ThenInclude(tc => tc.Category)
                .Where(t => t.Id == id)
                .ProjectTo<GetTrickDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (trick is null)
            {
                return Result<GetTrickDto>.Failure(new Error(ErrorCodes.NotFound, $"Trick '{id}' was not found."));
            }

            return Result<GetTrickDto>.Success(trick);
        }

        public async Task<Result<GetTrickDto>> CreateTrickAsync(CreateTrickDto createTrickDto)
        {
            if (await TrickExistsAsync(createTrickDto.Title))
            {
                return Result<GetTrickDto>.Failure(new Error(ErrorCodes.Conflict, $"Trick with title '{createTrickDto.Title}' already exists."));
            }

            var trick = mapper.Map<Trick>(createTrickDto);

            var categoryResult = await AssignCategoriesAsync(createTrickDto.CategoryIds, trick);
            if (!categoryResult.IsSuccess)
            {
                return Result<GetTrickDto>.Failure(categoryResult.Errors);
            }

            context.Tricks.Add(trick);

            await context.SaveChangesAsync();

            var getTrickDto = mapper.Map<GetTrickDto>(trick);

            return Result<GetTrickDto>.Success(getTrickDto);
        }

        public async Task<Result> UpdateTrickAsync(int id, UpdateTrickDto updateTrickDto)
        {
            if (id != updateTrickDto.Id)
            {
                return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id."));
            }

            var trick = await context.Tricks
                .Include(t => t.TrickCategories)
                .ThenInclude(tc => tc.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trick is null)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Trick '{id}' was not found."));
            }

            var titleExists = await context.Tricks
                .AnyAsync(t => t.Title == updateTrickDto.Title && t.Id != id);

            if (titleExists)
            {
                return Result.Failure(new Error(ErrorCodes.Conflict, $"Trick with title '{updateTrickDto.Title}' already exists."));
            }

            mapper.Map(updateTrickDto, trick);

            trick.TrickCategories.Clear();

            var categoryResult = await AssignCategoriesAsync(updateTrickDto.CategoryIds, trick);
            if (!categoryResult.IsSuccess)
            {
                return Result.Failure(categoryResult.Errors);
            }

            context.Tricks.Update(trick);

            await context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteTrickAsync(int id)
        {
            var affected = await context.Tricks
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            if (affected == 0)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Trick '{id}' was not found."));
            }

            return Result.Success();
        }

        public async Task<bool> TrickExistsAsync(int id)
        {
            return await context.Tricks.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> TrickExistsAsync(string title)
        {
            return await context.Tricks.AnyAsync(e => e.Title.ToLower().Trim() == title.ToLower().Trim());
        }

        private async Task<Result> AssignCategoriesAsync(IEnumerable<int> categoryIds, Trick trick)
        {
            if (!categoryIds.Any())
            {
                return Result.Success();
            }

            var categories = await context.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .ToListAsync();

            var foundIds = categories.Select(c => c.Id).ToHashSet();
            var missingIds = categoryIds.Except(foundIds).ToList();

            if (missingIds.Count > 0)
            {
                return Result.Failure(new Error(ErrorCodes.NotFound, $"The following categories were not found: {string.Join(", ", missingIds)}"));
            }

            foreach (var category in categories)
            {
                trick.TrickCategories.Add(new TrickCategory
                {
                    TrickId = trick.Id,
                    CategoryId = category.Id,
                    Category = category
                });
            }

            return Result.Success();
        }
    }
}
