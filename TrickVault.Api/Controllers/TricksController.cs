using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController(TrickVaultDbContext context) : ControllerBase
    {
        // GET: api/Tricks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTricksDto>>> GetTricks()
        {
            var tricks = await context.Tricks
                .AsNoTracking()
                .Select(t => new GetTricksDto(t.Id, t.Title))
                .ToListAsync();

            return Ok(tricks);
        }

        // GET: api/Tricks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrickDto>> GetTrick(int id)
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

            if (trick == null)
            {
                return NotFound();
            }

            return Ok(trick);
        }

        // POST: api/Tricks
        [HttpPost]
        public async Task<ActionResult<GetTrickDto>> PostTrick(CreateTrickDto createTrickDto)
        {
            var trick = new Trick
            {
                Title = createTrickDto.Title,
                Effect = createTrickDto.Effect,
                Setup = createTrickDto.Setup,
                Method = createTrickDto.Method,
                Patter = createTrickDto.Patter,
                Comments = createTrickDto.Comments,
                Credits = createTrickDto.Credits,
                //CategoryIds = createTrickDto.CategoryIds
            };

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

            //return CreatedAtAction("GetTrick", new { id = trick.Id }, trick);
            return CreatedAtAction(
                nameof(GetTrick),
                new { id = trick.Id },
                new GetTrickDto(
                    trick.Id,
                    trick.Title,
                    trick.Effect,
                    trick.Setup,
                    trick.Method,
                    trick.Patter,
                    trick.Comments,
                    trick.Credits,
                    trick.Categories.Select(c => new GetCategoryDto(c.Id, c.Name, c.Description)).ToList()
                )
            );
        }

        // PUT: api/Tricks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrick(int id, UpdateTrickDto updateTrickDto)
        {
            if (id != updateTrickDto.Id)
            {
                return BadRequest();
            }

            var trick = await context.Tricks
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (trick is null)
            {
                return NotFound();
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

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TrickExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Tricks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrick(int id)
        {
            var trick = await context.Tricks.FindAsync(id);
            if (trick == null)
            {
                return NotFound();
            }

            context.Tricks.Remove(trick);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TrickExistsAsync(int id)
        {
            return await context.Tricks.AnyAsync(e => e.Id == id);
        }
    }
}
