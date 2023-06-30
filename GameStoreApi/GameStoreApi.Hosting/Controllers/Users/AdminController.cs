using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStoreApi.Application.Users.Admin.Interfaces;

namespace GameStoreApi.Hosting.Controllers.Users
{
    [ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
			this.adminService = adminService;
        }

		[HttpGet("game/{id}")]
		public async Task<ActionResult<Game>> GetGame([FromRoute] int id) => await adminService.GetGame(id);

		[HttpGet("names")]
		public async Task<ActionResult<List<GameNameIdDto>>> GetGameNames() => await adminService.GetGameNameIdDtos();

		[HttpPost("create")]
		public async Task<IActionResult> CreateGame([FromBody] Game game)
		{
			await adminService.CreateGame(game);
			return CreatedAtAction(nameof(CreateGame), new { id = game.Id }, game);
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeactivateGame([FromRoute] int id)
		{
			await adminService.DeactivateGame(id);

			return Ok();
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateGame([FromBody] Game game)
		{
			await adminService.UpdateGame(game);
			return Ok(game);
		}

	}
}
