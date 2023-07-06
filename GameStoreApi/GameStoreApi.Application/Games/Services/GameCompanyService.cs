using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Services
{
	public class GameCompanyService : IGameCompanyService
	{
		private readonly AppDbContext context;
        public GameCompanyService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<GameCompany> AddGameToCompany(GameCompany gameCompany)
		{
			await context.GameCompanies.AddAsync(gameCompany);
			await context.SaveChangesAsync();

			return gameCompany;
		}

		public async Task<List<Company>> GetCompaniesOfGame(int GameId) => await context.Companies
		   .Where(c => c.GameCompanies
		   .Any(gc => gc.GameId == GameId))
		   .ToListAsync();

		public async Task<List<Game>> GetGamesOfCompany(int CompanyId) => await context.Games
			.Where(g => g.GameCompanies
			.Any(gc => gc.CompanyId == CompanyId))
			.ToListAsync();

		public async Task RemoveGameCompany(GameCompany gameCompany)
		{
			context.GameCompanies.Remove(gameCompany);
			await context.SaveChangesAsync();
		}
	}
}
