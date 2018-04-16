using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.API.ResourceModel;
using SportUnite.BLL.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportUnite.API.Controllers
{
	[Produces("application/json")]
	[Route("api/address")]
	public class AddressController : Controller
	{

		private readonly ISportComplexAccess _complexAccess;

		public AddressController(ISportComplexAccess complexAccess)
		{
			_complexAccess = complexAccess;
		}

		/// <summary>
		/// Gets a single address.
		/// </summary>
		/// <param name="id"></param>
		/// <returns>A single address.</returns>
		/// <response code="200">Returns an address.</response>
		/// <response code="400">Returns if the ID is 0 or an address with given ID does not exist.</response>  
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			if (id.Equals(0) || _complexAccess.GetAddress(id) == null)
			{
				return BadRequest();
			}

			var address = new AddressResource(_complexAccess.GetAddress(id));

			var response = new HALResponse(address).AddSelfLink(Request);

			return Ok(response);
		}
	}
}