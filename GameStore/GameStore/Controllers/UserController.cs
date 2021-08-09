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

        [HttpGet("get")]
        public ActionResult<User> GetUserById([FromQuery]int id) => userService.GetUserById(id);

        [HttpGet("login")]
        public ActionResult<User> LogIn(string username, string password) => userService.LogIn(username, password);

        [HttpDelete("delete")]
        public IActionResult DeleteUser([FromQuery]int id)
        {
            userService.RemoveUserById(id);
            return NoContent();
        }

        [HttpPost("register")]
        public IActionResult CreateUser([FromQuery] int id,
                                                    string firstName,
                                                    string lastName,
                                                    string username,
                                                    string email,
                                                    string password,
                                                    string telephoneNumber,
                                                    int roleId)
        {
            var user = userService.CreateUser(id, firstName, lastName, username, email, password, telephoneNumber, roleId);
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPut("updateUser")]
        public IActionResult UpdateUser([FromQuery] int id,
                                                    string firstName,
                                                    string lastName,
                                                    string username,
                                                    string email,
                                                    string password,
                                                    string telephoneNumber,
                                                    int roleId)
        {
            if (id < 1)
                return BadRequest();
            var user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Email = email,
                Password = password,
                TelephoneNumber = telephoneNumber,
                RoleId = roleId
            };
            userService.UpdateUser(id, user);
            return NoContent();
        }

        [HttpPut("changePassword")]
        public IActionResult ChangePassword(string  username, string password)
        {
            var user = userService.LogIn(username, password);
            if (user == null)
                throw new UnauthorizedAccessException();
            else
                userService.UpdateUsersPassword(user.Id, password);
            return NoContent();
        }

    }
}
