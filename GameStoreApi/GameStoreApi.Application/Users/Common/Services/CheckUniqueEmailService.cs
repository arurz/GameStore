using GameStoreApi.Application.Users.Common.Interfaces;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Common.Services
{
	public class CheckUniqueEmailService : ICheckUniqueEmail
	{
		private readonly AppDbContext context;
        public CheckUniqueEmailService(AppDbContext context)
        {
			this.context = context;
        }
        public async Task<bool> IsEmailUnique(string email)
		{
			var users = await context.Users.Where(u => u.Email == email)
				.ToListAsync();
			if (users.Any())
				return false;

			return true;
		}
	}
}
