using GameStoreApi.Data.Games;
using GameStoreApi.Data.Messages;
using GameStoreApi.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace GameStoreApi.Persistence
{
	public class AppDbContext : DbContext
	{
		public DbSet<GameCompany> GameCompanies { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<GenreGame> GameTypes { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Message> Messages { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{ }

		public override int SaveChanges()
		{
			var entries = ChangeTracker
				.Entries()
				.Where(x => x.State == EntityState.Added && x.Entity is Comment || x.Entity is Cart);

			foreach (var entry in entries)
			{
				entry.Property("CreationDate").CurrentValue = DateTime.Now;
			}

			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameCompanyConfiguration).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartConfiguration).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameTypeConfiguration).Assembly);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
		}
	}
}
