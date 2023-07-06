using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Services
{
	public class GameService : IGameService
	{
		private readonly AppDbContext context;
        public GameService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Game> GetGame(int id) => await context.Games
			.Include(g => g.Comments)
			.Include(x => x.Carts)
			.Include(x => x.GameTypes)
			.SingleOrDefaultAsync(x => x.Id == id);

		public async Task<List<GameDto>> GetGamesDto()
		{
			var games = await context.Games.ToListAsync();
			var gamesDto = new List<GameDto>();
			foreach (var game in games)
			{
				var gameDto = new GameDto()
				{
					Id = game.Id,
					Name = game.Name,
					Picture = game.Picture,
					Price = game.Price
				};
				gamesDto.Add(gameDto);
			}
			return gamesDto;
		}

		public async Task<List<Game>> SearchGames(SearchDto searchDto)
		{
			var games = context.Games
				.Include(g => g.GameTypes)
				.Include(c => c.GameCompanies)
				.AsQueryable();

			if (!string.IsNullOrEmpty(searchDto.Name))
			{
				games = games.Where(x => x.Name.ToLower().Contains(searchDto.Name.ToLower()));
			}

			if (!string.IsNullOrWhiteSpace(searchDto.Genres))
			{
				foreach (var genre in searchDto.Genres.Split(','))
				{
					int newgenre = int.Parse(genre);
					games = games.Where(x => x.GameTypes.Any(i => i.TypeId == newgenre));
				}
			}

			if (!string.IsNullOrWhiteSpace(searchDto.Companies))
			{
				foreach (var company in searchDto.Companies.Split(','))
				{
					int newcompany = int.Parse(company);
					games = games.Where(x => x.GameCompanies.Any(i => i.CompanyId == newcompany));
				}
			}

			if (searchDto.PriceBottomBound > 0)
			{
				games = games.Where(x => x.Price >= searchDto.PriceBottomBound);
			}

			if (searchDto.PriceTopBound > 0)
			{
				games = games.Where(x => x.Price <= searchDto.PriceTopBound);
			}

			return await games.ToListAsync();
		}
	}
}
