using GameStoreApi.Infrastructure.Middlewares.Interfaces;
using GameStoreApi.Infrastructure.Middlewares.Services;
using Microsoft.Extensions.DependencyInjection;
using GameStoreApi.Application.Nomenclatures.Services;
using GameStoreApi.Data.Users;
using GameStoreApi.Application.Nomenclatures.Interfaces;
using GameStoreApi.Data.Games;

namespace GameStoreApi.Infrastructure.DependencyInjections
{
	public static class ConfigureServicesInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IErrorService, ErrorService>();

			services.AddScoped<INomenclatureService<Role>, NomenclatureService<Role>>();			services.AddScoped<INomenclatureService<Role>, NomenclatureService<Role>>();
			services.AddScoped<INomenclatureService<Genre>, NomenclatureService<Genre>>();
			services.AddScoped<INomenclatureService<Company>, NomenclatureService<Company>>();


			return services;
		}
	}
}
