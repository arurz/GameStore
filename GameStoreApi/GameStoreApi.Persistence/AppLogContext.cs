using GameStoreApi.Data.Errors;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace GameStoreApi.Persistence
{
	public class AppLogContext : DbContext
	{
        public DbSet<Error> Errors{ get; set; }
        public AppLogContext(DbContextOptions<AppLogContext> options) : base(options)
		{ }

		public override int SaveChanges()
		{
			var entries = ChangeTracker
				.Entries()
				.Where(x => x.State == EntityState.Added);

			foreach (var entry in entries)
			{
				entry.Property("ErrorAppearanceDateTime").CurrentValue = DateTime.Now;
			}

			return base.SaveChanges();
		}
	}
}
