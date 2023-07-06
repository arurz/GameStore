using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Games
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameCompanyController : ControllerBase
	{
		private readonly IGameCompanyService gameCompanyService;
        public GameCompanyController(IGameCompanyService gameCompanyService)
        {
            this.gameCompanyService = gameCompanyService;
        }

		[HttpPost("add")]
		public async Task<IActionResult> AddGameCompanyAsync([FromBody] GameCompany gameCompany)
		{
			await gameCompanyService.AddGameToCompany(gameCompany);
			return CreatedAtAction(nameof(AddGameCompanyAsync), new { id = gameCompany.GameId }, gameCompany);
		}

		[HttpGet("gameByCompany/{id}")]
		public async Task<List<Game>> GetGamesOfCompany(int id) => await gameCompanyService.GetGamesOfCompany(id);

		[HttpGet("companyByGame/{id}")]
		public async Task<List<Company>> GetCompaniesOfGame(int id) => await gameCompanyService.GetCompaniesOfGame(id);

		[HttpDelete("delete")]
		public async Task RemoveGameCompany([FromBody] GameCompany gameCompany) => await gameCompanyService.RemoveGameCompany(gameCompany);
	}
}
