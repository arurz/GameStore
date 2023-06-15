using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Data.Games
{
	public class GenreGame
	{
		public int GameId { get; set; }
		public Game Game { get; set; }

		public int TypeId { get; set; }
		public Genre Genre { get; set; }
	}
	public class GameTypeConfiguration : IEntityTypeConfiguration<GenreGame>
	{
		public void Configure(EntityTypeBuilder<GenreGame> builder)
		{
			builder.HasKey(x => new { x.GameId, x.TypeId });

			builder
				.HasOne<Game>(gg => gg.Game)
				.WithMany(g => g.GameTypes)
				.HasForeignKey(gt => gt.GameId);

			builder
				.HasOne<Genre>(gg => gg.Genre)
				.WithMany(g => g.GameTypes)
				.HasForeignKey(gt => gt.TypeId);
		}
	}
}
