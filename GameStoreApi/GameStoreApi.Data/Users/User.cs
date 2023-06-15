using GameStoreApi.Data.Common.Interfaces;
using GameStoreApi.Data.Games;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApi.Data.Users
{
	public class User : IEntity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		[NotMapped]
		public string RepeatPassword { get; set; }
		public string TelephoneNumber { get; set; }
		public string Salt { get; set; }
		public int RoleId { get; set; }
		public Role Role { get; set; }

		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
		public ICollection<Cart> Carts { get; set; } = new List<Cart>();
	}
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property("Email").IsRequired();
			builder.Property("FirstName").IsRequired();
			builder.Property("LastName").IsRequired();
			builder.Property("Username").IsRequired();
			builder.Property("Password").IsRequired();
		}
	}
}
