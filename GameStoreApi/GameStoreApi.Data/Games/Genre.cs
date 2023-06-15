using GameStoreApi.Data.Nomenclatures;
using System.Collections.Generic;

namespace GameStoreApi.Data.Games
{
	public class Genre : Nomenclature
	{
		public string Description { get; set; }

		public ICollection<GenreGame> GameTypes { get; set; } = new List<GenreGame>();
	}
}
