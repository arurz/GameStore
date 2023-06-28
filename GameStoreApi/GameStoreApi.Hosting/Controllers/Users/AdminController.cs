using Microsoft.AspNetCore.Mvc;

namespace GameStoreApi.Hosting.Controllers.Users
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
