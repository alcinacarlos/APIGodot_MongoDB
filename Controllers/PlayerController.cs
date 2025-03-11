using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using APIGodot.Models;

namespace APIGodot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMongoCollection<Player> _players;

        public PlayerController(MongoPlayerContext context)
        {
            _players = context.Players;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _players.Find(player => true).ToListAsync();
            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            var player = await _players.Find<Player>(player => player.Id == id).FirstOrDefaultAsync();
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            await _players.InsertOneAsync(player);
            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            var result = await _players.ReplaceOneAsync(p => p.Id == id, player);
            if (result.MatchedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(long id)
        {
            var result = await _players.DeleteOneAsync(player => player.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}