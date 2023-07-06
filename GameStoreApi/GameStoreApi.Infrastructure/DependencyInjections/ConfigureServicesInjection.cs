﻿using GameStoreApi.Infrastructure.Middlewares.Interfaces;
using GameStoreApi.Infrastructure.Middlewares.Services;
using Microsoft.Extensions.DependencyInjection;
using GameStoreApi.Application.Nomenclatures.Services;
using GameStoreApi.Data.Users;
using GameStoreApi.Application.Nomenclatures.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Application.Users.Admin.Interfaces;
using GameStoreApi.Data.Users.Admin.Services;
using GameStoreApi.Application.Users.Register.Services;
using GameStoreApi.Application.Users.Register.Interfaces;

namespace GameStoreApi.Infrastructure.DependencyInjections
{
	public static class ConfigureServicesInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IErrorService, ErrorService>();
			#region Communication Services
			services.AddScoped<MailService>();
			#endregion

			services.AddScoped<INomenclatureService<Role>, NomenclatureService<Role>>();
			services.AddScoped<INomenclatureService<Genre>, NomenclatureService<Genre>>();
			services.AddScoped<INomenclatureService<Company>, NomenclatureService<Company>>();

			services.AddScoped<IAdminService, AdminService>();
			services.AddScoped<ICartService, CartService>();

			services.AddScoped<IRegisterService, RegisterService>();
			#region CheckUniqueProperties Services
			services.AddScoped<ICheckUniqueEmail, CheckUniqueEmailService>();
			services.AddScoped<ICheckUniqueUsername, CheckUniqueUsernameService>();
			#endregion

			return services;
		}
	}
}
