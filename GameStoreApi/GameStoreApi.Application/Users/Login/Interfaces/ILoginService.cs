using GameStoreApi.Data.Users;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Login.Interfaces
{
	public interface ILoginService
	{
		Task<User> LogIn(string username, string password);
	}
}
