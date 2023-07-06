using GameStoreApi.Application.Hashing;
using GameStoreApi.Application.Users.Common.Interfaces;
using GameStoreApi.Application.Users.Register.Interfaces;
using GameStoreApi.Data.DomainValidation.Enums;
using GameStoreApi.Data.DomainValidation.Services;
using GameStoreApi.Data.Users;
using GameStoreApi.Data.Users.Constants;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Register.Services
{
	public class RegisterService : IRegisterService
	{
		private readonly AppDbContext context;
		private readonly ICheckUniqueUsername checkUniqueUsername;
		private readonly ICheckUniqueEmail checkUniqueEmail;
        public RegisterService(AppDbContext context, ICheckUniqueEmail checkUniqueEmail, ICheckUniqueUsername checkUniqueUsername)
        {
			this.context = context;
			this.checkUniqueUsername = checkUniqueUsername;
			this.checkUniqueEmail = checkUniqueEmail;
        }
        public async Task<User> CreateUser(User user)
		{
			if (!await checkUniqueEmail.IsEmailUnique(user.Email))
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_EmailTaken);
			}

			if (!await checkUniqueUsername.IsUsernameUnique(user.Username))
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_UsernameTaken);
			}

			string salt = "";
			var newPassword = Task.Run(() => HashService.GenerateHashString(user.Password, ref salt));

			user.Password = await newPassword;
			user.Salt = salt;

			user.Role = await context.Roles.SingleOrDefaultAsync(r => r.Alias == RoleAlias.USER_CUSTOMER);

			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();

			return user;
		}
	}
}
