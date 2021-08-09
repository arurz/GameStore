using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Models
{
    public class GameType
    {
        public int GameId { get; set; }
        public Game Game { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
    public class GameTypeConfiguration : IEntityTypeConfiguration<GameType>
    {
        public void Configure(EntityTypeBuilder<GameType> builder)
        {
            builder.HasKey(x => new { x.GameId, x.TypeId });
        }
    }
}
