using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrickVault.Api.Constants;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Category;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPost]
        [Authorize(Roles = RoleNames.Administrator)]
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
        [HttpPut("{id}")]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> PutCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var result = await categoriesService.UpdateCategoryAsync(id, updateCategoryDto);

            return ToActionResult(result);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleNames.Administrator)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await categoriesService.DeleteCategoryAsync(id);

            return ToActionResult(result);
        }
    }
}
