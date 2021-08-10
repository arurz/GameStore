using System.Collections.Generic;

namespace GameStore.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<GameType> GameTypes { get; set; } = new List<GameType>();
    }
}
