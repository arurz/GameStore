using GameStoreApi.Application.Games.Interfaces;
using GameStoreApi.Data.Games;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace GameStoreApi.Hosting.Controllers.Games
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService commentService;
		public CommentController(ICommentService commentService)
		{
			this.commentService = commentService;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Comment>> GetComment([FromRoute] int id) => await commentService.GetComment(id);

		[HttpPost("create")]
		public async Task<IActionResult> CreateComment([FromBody] Comment comment)
		{
			commentService.CreateComment(comment);
			return CreatedAtAction(nameof(CreateComment), new { id = comment.Id }, comment);
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> DeleteComment([FromRoute] int id)
		{
			await commentService.DeleteComment(id);
			return NoContent();
		}
	}
}
