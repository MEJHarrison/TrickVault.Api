using Microsoft.AspNetCore.Mvc;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoriesService categoriesService) : BaseApiController
    {
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCategoriesDto>>> GetCategories()
        {
            var result = await categoriesService.GetCategoriesAsync();

            return ToActionResult(result);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategory(int id)
        {
            var result = await categoriesService.GetCategoryAsync(id);

            return ToActionResult(result);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> PostCategory(CreateCategoryDto createCategoryDto)
        {
            var result = await categoriesService.CreateCategoryAsync(createCategoryDto);

            if (!result.IsSuccess)
            {
                return MapErrorsToResponse(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = result.Value!.Id },
                result.Value
            );
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var result = await categoriesService.UpdateCategoryAsync(id, updateCategoryDto);

            return ToActionResult(result);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await categoriesService.DeleteCategoryAsync(id);

            return ToActionResult(result);
        }
    }
}
