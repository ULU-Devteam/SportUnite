using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;

namespace SportUnite.UI.Controllers
{

	public class AdminController : Controller
	{
		private readonly IInvoiceAccess _invoiceAccess;
		private readonly IReservationAccess _reservationAccess;

		public AdminController(IInvoiceAccess invoiceAccess, IReservationAccess reservationAccess)
		{
			_invoiceAccess = invoiceAccess;
			_reservationAccess = reservationAccess;
		}

		[Authorize(Roles = "Administrator, ComplexManager")]
		public ViewResult Dashboard()
		{
			ViewBag.Title = "Dashboard";

			var dashboardInfo =
				new DashboardViewModel
				{
					Invoices = _invoiceAccess.GetInvoices().OrderByDescending(x => x.Date).Take(5),
					Reservations = _reservationAccess.GetReservations().OrderByDescending(x => x.DateTime).Take(5)
				};

			return View(dashboardInfo);
		}
	}
}