using System.Collections.Generic;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.API.ResourceModel;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Invoice")]
    public class InvoiceController : Controller
    {
	    private readonly IInvoiceAccess _invoiceAccess;

	    public InvoiceController(IInvoiceAccess invoiceAccess)
	    {
		    _invoiceAccess = invoiceAccess;
	    }

		/// <summary>
		/// Gets all invoices.
		/// </summary>
		/// <returns>Returns all invoices.</returns>
		/// <response code="201">Returns all invoices.</response>
		/// <response code="400">If the invoice is null.</response>
		[HttpGet]
	    [ProducesResponseType(typeof(Invoice), 201)]
	    [ProducesResponseType(typeof(Invoice), 400)]
	    public IActionResult Get()
	    {
		    var invoices = _invoiceAccess.GetInvoices();
		    var response = new List<HALResponse>();

		    foreach (var invoice in invoices)
		    {
				var resource = new InvoiceResource(invoice);
			    response.Add(new HALResponse(resource).AddLinks(
					new Link("invoice", "/api/Invoice/" + invoice.InvoiceId + "", null, "GET"),
					new Link("address", "/api/Address/" + resource.AddressId + "", null, "GET"),
					new Link("bankaccount", "/api/BankAccount/" + resource.BankAccounts + "", null, "GET"),
					new Link("create", "/api/Address/", null, "POST")
					));
		    }

		    return Ok(response);
	    }

	    /// <summary>
	    /// Gets an invoice by id.
	    /// </summary>
	    /// <returns>Returns an invoice by id.</returns>
	    /// <response code="201">Returns an invoice by id.</response>
	    /// <response code="400">Returns if the invoice ID is 0 or an invoice with the given ID does not exist.</response>
	    [HttpGet("{id}")]
	    [ProducesResponseType(typeof(Invoice), 201)]
	    [ProducesResponseType(typeof(Invoice), 400)]
	    public IActionResult Get(int id)
	    {
		    if (id.Equals(0) || _invoiceAccess.GetInvoice(id) == null)
		    {
			    return BadRequest();
		    }
			var resource = new InvoiceResource(_invoiceAccess.GetInvoice(id));
		    var response = new HALResponse(resource).AddSelfLink(Request).AddLinks(
				new Link("address", "/api/Address/" + resource.AddressId + "", null, "GET"),
				new Link("bankaccount", "/api/BankAccount/" + resource.BankAccounts + "", null, "GET"),
				new Link("invoice", "/api/Invoice/", null, "GET"));

		    return Ok(response);
		}



	    //TODO: DIT IS EEN VOORBEELD!!! de rest + dit nog verder uitwerken

	    /// <summary>
	    /// Creates an invoice.
	    /// </summary>
	    /// <remarks>
	    /// Sample request:
	    /// 
	    /// POST /Invoice
	    /// 
	    /// {
		///		"date": "2017-09-28T00:00:00",
		///		"name": "Name",
		///		"amountExBtw": 20,
		///		"btw": 1,
		///		"amountIncBtw": 21,
		///		"profit": 5,
		///		"address": {
		///			"City" : "City",
		///			"HouseNumber" : "HouseNr",
		///			"PostalCode" : "PostalCode",
		///			"Street" : "Street"
		///		}
		/// }
		/// </remarks>
		/// <param name="invoice"></param>
		/// <returns>A newly-created invoice</returns>
		/// <response code="201">Returns the newly-created item</response>
		/// <response code="400">If the item is null</response>  
	[HttpPost]
	    [ProducesResponseType(typeof(Invoice), 201)]
	    [ProducesResponseType(typeof(Invoice), 400)]
	    public IActionResult Post([FromBody] Invoice invoice)
	    {
		    _invoiceAccess.AddInvoice(invoice);
		    var resource = new InvoiceResource(invoice);
		    var response = new HALResponse(resource).AddSelfLink(Request);
			return Ok(response);
		}
	}
}