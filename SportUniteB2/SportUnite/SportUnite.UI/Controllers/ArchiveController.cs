using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class ArchiveController : Controller
	{
		private readonly IInvoiceAccess _invoiceAccess;
		private readonly ISportComplexAccess _complexAccess;
		private readonly IEventAccess _eventAccess;
		private readonly ISportAccess _sportAccess;
		private readonly IOrderAccess _orderAccess;
		private readonly IReservationAccess _reservationAccess;

		public ArchiveController(
			IInvoiceAccess invoiceAccess,
			ISportComplexAccess complexAccess,
			IEventAccess eventAccess,
			ISportAccess sportAccess,
			IOrderAccess orderAccess,
			IReservationAccess reservationAccess)
		{
			_invoiceAccess = invoiceAccess;
			_complexAccess = complexAccess;
			_eventAccess = eventAccess;
			_sportAccess = sportAccess;
			_orderAccess = orderAccess;
			_reservationAccess = reservationAccess;
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult ArchiveEvent()
		{
			ViewBag.Title = "Archief event";

			var events = _eventAccess.GetArchivedEvents();

			return View(events);
		}

		public ViewResult ArchiveHall()
		{
			ViewBag.Title = "Archief hall";

			var model = new SportHallenViewModel { SportHalls = _complexAccess.GetArchivedHalls() };

			return View(model);
		}

		public ViewResult ArchiveInvoice()
		{
			ViewBag.Title = "Archief facturen";

			var model = new InvoiceViewModel { Invoices = _invoiceAccess.GetArchivedInvoices() };

			return View(model);
		}

		public ViewResult ArchiveOrder()
		{
			ViewBag.Title = "Archief inkoop";

			var model = new OrderViewModel { Orders = _orderAccess.GetArchivedOrders() };

			return View(model);

		}

		public ViewResult ArchiveReservation()
		{
			ViewBag.Title = "Archief reserveringen";

			return View(new List<Reservation>());
		}

		public ViewResult ArchiveSport()
		{
			ViewBag.Title = "Archief sport";

			var model = new SportViewModel { Sports = _sportAccess.GetArchivedSports() };

			return View(model);
		}

		public ViewResult ArchiveSportAttribute()
		{
			ViewBag.Title = "Archief sportmaterialen";

			var model = new SportAttributesViewModel { Attributes = _complexAccess.GetArchivedSportAttributes() };

			return View(model);
		}

		public ViewResult ArchiveSportComplex()
		{
			ViewBag.Title = "Archief sportcomplex";

			var model = new SportComplexViewModel { Complexes = _complexAccess.GetArchivedSportComplexes() };

			return View(model);
		}

		public IActionResult RestoreHall(SportHallenViewModel viewModel)
		{
			_complexAccess.RestoreHall(viewModel.SelectedHallId);

			return RedirectToAction("ArchiveHall", viewModel);
		}

		public IActionResult RestoreEvent(EventSportViewModel viewModel)
		{
			_eventAccess.RestoreEvent(viewModel.SelectedEventId);

			return RedirectToAction("ArchiveEvent");
		}

		public IActionResult RestoreInvoice(InvoiceViewModel viewModel)
		{
			_invoiceAccess.RestoreInvoice(viewModel.SelectedInvoiceId);

			return RedirectToAction("ArchiveInvoice");
		}

		public IActionResult RestoreOrder(OrderViewModel viewModel)
		{
			_orderAccess.RestoreOrder(viewModel.SelectedOrderId);

			return RedirectToAction("ArchiveOrder");
		}

		public IActionResult RestoreSport(SportViewModel viewModel)
		{
			_sportAccess.RestoreSport(viewModel.SelectedSportId);

			return RedirectToAction("ArchiveSport");
		}

		public IActionResult RestoreSportAttribute(SportAttributesViewModel viewModel)
		{
			_complexAccess.RestoreSportAttribute(viewModel.SelectedSportAttributeId);

			return RedirectToAction("ArchiveSportAttribute");
		}

		public IActionResult RestoreSportComplex(SportComplexViewModel viewModel)
		{
			_complexAccess.RestoreSportComplex(viewModel.SelectedSportComplexId);

			return RedirectToAction("ArchiveSportComplex");
		}

		public IActionResult RestoreReservation()
		{
			return RedirectToAction("ArchiveReservation");
		}
	}
}