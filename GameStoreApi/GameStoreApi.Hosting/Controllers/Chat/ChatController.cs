using GameStoreApi.Application.Communications.SignalR.Hubs;
using GameStoreApi.Application.Communications.SignalR.Services;
using GameStoreApi.Data.Chat.Dtos;
using GameStoreApi.Data.Chat;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Hosting.Controllers.Chat
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatController : ControllerBase
	{
		private readonly ChatHub chatHub;
		private readonly ChatService chatService;
		public ChatController(ChatHub chatHub, ChatService chatService)
		{
			this.chatHub = chatHub;
			this.chatService = chatService;
		}

		[HttpPost]
		public async Task<ActionResult<Message>> SendMessage([FromBody] MessageDto dto)
		{
			var message = await chatHub.SendMessage(dto);
			return message;
		}

		[HttpGet("all")]
		public async Task<ActionResult<List<ChatUserDto>>> GetAllUserChats() => await chatService.GetAllUserChats();

		[HttpGet("messages/{id}")]
		public async Task<ActionResult<List<Message>>> GetAllMessagesFromUserChat([FromRoute] int id) => await chatService.GetAllMessagesFromUserChat(id);
	}
}
