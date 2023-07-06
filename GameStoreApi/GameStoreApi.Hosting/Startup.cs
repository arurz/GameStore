using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GameStoreApi.Infrastructure.DependencyInjections;
using GameStoreApi.Infrastructure.Middlewares;
using System;
using GameStoreApi.Application.Communications.SignalR.Hubs;

namespace GameStoreApi.Hosting
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDBConnections(Configuration);

			services.AddServices();

			services.AddControllers();

			services.AddSignalR();

			services.AddCors();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			app.UseCors(builder => builder.WithOrigins(
				"http://localhost:4200")
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials());

			app.UseMiddleware<ErrorLoggingMiddleware>();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<ChatHub>("/ChatHub");
			});
		}
	}
}
