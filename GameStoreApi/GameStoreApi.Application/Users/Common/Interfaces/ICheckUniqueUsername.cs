using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Common.Interfaces
{
	public interface ICheckUniqueUsername
	{
		Task<bool> IsUsernameUnique(string username);
	}
}
