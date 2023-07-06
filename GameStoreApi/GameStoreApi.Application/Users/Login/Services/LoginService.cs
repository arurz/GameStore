using GameStoreApi.Application.Hashing;
using GameStoreApi.Application.Users.Login.Interfaces;
using GameStoreApi.Data.DomainValidation.Enums;
using GameStoreApi.Data.DomainValidation.Services;
using GameStoreApi.Data.Users;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Login.Services
{
    public class LoginService : ILoginService
	{
        private readonly AppDbContext context;
        public LoginService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<User> LogIn(string username, string password)
        {
            var user = await context.Users
                .Include(x => x.Role)
                .SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                DomainValidationService.ThrowErrorMessage(ErrorCode.User_InvalidCredentials);
            }

            var hashPassword = Task.Run(() => HashService.GenerateHashString(password, user.Salt));

            if (user.Password != await hashPassword)
            {
                DomainValidationService.ThrowErrorMessage(ErrorCode.User_InvalidCredentials);
            }

            return user;
        }
    }
}
