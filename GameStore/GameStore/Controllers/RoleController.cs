using GameStore.Models;
using GameStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService roleService;
        public RoleController(RoleService roleService)
        {
            this.roleService = roleService;
        }

        [HttpPost("create")]
        public IActionResult AddRole(int id, string name)
        {
            var role = new Role()
            {
                Id = id,
                Name = name
            };
            roleService.CreateRole(role);

            return CreatedAtAction(nameof(AddRole), new { id = role.Id }, role);
        }
    }
}
