using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService gameService;
        public GameController(GameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public ActionResult<List<Game>> GetAll() => gameService.GetAllGames();

        [HttpGet("get/{id}")]
        public ActionResult<Game> GetGame([FromRoute] int id) => gameService.GetGameById(id);

        [HttpPost("create")]
        public IActionResult CreateGame([FromBody]Game game)
        {
            gameService.CreateGame(game);
            return CreatedAtAction(nameof(CreateGame), new { id = game.Id }, game);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteGame([FromRoute] int id)
        {
            gameService.RemoveGameById(id);

            return NoContent();
        }

        [HttpPut("update")]
        public IActionResult UpdateGame([FromBody] Game game)
        {
            gameService.UpdateGame(game.Id, game);
            return Ok(game);
        }

        [HttpPatch("update/{id}/description")]
        public IActionResult UpdateGameDesctiption([FromRoute]int id, [FromBody] string description)
        {
            gameService.UpdateGameDescripion(id, description);

            return Ok(description);
        }
    }
}
