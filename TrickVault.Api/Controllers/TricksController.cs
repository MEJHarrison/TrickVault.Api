using Microsoft.AspNetCore.Mvc;
using TrickVault.Api.Contracts;
using TrickVault.Api.DTOs.Trick;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController(ITricksService tricksService) : BaseApiController
    {
        // GET: api/Tricks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTricksDto>>> GetTricks()
        {
            var result = await tricksService.GetTricksAsync();

            return ToActionResult(result);
        }

        // GET: api/Tricks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrickDto>> GetTrick(int id)
        {
            var result = await tricksService.GetTrickAsync(id);

            return ToActionResult(result);
        }

        // POST: api/Tricks
        [HttpPost]
        public async Task<ActionResult<GetTrickDto>> PostTrick(CreateTrickDto createTrickDto)
        {
            var result = await tricksService.CreateTrickAsync(createTrickDto);

            if (!result.IsSuccess)
            {
                return MapErrorsToResponse(result.Errors);
            }

            return CreatedAtAction(
                nameof(GetTrick),
                new { id = result.Value!.Id },
                result.Value
            );
        }

        // PUT: api/Tricks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrick(int id, UpdateTrickDto updateTrickDto)
        {
            var result = await tricksService.UpdateTrickAsync(id, updateTrickDto);

            return ToActionResult(result);
        }

        // DELETE: api/Tricks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrick(int id)
        {
            var result = await tricksService.DeleteTrickAsync(id);

            return ToActionResult(result);
        }
    }
}
