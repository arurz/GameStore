using GameStoreApi.Data.Common.Interfaces;
using GameStoreApi.Data.Users;
using System;

namespace GameStoreApi.Data.Games
{
	public class Comment : IEntity
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Username { get; set; }
		public DateTime CreationDate { get; set; }

		public int GameId { get; set; }
		public Game Game { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }
	}
}
