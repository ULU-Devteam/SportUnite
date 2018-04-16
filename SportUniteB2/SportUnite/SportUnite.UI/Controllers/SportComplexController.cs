using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class SportComplexController : Controller
	{
		private readonly ISportComplexAccess _complexAccess;
		private readonly ISportAccess _sportAccess;
		public int PageSize = 10;

		public SportComplexController(ISportComplexAccess complexAccess, ISportAccess sportAccess)
		{
			_complexAccess = complexAccess;
			_sportAccess = sportAccess;
		}

		// SportComplex Methods

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult SportComplex(int page = 1)
			=> View(new SportComplexViewModel
			{
				Complexes = _complexAccess.GetSportComplexes()
				.OrderBy(s => s.Name)
				.Skip((page - 1) * PageSize)
				.Take(PageSize),
				PagingViewModel = new PagingViewModel
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = _complexAccess.GetSportComplexes().Count()
				}
			});

		[HttpGet]
		public ViewResult AddSportComplex()
		{
			ViewBag.Title = "Sportcomplex toevoegen";

			return View();
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost]
		public IActionResult AddSportComplex(SportComplex sportComplex)
		{
			if (!ModelState.IsValid) return View("AddSportComplex");
			ViewBag.Title = "Sportcomplex toevoegen";
			_complexAccess.AddSportComplex(sportComplex);

			return RedirectToAction("SportComplex", new SportComplexViewModel { Complexes = _complexAccess.GetSportComplexes() });
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult UpdateSportComplex(int id)
		{
			return View("UpdateSportComplex", _complexAccess.GetSportComplex(id));
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost]
		public IActionResult UpdateSportComplex(SportComplex sportComplex)
		{
			if (!ModelState.IsValid) return View(sportComplex);
			_complexAccess.UpdateSportComplex(sportComplex);

			return RedirectToAction("SportComplex", new SportComplexViewModel { Complexes = _complexAccess.GetSportComplexes() });
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public IActionResult DeleteSportComplex(SportComplex sportComplex)
		{
			ViewBag.Title = "Sportcomplex toevoegen";
			_complexAccess.DeleteSportComplex(sportComplex.SportComplexId);

			return RedirectToAction("SportComplex", new SportComplexViewModel { Complexes = _complexAccess.GetSportComplexes() });
		}


		//Hall Methods

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult Hall(SportHallenViewModel viewModel)
		{
			ViewBag.Title = "Sporthallen";
			viewModel.SportHalls = _complexAccess.GetHalls(viewModel.SportComplexId);

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult AddHall(SportHallenViewModel viewModel)
		{
			ViewBag.Title = "Sporthallen toevoegen";

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost, ActionName("AddHall")]
		public IActionResult AddHallConfirmed(SportHallenViewModel viewModel)
		{
			ViewBag.Title = "Sporthallen toevoegen";

			if (ModelState.ContainsKey("Hall.HallId") && ModelState.ContainsKey("Hall.SportComplex"))
			{
				ModelState["Hall.HallId"].ValidationState = ModelValidationState.Valid;
				ModelState["Hall.SportComplex"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid) return View(viewModel);

			var hall = viewModel.Hall;
			var complex = _complexAccess.GetSportComplex(viewModel.SportComplexId);
			hall.SportComplex = complex;

			_complexAccess.AddHall(viewModel.Hall);

			viewModel.SportHalls = _complexAccess.GetHalls(viewModel.SportComplexId);

			return RedirectToAction("Hall", viewModel);
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult UpdateHall(SportHallenViewModel viewModel)
		{
			ViewBag.Title = "Sporthallen wijzigen";

			viewModel.Hall = _complexAccess.GetHall(viewModel.SelectedHallId);

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost, ActionName("UpdateHall")]
		public IActionResult UpdateHallConfirmed(SportHallenViewModel viewModel)
		{
			ViewBag.Title = "Sporthallen wijzigen";

			if (ModelState.ContainsKey("Hall.SportComplex"))
			{
				ModelState["Hall.SportComplex"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid) return View(viewModel);

			var hall = viewModel.Hall;
			var complex = _complexAccess.GetSportComplex(viewModel.SportComplexId);
			hall.SportComplex = complex;
			hall.HallId = viewModel.SelectedHallId;

			_complexAccess.UpdateHall(hall);

			viewModel.SportHalls = _complexAccess.GetHalls(viewModel.SportComplexId);

			return RedirectToAction("Hall", viewModel);
		}


		[Authorize(Roles = "Administrator,ComplexManager")]
		public IActionResult DeleteHall(SportHallenViewModel viewModel)
		{
			_complexAccess.DeleteHall(viewModel.SelectedHallId);
			viewModel.SportHalls = _complexAccess.GetHalls(viewModel.SportComplexId);

			return RedirectToAction("Hall", viewModel);
		}

		// SportAttribute Methods

		[Authorize(Roles = "Administrator, ComplexManager")]
		public ViewResult SportAttribute(SportAttributesViewModel viewModel)
		{
			ViewBag.Title = "Sportmaterialen";
			viewModel.Attributes = _complexAccess.GetSportAttributes(viewModel.SportHallId);

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator, ComplexManager")]
		[HttpGet]
		public ViewResult AddSportAttribute(SportAttributesViewModel viewModel)
		{
			ViewBag.Title = "Sportmateriaal toevoegen";
			viewModel.Sports = _sportAccess.GetSports();

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator, ComplexManager")]
		[HttpPost, ActionName("AddSportAttribute")]
		public IActionResult AddSportAttributeConfirmed(SportAttributesViewModel viewModel)
		{
			ViewBag.Title = "Sportmateriaal toevoegen";

			if (ModelState.ContainsKey("NewAttribute.Sport") && ModelState.ContainsKey("NewAttribute.Hall"))
			{
				ModelState["NewAttribute.Sport"].ValidationState = ModelValidationState.Valid;
				ModelState["NewAttribute.Hall"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid)
			{
				viewModel.Sports = _sportAccess.GetSports();
				return View(viewModel);
			}

			viewModel.NewAttribute.Hall = _complexAccess.GetHall(viewModel.SportHallId);
			viewModel.NewAttribute.Sport = _sportAccess.GetSport(viewModel.SportId);
			_complexAccess.AddSportAttribute(viewModel.NewAttribute);

			return RedirectToAction("SportAttribute", viewModel);
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult UpdateSportAttribute(SportAttributesViewModel viewModel)
		{
			ViewBag.Title = "Sportmateriaal wijzigen";
			viewModel.NewAttribute = _complexAccess.GetSportAttribute(viewModel.SelectedSportAttributeId);
			viewModel.Sports = _sportAccess.GetSports();

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator, ComplexManager")]
		[HttpPost, ActionName("UpdateSportAttribute")]
		public IActionResult UpdateSportAttributeConfirmed(SportAttributesViewModel viewModel)
		{

			if (ModelState.ContainsKey("NewAttribute.Sport") && ModelState.ContainsKey("NewAttribute.Hall"))
			{
				ModelState["NewAttribute.Sport"].ValidationState = ModelValidationState.Valid;
				ModelState["NewAttribute.Hall"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid)
			{
				viewModel.Sports = _sportAccess.GetSports();
				viewModel.NewAttribute = _complexAccess.GetSportAttribute(viewModel.SelectedSportAttributeId);
				return View(viewModel);
			}

			viewModel.NewAttribute.Sport = _sportAccess.GetSport(viewModel.SportId);
			viewModel.NewAttribute.Hall = _complexAccess.GetHall(viewModel.SportHallId);
			_complexAccess.UpdateSportAttribute(viewModel.NewAttribute);

			return RedirectToAction("SportAttribute", viewModel);
		}

		[Authorize(Roles = "Administrator, ComplexManager")]
		public IActionResult DeleteSportAttribute(SportAttributesViewModel viewModel)
		{
			_complexAccess.DeleteSportAttribute(viewModel.SelectedSportAttributeId);

			return RedirectToAction("SportAttribute", viewModel);
		}
	}
}