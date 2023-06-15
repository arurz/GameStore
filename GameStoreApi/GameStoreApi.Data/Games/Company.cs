using GameStoreApi.Data.Nomenclatures;
using System.Collections.Generic;

namespace GameStoreApi.Data.Games
{
	public class Company : Nomenclature
	{
		public string Country { get; set; }
		public string CompanyInfo { get; set; }

		public ICollection<GameCompany> GameCompanies { get; set; } = new List<GameCompany>();
	}
}
