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
	[Route("api/sport")]
	public class SportController : Controller
	{
		private readonly ISportAccess _sportAccess;

		public SportController(ISportAccess sportAccess)
		{
			_sportAccess = sportAccess;
		}
		/// <summary>
		/// Gets all sports.
		/// </summary>
		/// <returns>Returns all sports.</returns>
		/// <response code="201">Returns all sports.</response>
		/// <response code="400">If the sport is null.</response>
		[HttpGet]
		[ProducesResponseType(typeof(Sport), 201)]
		[ProducesResponseType(typeof(Sport), 400)]
		public IActionResult Get()
		{
			var sports = _sportAccess.GetSports();
			var response = new List<HALResponse>();

			foreach (var sport in sports)
			{
				var resource = new SportResource(sport);
				response.Add(new HALResponse(resource).AddLinks(
					new Link("self", "/api/Sport/" + sport.SportId + "", null,"GET")));
			}

			return Ok(response);
		}
		/// <summary>
		/// Gets a sport by id.
		/// </summary>
		/// <returns>Returns a sport by id.</returns>
		/// <response code="201">Returns a sport by id.</response>
		/// <response code="400">If the sport is null.</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Sport), 201)]
		[ProducesResponseType(typeof(Sport), 400)]
		public IActionResult Get(int id)
		{
			if (id.Equals(0) || _sportAccess.GetSport(id) == null)
			{
				return BadRequest();
			}
			var resource = new SportResource(_sportAccess.GetSport(id));
			var response = new HALResponse(resource).AddSelfLink(Request).AddLinks(
				new Link("sport", "/api/Sport/", null, "GET"));

			return Ok(response);
		}

	}
}