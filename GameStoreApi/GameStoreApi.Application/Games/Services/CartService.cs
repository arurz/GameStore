using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.DomainValidation.Enums;
using GameStoreApi.Data.DomainValidation.Services;
using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Data.Games.Enums;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Services
{
	public class CartService : ICartService
	{
        private readonly AppDbContext context;
        public CartService(AppDbContext context)
        {
            this.context = context;
        }

		public async Task<Cart> AddCart(Cart cart)
		{
			cart.Status = CartStatus.AddedToCart;

			var isCartExcists = await context.Carts.SingleOrDefaultAsync(c => c.UserId == cart.UserId && c.GameId == cart.GameId);
			if (isCartExcists != null)
			{
				DomainValidationService.ThrowErrorMessage(ErrorCode.User_CartAlreadyExists);
			}

			await context.Carts.AddAsync(cart);
			context.SaveChanges();

			return cart;
		}

		public async Task ChangeCartStatus(List<Cart> carts)
		{
			foreach (var cart in carts)
			{
				var dbCart = await context.Carts
					.SingleOrDefaultAsync(c => c.GameId == cart.GameId && c.UserId == cart.UserId);
				dbCart.Status = CartStatus.Bought;

				context.SaveChanges();
			}
		}

		public async Task<List<CartDto>> GetGamesDependsOnStatus(int id, CartStatus cartStatus)
		{
			var carts = await context.Carts
				.Include(c => c.Game)
				.Where(c => c.UserId == id && c.Status == cartStatus)
				.ToListAsync();
			var games = new List<CartDto>();
			foreach (var cart in carts) 
			{
				var dto = new CartDto()
				{
					Id = cart.Game.Id,
					Name = cart.Game.Name,
					Picture = cart.Game.Picture,
					Price = cart.Game.Price,
					CreationDate = cart.CreationDate
				};
				games.Add(dto);
			}
			return games;

		}

		public async Task RemoveCart(Cart Cart)
		{
			context.Carts.Remove(Cart);

			await context.SaveChangesAsync();
		}
	}
}
