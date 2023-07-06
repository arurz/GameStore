using GameStoreApi.Application.Communications.Mail;
using GameStoreApi.Application.Hashing;
using GameStoreApi.Application.Users.Common.Interfaces;
using GameStoreApi.Application.Users.Users.Interfaces;
using GameStoreApi.Data.DomainValidation.Enums;
using GameStoreApi.Data.DomainValidation.Services;
using GameStoreApi.Data.Users;
using GameStoreApi.Data.Users.Dtos;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Users.Services
{
	public class UserService : IUserService
	{
		private readonly AppDbContext context;
		private readonly MailService mailService;
		private readonly ICheckUniqueUsername checkUniqueUsername;
		private readonly ICheckUniqueEmail checkUniqueEmail;

		public UserService(AppDbContext context, MailService mailService,
						   ICheckUniqueUsername checkUniqueUsername,
						   ICheckUniqueEmail checkUniqueEmail)
        {
			this.context = context;
			this.mailService = mailService;
			this.checkUniqueUsername = checkUniqueUsername;
			this.checkUniqueEmail = checkUniqueEmail;
        }

        public async Task<User> GetUser(int id) => await context.Users
			.Include(x => x.Comments)
			.Include(x => x.Carts)
			.SingleOrDefaultAsync(x => x.Id == id);

		public async Task<List<User>> GetUsers() => await context.Users.ToListAsync();

		public async Task<UserSettingsDto> GetUserSettings(int userId)
		{
			var user = await context.Users.SingleOrDefaultAsync(u => u.Id == userId);
			var dto = new UserSettingsDto()
			{
				Id = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Username = user.Username,
				Email = user.Email,
				TelephoneNumber = user.TelephoneNumber
			};

			return dto;
		}

		public async Task RemoveUser(int id)
		{
			var user = await context.Users.SingleOrDefaultAsync(x => x.Id == id);

			context.Users.Remove(user);
			await context.SaveChangesAsync();
		}

		public async Task RestorePassword(string email)
		{
			var user = await context.Users.SingleOrDefaultAsync(u => u.Email == email);
			if (user == null)
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_NotFound);
			}

			var responseFromPasswordRestore = await GeneratePasswordForRestore(user);
			await mailService.SendMessage(user.Id, "Password restore", "your password has been changed to: " + responseFromPasswordRestore.Item2);
		}

		public async Task UpdateUser(User user)
		{
			if (!await checkUniqueEmail.IsEmailUnique(user.Email))
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_EmailTaken);
			}

			if (!await checkUniqueUsername.IsUsernameUnique(user.Username))
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_UsernameTaken);
			}

			context.Users.Update(user);
			await context.SaveChangesAsync();
		}

		public async Task UpdateUsersPassword(NewPasswordDto dto)
		{
			var user = await context.Users.SingleOrDefaultAsync(u => u.Id == dto.Id);
			var oldPassword = Task.Run(() => HashService.GenerateHashString(dto.OldPassword, user.Salt));

			if (user.Password != oldPassword.Result)
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_ChangePasswordOldPasswordMismatch);
			}

			if (dto.NewPassword != dto.RepeatNewPassword)
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_ChangePasswordNewPasswordMismatch);
			}

			var newSalt = "";
			var newPassword = Task.Run(() => HashService.GenerateHashString(dto.NewPassword, ref newSalt));

			user.Password = await newPassword;
			user.Salt = newSalt;

			await context.SaveChangesAsync();
		}
		
		public async Task<Tuple<User,string>> GeneratePasswordForRestore(User user)
		{
			var tempPassword = HashService.GenerateSalt();
			var salt = "";
			var newPassword = Task.Run(() => HashService.GenerateHashString(tempPassword, ref salt));

			user.Password = await newPassword;
			user.Salt = salt;

			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();

			return Tuple.Create(user, tempPassword);
		}
	}
}
