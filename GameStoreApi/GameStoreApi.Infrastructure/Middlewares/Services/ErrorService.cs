using GameStoreApi.Data.Errors;
using GameStoreApi.Infrastructure.Middlewares.Interfaces;
using GameStoreApi.Persistence;
using System;
using System.Threading.Tasks;

namespace GameStoreApi.Infrastructure.Middlewares.Services
{
	public class ErrorService : IErrorService
	{
		private readonly AppLogContext context;
		public ErrorService(AppLogContext context)
		{
			this.context = context;
		}
		public async Task AddError(Error error)
		{
			error.ErrorAppearanceDateTime = DateTime.Now;

			await context.Errors.AddAsync(error);
			await context.SaveChangesAsync();
		}

	}
}
