using GameStoreApi.Data.Games.Enums;
using GameStoreApi.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStoreApi.Data.Games
{
	public class Cart
	{
		public int GameId { get; set; }
		public Game Game { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public DateTime? CreationDate { get; set; }

		[NotMapped]
		private CartStatus CartStatus;
		public CartStatus Status
		{
			get
			{
				return CartStatus;
			}
			set
			{
				CartStatus = value;
			}
		}
	}

	public class CartConfiguration : IEntityTypeConfiguration<Cart>
	{
		public void Configure(EntityTypeBuilder<Cart> builder)
		{
			builder.HasKey(x => new { x.GameId, x.UserId });
		}
	}
}
