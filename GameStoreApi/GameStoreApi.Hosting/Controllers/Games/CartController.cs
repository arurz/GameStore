using GameStoreApi.Application.Communications.Mail;
using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameStoreApi.Data.Games.Enums;

namespace GameStoreApi.Hosting.Controllers.Games
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService cartService;
		private readonly MailService mailService;
		public CartController(ICartService cartService, MailService mailService)
		{
			this.cartService = cartService;
			this.mailService = mailService;
		}

		[HttpGet("{id}")]
		public async Task<List<CartDto>> GetGamesFromCart([FromRoute] int id) => await cartService.GetGamesDependsOnStatus(id, CartStatus.AddedToCart);

		[HttpGet("sold/{id}")]
		public async Task<List<CartDto>> GetGamesForShoppingHistory([FromRoute] int id) => await cartService.GetGamesDependsOnStatus(id, CartStatus.Bought);

		[HttpPost("add")]
		public async Task<IActionResult> CreateCart([FromBody] Cart cart)
		{
			await cartService.AddCart(cart);
			return CreatedAtAction(nameof(CreateCart), new { id = cart.UserId }, cart);
		}

		[HttpDelete("delete")]
		public async Task<IActionResult> DeleteCart([FromBody] Cart cart)
		{
			await cartService.RemoveCart(cart);
			return NoContent();
		}

		[HttpPost("buy")]
		public async Task<IActionResult> BuyGames([FromBody] List<Cart> carts)
		{
			var userId = carts[0].UserId;
			await mailService.SendMessage(userId, "GameStore purchase", "your purchase was accepted");
			await cartService.ChangeCartStatus(carts);

			return Ok();
		}
	}
}
