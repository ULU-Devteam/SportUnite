using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportUnite.BLL.Abstract;

namespace SportUnite.UI.Controllers
{

	public class ReservationController : Controller
	{
		private readonly IReservationAccess _reservationAccess;

		public ReservationController(IReservationAccess reservationAccess)
		{
			_reservationAccess = reservationAccess;
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult Reservation()
		{
			ViewBag.Title = "Reserveringen";

			return View();
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult AddReservation()
		{
			ViewBag.Title = "Reserveringen toevoegen";

			return View();
		}

		[Authorize(Roles = "Administrator,ComplexManager")]
		public ViewResult UpdateReservation()
		{
			ViewBag.Title = "Reserveringen wijzigen";

			return View();
		}
	}
}