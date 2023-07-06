using GameStoreApi.Data.Games;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Interfaces
{
	public interface IGameCompanyService
	{
		Task<GameCompany> AddGameToCompany(GameCompany gameCompany);

		Task RemoveGameCompany(GameCompany gameCompany);

		Task<List<Company>> GetCompaniesOfGame(int GameId);

		Task<List<Game>> GetGamesOfCompany(int CompanyId);
	}
}
