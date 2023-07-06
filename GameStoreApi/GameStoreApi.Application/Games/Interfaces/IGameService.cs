using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Interfaces
{
	public interface IGameService
	{
		Task<List<GameDto>> GetGamesDto();

		Task<Game> GetGame(int id);

		Task<List<Game>> SearchGames(SearchDto searchDto);
	}
}
