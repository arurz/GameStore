using GameStoreApi.Application.Users.Common.Interfaces;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Common.Services
{
	public class CheckUniqueUsernameService : ICheckUniqueUsername
	{
		private readonly AppDbContext context;
        public CheckUniqueUsernameService(AppDbContext context)
        {
            this.context = context;
        }

		public async Task<bool> IsUsernameUnique(string username)
		{
			var users = await context.Users.Where(u => u.Username == username)
				.ToListAsync();
			if (users.Any())
				return false;

			return true;
		}
	}
}
