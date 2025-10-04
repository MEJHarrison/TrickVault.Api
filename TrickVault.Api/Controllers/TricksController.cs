using Microsoft.AspNetCore.Mvc;
using TrickVault.Api.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrickVault.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksController : ControllerBase
    {
        private static List<Trick> tricks = new List<Trick>
        {
            new Trick { Id = 1, Title = "Pull Rabbit from Hat", Effect = "The magician shows an empty hat, then pulls a rabbit out of it", Setup = "Put rabbit in hat", Method = "Magician shows an empty hat, then reaches into secret compartment and removes hidden rabbit", Patter = "Look, the hat is completely empty. Except for this here ribbit!", Comments = "This is great with children.", Credits = "Many poor magicians." },
            new Trick { Id = 2, Title = "Pick a Card", Effect = "Magician has audiende member select a random card. The card is shuffled back into the deck. Then the magician finds the selected card.", Method = "Spectator selects a card. The card is returned to the deck and the deck is then shuffled. Then through secret means (not given here), the magician is able to find the selected card.", Patter = "Pick a card, any card! Show the audience. Now put it back anywhere in the deck. I'm going to shuffle the cards a few times. Now, I couldn't possibly know the location of your card, rght? Yet here it is!" }
        };

        // GET: api/<TricksController>
        [HttpGet]
        public ActionResult<IEnumerable<Trick>> Get()
        {
            return Ok(tricks);
        }

        // GET api/<TricksController>/5
        [HttpGet("{id}")]
        public ActionResult<Trick> Get(int id)
        {
            var trick = tricks.FirstOrDefault(t => t.Id == id);

            if (trick is null)
            {
                return NotFound();
            }

            return Ok(trick);
        }

        // POST api/<TricksController>
        [HttpPost]
        public ActionResult<Trick> Post([FromBody] Trick newTrick)
        {
            if (tricks.Any(t => t.Id == newTrick.Id))
            {
                return BadRequest("That trick already exists.");
            }

            tricks.Add(newTrick);

            return CreatedAtAction(nameof(Get), new { id = newTrick.Id }, newTrick);
        }

        // PUT api/<TricksController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Trick trick)
        {
            var existingTrick = tricks.FirstOrDefault(trick => trick.Id == id);

            if (existingTrick is null)
            {
                return NotFound(new { message = "Trick not found." });
            }

            existingTrick.Title = trick.Title;
            existingTrick.Effect = trick.Effect;
            existingTrick.Setup = trick.Setup;
            existingTrick.Method = trick.Method;
            existingTrick.Patter = trick.Patter;
            existingTrick.Comments = trick.Comments;
            existingTrick.Credits = trick.Credits;

            return NoContent();
        }

        // DELETE api/<TricksController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var trick = tricks.FirstOrDefault(trick => trick.Id == id);

            if (trick is null)
            {
                return NotFound(new { message = "Trick not found." });
            }

            tricks.Remove(trick);

            return NoContent();
        }
    }
}