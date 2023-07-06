using GameStoreApi.Application.Communications.SignalR.Services;
using GameStoreApi.Data.Chat.Dtos;
using GameStoreApi.Data.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Communications.SignalR.Hubs
{
	public class ChatHub : Hub
	{
		private readonly MessageService messageService;
		protected IHubContext<ChatHub> chatContext;

		public ChatHub(MessageService messageService, IHubContext<ChatHub> chatContext)
		{
			this.messageService = messageService;
			this.chatContext = chatContext;
		}

		public override Task OnConnectedAsync()
		{
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			return base.OnDisconnectedAsync(exception);
		}

		public async Task<Message> SendMessage(MessageDto dto)
		{
			var message = await messageService.StoreMessage(dto);
			await chatContext.Clients.All.SendAsync("Receive", message);

			return message;
		}
	}
}
