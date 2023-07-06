using GameStoreApi.Data.Chat;
using GameStoreApi.Data.Chat.Dtos;
using GameStoreApi.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Communications.SignalR.Services
{
	public class MessageService
	{
		private readonly AppDbContext context;
        public MessageService(AppDbContext context)
        {
            this.context = context;
        }

		public async Task<Message> StoreMessage(MessageDto dto)
		{
			var user = context.Users.SingleOrDefault(u => u.Id == dto.FromUserId);
			var message = new Message
			{
				FromUserId = dto.FromUserId,
				FromUser = user,
				ToUserId = dto.ToUserId,
				Content = dto.Content
			};

			await context.Messages.AddAsync(message);
			context.SaveChanges();
			return message;
		}
	}
}
