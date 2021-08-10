using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        public UserController(UserService UserService)
        {
            userService = UserService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll() => userService.GetUsers();

        [HttpGet("get/{id}")]
        public ActionResult<User> GetUserById([FromRoute] int id) => userService.GetUserById(id);

        [HttpGet("login")]
        public ActionResult<User> LogIn([FromBody] string username, string password) => userService.LogIn(username, password);

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            userService.RemoveUserById(id);
            return NoContent();
        }

        [HttpPost("register")]
        public IActionResult CreateUser([FromBody] User user)
        {
            var createdUser = userService.CreateUser(user);
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromBody] User user)
        {
            userService.UpdateUser(user.Id, user);
            return NoContent();
        }

        [HttpPut("changePassword")]
        public IActionResult ChangePassword([FromBody] NewPasswordDto model)
        {
            if (model == null)
                throw new UnauthorizedAccessException();
            else
                userService.UpdateUsersPassword(model.Id, model.NewPassword);
            return NoContent();
        }

    }
}
