using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Type = GameStore.Models.Type;

namespace GameStore.Contexts
{
    public class GameStoreDBContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options) : base(options)
        { }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added && x.Entity is Comment);

            foreach(var entry in entries)
            {
                entry.Property("CreationDate").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommentConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CartConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameTypeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
