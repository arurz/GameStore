using GameStoreApi.Application.Token;
using GameStoreApi.Application.Users.Login.Interfaces;
using GameStoreApi.Data.Users;
using GameStoreApi.Data.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Users
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService loginService;
		private readonly JwtService jwtService;

		public LoginController(ILoginService loginService, JwtService jwtService)
        {
			this.loginService = loginService;
			this.jwtService = jwtService;
        }

		[AllowAnonymous]
		[HttpPost]
		public async Task<ActionResult<User>> LogInAsync([FromBody] LoginDto model)
		{
			var user = await loginService.LogIn(model.Username, model.Password);
			var tokenString = await jwtService.GenerateJWT(user);

			return Ok(new { token = tokenString });
		}
	}
}
