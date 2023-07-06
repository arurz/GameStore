using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Services
{
	public class GenreGameService : IGenreGameService
	{
		private readonly AppDbContext context;
        public GenreGameService(AppDbContext context)
        {
			this.context = context;
        }

        public async Task<GenreGame> AddGenre(GenreGame genreGame)
		{
			await context.GameTypes.AddAsync(genreGame);
			await context.SaveChangesAsync();

			return genreGame;
		}

		public async Task<List<Game>> GetGamesByGenre(int genreId) => await context.Games
			.Where(g => g.GameTypes
			.Any(gt => gt.TypeId == genreId))
			.ToListAsync();

		public async Task RemoveGenreGame(GenreGame GenreGame)
		{
			var genreGame = await context.GameTypes.SingleOrDefaultAsync(x => x.GameId == GenreGame.GameId && 
																		 x.TypeId == GenreGame.TypeId);
			context.GameTypes.Remove(genreGame);

			await context.SaveChangesAsync();
		}

		public async Task<GenreGame> UpdateGenreGame(int gameId, int typeId, int newTypeId)
		{
			var genreGame = await context.GameTypes
				.SingleOrDefaultAsync(x => x.GameId == gameId && x.TypeId == typeId);
			context.GameTypes.Remove(genreGame);

			var newGenreGame = new GenreGame
			{
				GameId = gameId,
				TypeId = newTypeId
			};
			await AddGenre(newGenreGame);
			await context.SaveChangesAsync();

			return genreGame;
		}
	}
}
