using GameStoreApi.Application.Users.Register.Interfaces;
using GameStoreApi.Data.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Users
{
	[ApiController]
	[Route("api/[controller]")]
	public class RegisterController : ControllerBase
	{
		private readonly IRegisterService registerService;
		public RegisterController(IRegisterService registerService)
		{
			this.registerService = registerService;
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] User user)
		{
			await registerService.CreateUser(user);

			if (user != null)
				return Ok(user);
			else
				return BadRequest("Register Failed");
		}
	}
}
