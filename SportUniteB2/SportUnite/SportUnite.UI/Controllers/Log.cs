using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportUnite.UI.Controllers
{

	public class LogController : Controller
	{
		public int PageSize = 10;

		public LogController()
		{
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult Log()
		{
			ViewBag.Title = "Log";

			return View();
		}
	}
}