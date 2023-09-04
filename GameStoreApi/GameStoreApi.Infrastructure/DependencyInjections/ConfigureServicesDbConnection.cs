using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStoreApi.Infrastructure.DependencyInjections
{
	public static class ConfigureServicesDbConnection
	{
		public static IServiceCollection AddDBConnections(this IServiceCollection services, IConfiguration configuration)
		{
			//services.AddDbContext<AppDbContext>(
			//	options => options.UseNpgsql(configuration["DBConnection:AppConnectionString"]));

			//services.AddDbContext<AppLogContext>(
			//	options => options.UseNpgsql(configuration["DBConnection:LogConnectionString"]));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["ConnectionString:GameStoreAppDb:SqlDb"]));
            services.AddDbContext<AppLogContext>(options => options.UseSqlServer(configuration["DBConnection:LogConnectionString"]));

            return services;
		}
	}
}
