using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Data.Games
{
	public class GameCompany
	{
		public int GameId { get; set; }
		public Game Game { get; set; }

		public int CompanyId { get; set; }
		public Company Company { get; set; }

	}
	public class GameCompanyConfiguration : IEntityTypeConfiguration<GameCompany>
	{
		public void Configure(EntityTypeBuilder<GameCompany> builder)
		{
			builder.HasKey(x => new { x.GameId, x.CompanyId });

			builder
				.HasOne<Game>(gg => gg.Game)
				.WithMany(g => g.GameCompanies)
				.HasForeignKey(gc => gc.GameId);

			builder
				.HasOne<Company>(gg => gg.Company)
				.WithMany(g => g.GameCompanies)
				.HasForeignKey(gc => gc.CompanyId);
		}
	}
}
