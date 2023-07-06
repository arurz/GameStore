using GameStoreApi.Data.Chat;
using GameStoreApi.Data.Chat.Dtos;
using GameStoreApi.Data.Users.Constants;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Communications.SignalR.Services
{
	public class ChatService
	{
		private readonly AppDbContext context;
        public ChatService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ChatUserDto>> GetAllUserChats()
		{
			var userIds = await context.Messages
				.Include(m => m.FromUser)
					.ThenInclude(fu => fu.Role)
				.Where(m => m.FromUser.Role.Name != RoleAlias.USER_ADMIN)
				.Select(m => m.FromUserId)
				.Distinct()
				.ToListAsync();

			var userChatDtos = new List<ChatUserDto>();
			foreach (var userId in userIds)
			{
				var userName = context.Users.SingleOrDefault(u => u.Id == userId).Username;

				var userChatDto = new ChatUserDto
				{
					UserId = (int)userId,
					Username = userName
				};
				userChatDtos.Add(userChatDto);
			}
			return userChatDtos;
		}

		public async Task<List<Message>> GetAllMessagesFromUserChat(int userId)
		{
			var messagesFromUser = await context.Messages.Where(m => m.FromUserId == userId).ToListAsync();
			var messagesToUser = await context.Messages.Where(m => m.ToUserId == userId).ToListAsync();

			var messages = messagesFromUser.Union(messagesToUser);
			return messages.OrderBy(m => m.CreationDate).ToList();
		}
	}
}
