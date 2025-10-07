using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoriesService categoriesService) : ControllerBase
    {
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoriesDto>>> GetCategories()
        {
            var categories = await categoriesService.GetCategoriesAsync();

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategory(int id)
        {
            var category = await categoriesService.GetCategoryAsync(id);

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
            var getCategoryDto = await categoriesService.CreateCategoryAsync(createCategoryDto);

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = getCategoryDto.Id },
                getCategoryDto
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

            await categoriesService.UpdateCategoryAsync(id, updateCategoryDto);

            return NoContent();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await categoriesService.DeleteCategoryAsync(id);

            return NoContent();
        }
    }
}
