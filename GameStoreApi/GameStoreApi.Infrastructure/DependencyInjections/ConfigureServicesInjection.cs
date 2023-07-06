using GameStoreApi.Infrastructure.Middlewares.Interfaces;
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
using GameStoreApi.Application.Users.Common.Interfaces;
using GameStoreApi.Application.Users.Common.Services;
using GameStoreApi.Application.Communications.Mail;
using GameStoreApi.Application.Users.Users.Services;
using GameStoreApi.Application.Users.Users.Interfaces;
using GameStoreApi.Application.Hashing;
using GameStoreApi.Application.Token;
using GameStoreApi.Application.Users.Login.Interfaces;
using GameStoreApi.Application.Users.Login.Services;
using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Application.Games.Services;
using GameStoreApi.Application.Communications.SignalR.Services;
using GameStoreApi.Application.Communications.SignalR.Hubs;

namespace GameStoreApi.Infrastructure.DependencyInjections
{
	public static class ConfigureServicesInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			#region Infrastructure Services
			services.AddScoped<IErrorService, ErrorService>();
			services.AddScoped<HashService>();
			services.AddScoped<JwtService>();
			#endregion

			#region Communication Services
			services.AddScoped<MailService>();
			services.AddScoped<MessageService>();
			services.AddScoped<ChatHub>();
			#endregion

			#region Nomenclature Services
			services.AddScoped<INomenclatureService<Role>, NomenclatureService<Role>>();
			services.AddScoped<INomenclatureService<Genre>, NomenclatureService<Genre>>();
			services.AddScoped<INomenclatureService<Company>, NomenclatureService<Company>>();
			#endregion

			#region Game Services
			services.AddScoped<IGameService, GameService>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<ICommentService, CommentService>();
			services.AddScoped<IGenreGameService, GenreGameService>();
			services.AddScoped<IGameCompanyService, GameCompanyService>();
			#endregion

			#region User Services
			services.AddScoped<IAdminService, AdminService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRegisterService, RegisterService>();
			services.AddScoped<ILoginService, LoginService>();
			#endregion

			#region CheckUniqueProperties Services
			services.AddScoped<ICheckUniqueEmail, CheckUniqueEmailService>();
			services.AddScoped<ICheckUniqueUsername, CheckUniqueUsernameService>();
			#endregion

			return services;
		}
	}
}
