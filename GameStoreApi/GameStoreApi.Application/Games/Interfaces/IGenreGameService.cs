using GameStoreApi.Data.Games;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Interfaces
{
	public interface IGenreGameService
	{
		Task<List<Game>> GetGamesByGenre(int genreId);

		Task<GenreGame> AddGenre(GenreGame genreGame);

		Task RemoveGenreGame(GenreGame GenreGame);

		Task<GenreGame> UpdateGenreGame(int gameId, int typeId, int newTypeId);
	}
}
