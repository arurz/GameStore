using GameStoreApi.Data.Users;
using GameStoreApi.Data.Users.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Users.Interfaces
{
	public interface IUserService
	{
		Task<List<User>> GetUsers();

		Task<User> GetUser(int id);

		Task RemoveUser(int id);

		Task UpdateUser(User user);

		Task UpdateUsersPassword(NewPasswordDto dto);

		Task<UserSettingsDto> GetUserSettings(int userId);

		Task RestorePassword(string email);
	}
}
