using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Common.Interfaces
{
	public interface ICheckUniqueEmail
	{
		Task<bool> IsEmailUnique(string email);
	}
}
