using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrickVault.Api.Contracts;
using TrickVault.Api.Data;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController(ITricksService tricksService) : ControllerBase
    {
        // GET: api/Tricks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTricksDto>>> GetTricks()
        {
            var tricks = await tricksService.GetTricksAsync();

            return Ok(tricks);
        }

        // GET: api/Tricks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTrickDto>> GetTrick(int id)
        {
            var trick = await tricksService.GetTrickAsync(id);

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
            var getTrickDto = await tricksService.CreateTrickAsync(createTrickDto);

            return CreatedAtAction(
                nameof(GetTrick),
                new { id = getTrickDto.Id },
                getTrickDto
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

            await tricksService.UpdateTrickAsync(id, updateTrickDto);

            return NoContent();
        }

        // DELETE: api/Tricks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrick(int id)
        {
            await tricksService.DeleteTrickAsync(id);

            return NoContent();
        }
    }
}
