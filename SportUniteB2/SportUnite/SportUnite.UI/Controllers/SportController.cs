using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class SportController : Controller
	{
		private readonly ISportAccess _sportAccess;
		public int PageSize = 10;

		public SportController(ISportAccess sportAccess)
		{
			_sportAccess = sportAccess;
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult Sport(int page = 1)
			=> View(new SportViewModel
			{
				Sports = _sportAccess.GetSports()
					.OrderBy(s => s.Type)
					.Skip((page - 1) * PageSize)
					.Take(PageSize),
				PagingViewModel = new PagingViewModel
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = _sportAccess.GetSports().Count()
				}
			});

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult AddSport()
		{
			ViewBag.Title = "Sport toevoegen";

			return View();
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost]
		public IActionResult AddSport(Sport sport)
		{
			ViewBag.Title = "Sport toevoegen";

			if (!ModelState.IsValid) return View();

			_sportAccess.AddSport(sport);

			return RedirectToAction("Sport", new SportViewModel { Sports = _sportAccess.GetSports() });
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult UpdateSport(int id)
		{
			ViewBag.Title = "Sport wijzigen";

			return View(_sportAccess.GetSport(id));
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost]
		public IActionResult UpdateSport(Sport sport)
		{
			if (!ModelState.IsValid) return View(sport);

			_sportAccess.UpdateSport(sport);

			return RedirectToAction("Sport", new SportViewModel { Sports = _sportAccess.GetSports() });
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public IActionResult DeleteSport(Sport sport)
		{
			ViewBag.Title = "Sport";
			_sportAccess.DeleteSport(sport.SportId);

			return RedirectToAction("Sport", new SportViewModel { Sports = _sportAccess.GetSports() });
		}
	}
}