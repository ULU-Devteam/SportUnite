using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class EventController : Controller
	{
		private readonly IEventAccess _eventAccess;
		private readonly ISportAccess _sportAccess;

		public EventController(IEventAccess eventAccess, ISportAccess sportAccess)
		{
			_eventAccess = eventAccess;
			_sportAccess = sportAccess;
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult Event()
		{
			ViewBag.Title = "Evenementen";

			return View(_eventAccess.GetEvents());
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult AddEvent()
		{
			ViewBag.Title = "Evenementen toevoegen";

			return View(new EventSportViewModel { Sports = _sportAccess.GetSports() });
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpPost]
		public IActionResult AddEvent(EventSportViewModel viewModel)
		{
			viewModel.Sports = _sportAccess.GetSports();

			if (ModelState.ContainsKey("Event.EventSports"))
			{
				ModelState["Event.EventSports"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid) return View(viewModel);
			ViewBag.Title = "Evenementen toevoegen";

			_eventAccess.AddEvent(viewModel.Event, viewModel.SportIds);

			return RedirectToAction("Event", _eventAccess.GetEvents());
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		[HttpGet]
		public ViewResult UpdateEvent(EventSportViewModel viewModel)
		{
			ModelState.Clear();

			ViewBag.Title = "Evenementen wijzigen";

			viewModel.Event = _eventAccess.GetEvent(viewModel.SelectedEventId);
			viewModel.Sports = _sportAccess.GetSports();
			viewModel.SportIds = _sportAccess.GetSports(viewModel.Event).Select(s => s.SportId).ToList();

			return View("UpdateEvent", viewModel);
		}

		[HttpPost, ActionName("UpdateEvent")]
		public IActionResult UpdateEventConfirmed(EventSportViewModel viewModel)
		{
			viewModel.Sports = _sportAccess.GetSports();

			if (ModelState.ContainsKey("Event.EventSports"))
			{
				ModelState["Event.EventSports"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid) return View(viewModel);

			ViewBag.Title = "Evenementen wijzigen";

			_eventAccess.UpdateEvent(viewModel.Event, viewModel.SportIds);

			return RedirectToAction("Event", _eventAccess.GetEvents());
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public IActionResult DeleteEvent(Event newEvent)
		{
			_eventAccess.DeleteEvent(newEvent.EventId);

			return RedirectToAction("Event");
		}
	}
}