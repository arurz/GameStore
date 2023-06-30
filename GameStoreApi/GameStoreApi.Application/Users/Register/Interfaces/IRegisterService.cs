using GameStoreApi.Data.Users;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Register.Interfaces
{
	public interface IRegisterService
	{
		Task<User> CreateUser(User user);
	}
}
