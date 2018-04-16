using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class OrderController : Controller
	{
		private readonly IOrderAccess _orderAccess;
		private readonly ISportComplexAccess _complexAccess;

		public OrderController(IOrderAccess orderAccess, ISportComplexAccess complexAccess)
		{
			_orderAccess = orderAccess;
			_complexAccess = complexAccess;
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult Order()
		{
			ViewBag.Title = "Bestellingen";

			var viewModel = new OrderViewModel { Orders = _orderAccess.GetOrders() };

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ViewResult AddOrder()
		{
			ViewBag.Title = "Bestelling toevoegen";

			var viewModel = new OrderViewModel { SportComplexen = _complexAccess.GetSportComplexes() };

			return View(viewModel);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult AddOrder(OrderViewModel viewModel)
		{

			if (ModelState.ContainsKey("Order.Hall"))
			{
				ModelState["Order.Hall"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid)
			{
				viewModel.SportComplexen = _complexAccess.GetSportComplexes();
				viewModel.Halls = _complexAccess.GetHalls(viewModel.SelectedSportComplexId);
				return View(viewModel);
			}

			viewModel.Order.Hall = _complexAccess.GetHall(viewModel.SelectedHallId);
			_orderAccess.AddOrder(viewModel.Order);

			return RedirectToAction("Order");
		}

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ViewResult UpdateOrder(Order order)
		{
			var viewModel = new OrderViewModel
			{
				Order = _orderAccess.GetOrder(order.OrderId),
				SelectedOrderId = order.OrderId
			};
			viewModel.SelectedSportComplexId = viewModel.Order.Hall.SportComplex.SportComplexId;
			viewModel.SelectedHallId = viewModel.Order.Hall.HallId;
			viewModel.SportComplexen = _complexAccess.GetSportComplexes();
			viewModel.Halls = _complexAccess.GetHalls(viewModel.SelectedSportComplexId);

			return View("UpdateOrder", viewModel);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost, ActionName("UpdateOrder")]
		public IActionResult UpdateOrderConfirmed(OrderViewModel viewModel)
		{
			if (ModelState.ContainsKey("Order.Hall"))
			{
				ModelState["Order.Hall"].ValidationState = ModelValidationState.Valid;
			}

			if (!ModelState.IsValid)
			{
				viewModel.SportComplexen = _complexAccess.GetSportComplexes();
				viewModel.Halls = _complexAccess.GetHalls(viewModel.SelectedSportComplexId);
				return View(viewModel);
			}

			viewModel.Order.Hall = _complexAccess.GetHall(viewModel.SelectedHallId);

			_orderAccess.UpdateOrder(viewModel.Order);

			return RedirectToAction("Order");
		}

		[Authorize(Roles = "Administrator")]
		public IActionResult DeleteOrder(Order order)
		{
			ViewBag.Title = "Bestelling verwijderen";
			_orderAccess.DeleteOrder(order.OrderId);

			return RedirectToAction("Order");
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult LoadHalls(OrderViewModel viewModel)
		{
			viewModel.Halls = _complexAccess.GetHalls(viewModel.SelectedSportComplexId);
			viewModel.SportComplexen = _complexAccess.GetSportComplexes();

			if (viewModel.SelectedOrderId == 0) return View("AddOrder", viewModel);
			viewModel.Order = _orderAccess.GetOrder(viewModel.SelectedOrderId);
			return View("UpdateOrder", viewModel);
		}
	}
}