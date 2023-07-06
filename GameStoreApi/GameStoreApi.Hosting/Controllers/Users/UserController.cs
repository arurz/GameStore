using GameStoreApi.Application.Users.Users.Interfaces;
using GameStoreApi.Data.Users.Dtos;
using GameStoreApi.Data.Users;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Users
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

		[HttpGet]
		public async Task<ActionResult<List<User>>> GetUsers() => await userService.GetUsers();

		[HttpGet("get/{id}")]
		public async Task<ActionResult<User>> GetUser([FromRoute] int id) => await userService.GetUser(id);

		[HttpGet("{id}")]
		public async Task<UserSettingsDto> GetUserSettings([FromRoute] int id) => await userService.GetUserSettings(id);

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteUser([FromRoute] int id)
		{
			await userService.RemoveUser(id);
			return NoContent();
		}

		[HttpPut("updateUser")]
		public async Task<IActionResult> UpdateUser([FromBody] User user)
		{
			await userService.UpdateUser(user);
			return Ok(user);
		}

		[HttpPut("changePassword")]
		public async Task<IActionResult> ChangePassword([FromBody] NewPasswordDto model)
		{
			await userService.UpdateUsersPassword(model);
			return Ok(model);
		}

		[HttpPost("restorePassword")]
		public async Task<IActionResult> RestorePassword([FromBody] EmailDto emailDto)
		{
			await userService.RestorePassword(emailDto.Email);
			return Ok(emailDto);
		}
	}
}
