using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Games
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreGameController : ControllerBase
	{
		private readonly IGenreGameService genreGameService;
        public GenreGameController(IGenreGameService genreGameService)
        {
			this.genreGameService = genreGameService;
        }

		[HttpGet("gamesByGenre/{id}")]
		public async Task<List<Game>> GetGamesByGenre(int id) => await genreGameService.GetGamesByGenre(id);

		[HttpPost("add")]
		public async Task<IActionResult> AddGenreToGameAsync([FromBody] GenreGame genreGame)
		{
			await genreGameService.AddGenre(genreGame);
			return CreatedAtAction(nameof(AddGenreToGameAsync), new { id = genreGame.GameId }, genreGame);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteGenreGameAsync([FromBody] GenreGame genreGame)
		{
			await genreGameService.RemoveGenreGame(genreGame);
			return NoContent();
		}
	}
}
