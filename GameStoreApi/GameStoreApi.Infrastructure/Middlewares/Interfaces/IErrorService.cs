using GameStoreApi.Data.Errors;
using System.Threading.Tasks;

namespace GameStoreApi.Infrastructure.Middlewares.Interfaces
{
	public interface IErrorService
	{
		Task AddError(Error error);

	}
}
