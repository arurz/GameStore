using GameStore.Contexts;
using GameStore.Models;

namespace GameStore.Services
{
    public class RoleService
    {
        public void CreateRole(Role role)
        {
            using(var context = new GameStoreDBContext())
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
