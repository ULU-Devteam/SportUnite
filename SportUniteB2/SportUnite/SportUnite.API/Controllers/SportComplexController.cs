using System.Collections.Generic;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Mvc;
using SportUnite.API.ResourceModel;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportUnite.API.Controllers
{


	namespace API.Controllers
	{
		[Produces("application/json")]
		[Route("api/sportcomplex")]
		public class SportComplexController : Controller
		{

			private readonly ISportComplexAccess _complexAccess;

			public SportComplexController(ISportComplexAccess complexAccess)
			{
				_complexAccess = complexAccess;
			}


			/// <summary>
			/// Gets all sportcomplexes.
			/// </summary>
			/// <returns>Returns all sportcomplexes.</returns>
			/// <response code="200">Returns all sportcomplexes.</response>
			[HttpGet]
			public IActionResult GetSportComplexes()
			{
				var complexes = _complexAccess.GetSportComplexes();
				var resources = new List<HALResponse>();

				foreach (var sportComplex in complexes)
				{
					var resource = new SportComplexResource(sportComplex);
					resources.Add(new HALResponse(resource).AddLinks(
						new Link("sportcomplex", "/api/sportcomplex/" + resource.SportComplexId + "", null, "GET"),
						new Link("address", "/api/address/" + resource.AddressId + "", null, "GET"),
						new Link("halls", "/api/sportcomplex/" + resource.SportComplexId + "/hall", null, "GET")));
				}

				var response = new HALResponse(null).AddSelfLink(Request).AddEmbeddedCollection("sportComplexes", resources);

				return Ok(response);
			}


			/// <summary>
			/// Gets a single sportcomplex.
			/// </summary>
			/// <param name="id"></param>
			/// <returns>A single sportcomplex.</returns>
			/// <response code="200">Returns a sportcomplex.</response>
			/// <response code="400">Returns if the ID is 0 or a sportcomplex with given ID does not exist.</response>  
			[HttpGet("{id}")]
			public IActionResult GetSportComplex(int id)
			{
				if (id.Equals(0) || _complexAccess.GetSportComplex(id) == null)
				{
					return BadRequest();
				}

				var resource = new SportComplexResource(_complexAccess.GetSportComplex(id));

				var response = new HALResponse(resource).AddSelfLink(Request).AddLinks(
					new Link("address", "/api/address/" + resource.AddressId + "", null, "GET"),
					new Link("halls", "/api/sportcomplex/" + resource.SportComplexId + "/hall", null, "GET"));

				return Ok(response);
			}

			/// <summary>
			/// Gets all halls within a sportcomplex.
			/// </summary>
			/// <param name="id"></param>
			/// <returns>Selected sportcomplex with linked halls.</returns>
			/// <response code="200">Returns a sportcomplex with halls.</response>
			/// <response code="400">Returns if the ID is 0 or a sportcomplex with given ID does not exist.</response>
			[HttpGet("{id}/hall")]
			public IActionResult GetHalls(int id)
			{
				if (id.Equals(0) || _complexAccess.GetSportComplex(id) == null)
				{
					return BadRequest();
				}

				var halls = _complexAccess.GetHalls(id);
				var resources = new List<HALResponse>();

				foreach (var hall in halls)
				{
					var resource = new HallResource(hall);
					resources.Add(new HALResponse(resource).AddLinks(
						new Link("hall", "/api/sportcomplex/" + id + "/hall/" + resource.HallId + "", null, "GET"),
						new Link("sportattributes", "/api/sportcomplex/" + id + "/hall/" + resource.HallId + "/attribute", null, "GET")));
				}

				var response = new HALResponse(new SportComplexResource(_complexAccess.GetSportComplex(id))).AddSelfLink(Request).AddEmbeddedCollection("halls", resources);

				return Ok(response);
			}

			/// <summary>
			/// Gets a single hall from a specified sportcomplex.
			/// </summary>
			/// <param name="id"></param>
			/// <param name="hallid"></param>
			/// <returns>A single hall.</returns>
			/// <response code="200">Returns a hall.</response>
			/// <response code="400">Returns if the ID is 0 or a sportcomplex/hall with given ID does not exist.</response>
			[HttpGet("{id}/hall/{hallid}")]
			public IActionResult GetHall(int id, int hallid)
			{
				if (id.Equals(0) && hallid.Equals(0) || _complexAccess.GetSportComplex(id) == null || _complexAccess.GetHall(hallid) == null)
				{
					return BadRequest();
				}

				var resource = new HallResource(_complexAccess.GetHall(hallid));
				
				var response = new HALResponse(resource).AddSelfLink(Request).AddLinks(
					new Link("sportattributes", "/api/sportcomplex/" + id + "/hall/" + resource.HallId + "/attribute", null, "GET"));

				return Ok(response);
			}

			/// <summary>
			/// Gets all sportattributes within a hall.
			/// </summary>
			/// <param name="id"></param>
			/// <param name="hallid"></param>
			/// <returns>Selected hall with linked sportattributes.</returns>
			/// <response code="200">Returns a hall with sportattributes.</response>
			/// <response code="400">Returns if the ID is 0 or a sportcomplex/hall with given ID does not exist.</response>
			[HttpGet("{id}/hall/{hallid}/attribute")]
			public IActionResult GetAttributes(int id, int hallid)
			{

				if (id.Equals(0) && hallid.Equals(0) || _complexAccess.GetSportComplex(id) == null || _complexAccess.GetHall(hallid) == null)
				{
					return BadRequest();
				}

				var attributes = _complexAccess.GetSportAttributes(hallid);
				var resources = new List<HALResponse>();

				foreach (var attribute in attributes)
				{
					var resource = new SportAttributeResource(attribute);
					resources.Add(new HALResponse(resource).AddLinks(
						new Link("sportattribute", "/api/sportcomplex/" + id + "/hall/" + hallid + "/attribute/" + resource.SportAttributeId + "", null, "GET"),
						new Link("sport", "/api/sport/" + resource.sportId + "", null, "GET")));
				}

				var response = new HALResponse(new HallResource(_complexAccess.GetHall(hallid))).AddSelfLink(Request).AddEmbeddedCollection("sportattributes", resources);

				return Ok(response);
			}


			/// <summary>
			/// Gets a single sportattribute from a specified hall.
			/// </summary>
			/// <param name="id"></param>
			/// <param name="hallid"></param>
			/// <param name="attributeid"></param>
			/// <returns>A single sportattribute.</returns>
			/// <response code="200">Returns a sportattribute.</response>
			/// <response code="400">Returns if the ID is 0 or a sportcomplex/hall/sportattribute with given ID does not exist.</response>
			[HttpGet("{id}/hall/{hallid}/attribute/{attributeid}")]
			public IActionResult GetAttribute(int id, int hallid, int attributeid)
			{
				if (id.Equals(0) && hallid.Equals(0) || _complexAccess.GetSportComplex(id) == null || _complexAccess.GetHall(hallid) == null || _complexAccess.GetSportAttribute(attributeid) == null)
				{
					return BadRequest();
				}

				var resource = new SportAttributeResource(_complexAccess.GetSportAttribute(attributeid));

				var response = new HALResponse(resource).AddSelfLink(Request).AddLinks(
					new Link("sport", "/api/sport/" + resource.sportId + "", null, "GET"));

				return Ok(response);
			}















			//TODO: DIT IS EEN VOORBEELD!!! de rest + dit nog verder uitwerken

			/// <summary>
			/// Creates a sportcomplex.
			/// </summary>
			/// <remarks>	
			/// Sample request:
			///
			///     POST /SportComplex
			///     {
			///        "placeholder": 1,
			///        "placeholder2": "Item1",
			///        "placeholder3": true
			///     }
			///
			/// </remarks>
			/// <param name="sportComplex"></param>
			/// <returns>A newly-created sportcomplex</returns>
			/// <response code="201">Returns the newly-created item</response>
			/// <response code="400">If the item is null</response>  
			[HttpPost]
			[ProducesResponseType(typeof(SportComplex), 201)]
			[ProducesResponseType(typeof(SportComplex), 400)]
			public IActionResult Post([FromBody] SportComplex sportComplex)
			{
				if (sportComplex == null)
				{
					return BadRequest();
				}

				_complexAccess.AddSportComplex(sportComplex);
				return Ok(sportComplex);
			}

			[HttpDelete("{id}")]
			public IActionResult Delete(int id)
			{
				_complexAccess.DeleteSportComplex(id);
				return Ok();
			}

			[HttpPut]
			public IActionResult Put([FromBody] SportComplex sportComplex)
			{
				_complexAccess.UpdateSportComplex(sportComplex);
				return Ok(_complexAccess.GetSportComplex(sportComplex.SportComplexId));
			}


		}
	}
}
