using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportUnite.UI.Models;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Controllers
{

	public class InvoiceController : Controller
	{
		private readonly IInvoiceAccess _invoiceAccess;
		public int PageSize = 10;

		public InvoiceController(IInvoiceAccess invoiceAccess)
		{
			_invoiceAccess = invoiceAccess;
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult Invoice(int page = 1)
			=> View(new InvoiceViewModel
			{
				Invoices = _invoiceAccess.GetInvoices()
					.OrderBy(i => i.InvoiceId)
					.Skip((page - 1) * PageSize)
					.Take(PageSize),
				PagingViewModel = new PagingViewModel
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = _invoiceAccess.GetInvoices().Count()
				}
			});

		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ViewResult UpdateInvoice(Invoice invoice)
		{
			ViewBag.Title = "Factuur wijzigen";

			return View(invoice);
		}

		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult UpdateInvoicePost(Invoice invoice)
		{
			ViewBag.Title = "Factuur wijzigen";
			_invoiceAccess.UpdateInvoice(invoice);

			return RedirectToAction("Invoice", new InvoiceViewModel { Invoices = _invoiceAccess.GetInvoices() });
		}

		[Authorize(Roles = "Administrator")]
		public ViewResult DetailInvoice(Invoice invoice)
		{
			ViewBag.Title = "Factuur bekijken";

			return View(_invoiceAccess.GetInvoice(invoice.InvoiceId));
		}

		[Authorize(Roles = "Administrator")]
		public IActionResult DeleteInvoice(Invoice invoice)
		{
			_invoiceAccess.DeleteInvoice(invoice.InvoiceId);
			return RedirectToAction("Invoice", new InvoiceViewModel { Invoices = _invoiceAccess.GetInvoices() });
		}
	}
}