using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Games
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
			this.gameService = gameService;
        }

		[HttpGet]
		public async Task<ActionResult<List<GameDto>>> GetGamesDtoAsync() => await gameService.GetGamesDto();

		[HttpGet("{id}")]
		public async Task<ActionResult<Game>> GetGameAsync([FromRoute] int id) => await gameService.GetGame(id);

		[HttpGet("getGamesByParametres")]
		public async Task<ActionResult<List<Game>>> SearchGames([FromQuery] SearchDto searchDto) => await gameService.SearchGames(searchDto);
	}
}
