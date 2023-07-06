using GameStoreApi.Data.Games;
using GameStoreApi.Data.Games.Dtos;
using GameStoreApi.Data.Games.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Interfaces
{
	public interface ICartService
	{
		Task<Cart> AddCart(Cart cart);

		Task RemoveCart(Cart Cart);

		Task ChangeCartStatus(List<Cart> carts);

		Task<List<CartDto>> GetGamesDependsOnStatus(int id, CartStatus cartStatus);
	}
}
