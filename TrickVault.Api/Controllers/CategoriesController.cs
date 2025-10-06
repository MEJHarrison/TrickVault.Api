using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.Models;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(TrickVaultDbContext context) : ControllerBase
    {
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoriesDto>>> GetCategories()
        {
            var categories = await context.Categories
                .AsNoTracking()
                .Select(c => new GetCategoriesDto(c.Id, c.Name, c.Description))
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategory(int id)
        {
            var category = await context.Categories
                .AsNoTracking()
                .Select(c => new GetCategoryDto(c.Id, c.Name, c.Description))
                .SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> PostCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.Id },
                new GetCategoryDto(
                    category.Id,
                    category.Name,
                    category.Description
                )
            );
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id != updateCategoryDto.Id)
            {
                return BadRequest();
            }

            var category = await context.Categories
                .SingleOrDefaultAsync(c => c.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExistsAsync(id))
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CategoryExistsAsync(int id)
        {
            return await context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
