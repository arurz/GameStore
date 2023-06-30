using GameStoreApi.Data.DomainValidation.Enums;
using GameStoreApi.Data.DomainValidation.Services;
using GameStoreApi.Application.Users.Admin.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameStoreApi.Data.Users.Admin.Services
{
	public class AdminService : IAdminService
	{
		private readonly AppDbContext context;

		public AdminService(AppDbContext context) 
		{
			this.context = context;
		}

		public async Task<Game> CreateGame(Game game)
		{
			if (await IsGameExists(game.Name))
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.Game_AlreadyExists);
			}

			await context.Games.AddAsync(game);
			await context.SaveChangesAsync();
		
			return game;
		}

		public async Task DeactivateGame(int id)
		{
			var game = await context.Games.SingleOrDefaultAsync(x => x.Id == id);
			game.IsActive = false;

			await context.SaveChangesAsync();
		}

		public async Task<List<Game>> GetAllGames() => await context.Games.ToListAsync();

		public async Task<Game> GetGame(int id) => await context.Games
		   //.Include(g => g.Comments)
		   //.Include(x => x.Carts)
		   //.Include(x => x.GameTypes)
		   .SingleOrDefaultAsync(x => x.Id == id);

		public async Task<List<GameNameIdDto>> GetGameNameIdDtos()
		{
			var dtos = new List<GameNameIdDto>();
			var games = await context.Games.ToListAsync();
			foreach (var game in games)
			{
				var dto = new GameNameIdDto()
				{
					Id = game.Id,
					Name = game.Name
				};
				dtos.Add(dto);
			}
			return dtos;
		}

		public async Task<bool> IsGameExists(string name)
		{
			var games = await context.Games.Where(g => g.Name == name)
				.ToListAsync();
			if (games.Any())
				return true;

			return false;
		}

		public async Task UpdateGame(Game newGame)
		{
			context.Games.Update(newGame);
			await context.SaveChangesAsync();
		}
	}
}
