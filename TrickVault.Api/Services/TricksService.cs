using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;

namespace TrickVault.Api.Services
{
    public class TricksService(TrickVaultDbContext context, IMapper mapper) : ITricksService
    {
        public async Task<IEnumerable<GetTricksDto>> GetTricksAsync()
        {
            return await context.Tricks
                .AsNoTracking()
                .ProjectTo<GetTricksDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<GetTrickDto?> GetTrickAsync(int id)
        {
            return await context.Tricks
                .Include(t => t.Categories)
                .Where(t => t.Id == id)
                .ProjectTo<GetTrickDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<GetTrickDto> CreateTrickAsync(CreateTrickDto createTrickDto)
        {
            if (await TrickExistsAsync(createTrickDto.Title))
            {
                throw new InvalidOperationException($"A trick with the name '{createTrickDto.Title}' already exists.");
            }

            var trick = mapper.Map<Trick>(createTrickDto);

            if (createTrickDto.CategoryIds.Any())
            {
                var categories = await context.Categories
                    .Where(c => createTrickDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var category in categories)
                {
                    trick.Categories.Add(category);
                }
            }

            context.Tricks.Add(trick);

            await context.SaveChangesAsync();

            return mapper.Map<GetTrickDto>(trick);
        }

        public async Task UpdateTrickAsync(int id, UpdateTrickDto updateTrickDto)
        {
            var trick = await context.Tricks
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trick is null)
            {
                throw new KeyNotFoundException("Trick not found");
            }

            var titleExists = await context.Tricks
                .AnyAsync(t => t.Title == updateTrickDto.Title && t.Id != id);

            if (titleExists)
            {
                throw new InvalidOperationException($"A trick with the title '{updateTrickDto.Title}' already exists.");
            }

            mapper.Map(updateTrickDto, trick);

            trick.Categories.Clear();

            if (updateTrickDto.CategoryIds.Any())
            {
                var categories = await context.Categories
                    .Where(c => updateTrickDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var category in categories)
                {
                    trick.Categories.Add(category);
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteTrickAsync(int id)
        {
            await context.Tricks
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> TrickExistsAsync(int id)
        {
            return await context.Tricks.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> TrickExistsAsync(string title)
        {
            return await context.Tricks.AnyAsync(e => e.Title == title);
        }
    }
}
