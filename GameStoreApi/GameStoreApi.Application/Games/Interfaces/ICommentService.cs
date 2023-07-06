using GameStoreApi.Data.Games;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Interfaces
{
	public interface ICommentService
	{
		Task<Comment> GetComment(int id);
		Comment CreateComment(Comment comment);

		Task DeleteComment(int id);
	}
}
