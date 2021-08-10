using System.Collections.Generic;

namespace GameStore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string MinimumSystemRequirements { get; set; }
        public decimal Price { get; set; }

        public ICollection<GameType> GameTypes { get; set; } = new List<GameType>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Cart> Cards { get; set; } = new List<Cart>();
    }
}
