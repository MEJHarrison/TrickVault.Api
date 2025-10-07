using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;

namespace TrickVault.Api.Services
{
    public class TricksService(TrickVaultDbContext context) : ITricksService
    {
        public async Task<IEnumerable<GetTricksDto>> GetTricksAsync()
        {
            return await context.Tricks
                .AsNoTracking()
                .Select(t => new GetTricksDto(t.Id, t.Title))
                .ToListAsync();
        }

        public async Task<GetTrickDto?> GetTrickAsync(int id)
        {
            var trick = await context.Tricks
                .AsNoTracking()
                .Where(t => t.Id == id)
                .Select(t => new GetTrickDto(
                    t.Id,
                    t.Title,
                    t.Effect,
                    t.Setup,
                    t.Method,
                    t.Patter,
                    t.Comments,
                    t.Credits,
                    t.Categories
                        .Select(c => new GetCategoryDto(c.Id, c.Name, c.Description))
                        .ToList()
                ))
                .FirstOrDefaultAsync();

            return trick;
        }

        public async Task<GetTrickDto> CreateTrickAsync(CreateTrickDto createTrickDto)
        {
            if (await TrickExistsAsync(createTrickDto.Title))
            {
                throw new InvalidOperationException($"A trick with the name '{createTrickDto.Title}' already exists.");
            }

            var trick = new Trick
            {
                Title = createTrickDto.Title,
                Effect = createTrickDto.Effect,
                Setup = createTrickDto.Setup,
                Method = createTrickDto.Method,
                Patter = createTrickDto.Patter,
                Comments = createTrickDto.Comments,
                Credits = createTrickDto.Credits,
            };

            if (createTrickDto.CategoryIds.Any())
            {
                var categories = await context.Categories
                    .AsNoTracking()
                    .Where(c => createTrickDto.CategoryIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var category in categories)
                {
                    trick.Categories.Add(category);
                }
            }

            context.Tricks.Add(trick);

            await context.SaveChangesAsync();

            return new GetTrickDto(
                trick.Id,
                trick.Title,
                trick.Effect,
                trick.Setup,
                trick.Method,
                trick.Patter,
                trick.Comments,
                trick.Credits,
                trick.Categories
                    .Select(c => new GetCategoryDto(c.Id, c.Name, c.Description))
                    .ToList()
            );
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

            trick.Title = updateTrickDto.Title;
            trick.Effect = updateTrickDto.Effect;
            trick.Setup = updateTrickDto.Setup;
            trick.Method = updateTrickDto.Method;
            trick.Patter = updateTrickDto.Patter;
            trick.Comments = updateTrickDto.Comments;
            trick.Credits = updateTrickDto.Credits;

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
