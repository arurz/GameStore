using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Users.Admin.Interfaces
{
	public interface IAdminService
	{
		Task<List<GameNameIdDto>> GetGameNameIdDtos();

		Task<Game> CreateGame(Game game);

		Task<Game> GetGame(int id);

		Task<bool> IsGameExists(string name);

		Task DeactivateGame(int id);

		Task UpdateGame(Game newGame);

		Task<List<Game>> GetAllGames();
	}
}
