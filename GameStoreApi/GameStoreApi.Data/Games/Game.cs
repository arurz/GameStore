using GameStoreApi.Data.Common.Interfaces;
using System.Collections.Generic;

namespace GameStoreApi.Data.Games
{
	public class Game : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Picture { get; set; }
		public string Description { get; set; }
		public string MinimumSystemRequirements { get; set; }
		public decimal Price { get; set; }
		public bool IsActive { get; set; } = true;

        public ICollection<GenreGame> GameTypes { get; set; } = new List<GenreGame>();
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
		public ICollection<GameCompany> GameCompanies { get; set; } = new List<GameCompany>();

	}
}
