using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using GameStoreApi.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameStoreApi.Application.Games.Services
{
	public class CommentService : ICommentService
	{
		private readonly AppDbContext context;
        public CommentService(AppDbContext context)
        {
            this.context = context;
        }
		public async Task<Comment> GetComment(int id) => await context.Comments.SingleOrDefaultAsync(x => x.Id == id);

		public Comment CreateComment(Comment comment)
		{
			var user = context.Users.SingleOrDefault(x => x.Id == comment.UserId);
			comment.Username = user.Username;

			context.Comments.Add(comment);
			context.SaveChanges();

			return comment;
		}

		public async Task DeleteComment(int id)
		{
			var comment = await context.Comments.SingleOrDefaultAsync(x => x.Id == id);

			context.Comments.Remove(comment);
			await context.SaveChangesAsync();
		}
	}
}
