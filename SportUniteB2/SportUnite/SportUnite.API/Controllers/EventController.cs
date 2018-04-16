using System.Collections.Generic;
using System.Linq;
using SportUnite.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SportUnite.BLL.Abstract;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using SportUnite.API.Examples;
using SportUnite.API.ResourceModel;
using Swashbuckle.AspNetCore.Examples;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SportUnite.API.Controllers
{
	/// <inheritdoc />
	/// <summary>
	/// Controller for Event API queries.
	/// </summary>
	[Produces("application/json")]
	[Route("api/Event")]
	public class EventController : Controller
	{

		private readonly IEventAccess _eventAccess;

		/// <summary>
		/// Constructor to inject IEventAccess.
		/// </summary>
		/// <param name="complexAccess"></param>
		public EventController(IEventAccess complexAccess)
		{
			_eventAccess = complexAccess;
		}

		/// <summary>
		/// GET all Events.
		/// </summary>
		/// 
		/// <returns>
		/// A list of Event items.
		/// </returns>
		/// 
		/// <response code="200">Returns a list of Event items.</response>
		/// <response code="404">If the resulting list of Events is empty.</response>
		[HttpGet]
		[SwaggerResponse(200, Type = typeof(HALResponse))]
		[SwaggerResponseExample(200, typeof(EventListExample))]
		public IActionResult Get()
		{
			var events = _eventAccess.GetEvents();

			if (events == null)
			{
				return NotFound();
			}

			var resources = new List<HALResponse>();

			foreach (var e in events)
			{
				var resource = new EventResource(e);
				resources.Add(new HALResponse(resource).AddLinks(new Link("read", "/api/Event/" + e.EventId + "", null, "GET"))
					.AddLinks(new Link("update", "/api/Event/" + e.EventId + "", null, "PUT"))
					.AddLinks(new Link("delete", "/api/Event/" + e.EventId + "", null, "DELETE")));
			}

			var response = new HALResponse(null).AddSelfLink(Request).AddEmbeddedCollection("events", resources)
				.AddLinks(new Link("create", "/api/sportcomplex/", null, "POST"));

			return Ok(response);
		}

		/// <summary>
		/// GET a specific Event.
		/// </summary>
		/// 
		/// <param name="id"></param>
		/// 
		/// <returns>
		/// An Event item.
		/// </returns>
		/// 
		/// <response code="200">Returns an Event item.</response>
		/// <response code="404">If the result is empty.</response>
		[HttpGet("{id}", Name = "Get")]
		[SwaggerResponse(200, Type = typeof(HALResponse))]
		[SwaggerResponseExample(200, typeof(EventExample))]
		public IActionResult Get(int id)
		{
			var e = _eventAccess.GetEvent(id);

			if (e == null)
			{
				return NotFound();
			}

			var response = new HALResponse(new EventResource(e)).AddSelfLink(Request)
				.AddLinks(new Link("update", "/api/Event/" + e.EventId + "", null, "PUT"))
				.AddLinks(new Link("delete", "/api/Event/" + e.EventId + "", null, "DELETE"));

			return Ok(response);
		}

		/// <summary>
		/// POST a new Event.
		/// </summary>
		/// 
		/// <param name="e"></param>
		/// 
		/// <returns>
		/// An Event item.
		/// </returns>
		/// 
		/// <response code="200">Returns the new Event item.</response>
		/// <response code="400">If the body is empty.</response>
		[HttpPost]
		[SwaggerResponse(200, Type = typeof(HALResponse))]
		[SwaggerResponseExample(200, typeof(EventExample))]
		public IActionResult Post([FromBody] EventPostPut e)
		{
			if (e == null)
			{
				return BadRequest();
			}

			var ev = new Event { Name = e.Name, PeopleAmount = e.PeopleAmount };
			var sportIds = e.SportIds.ToList();

			var createdEvent = _eventAccess.AddEvent(ev, sportIds);

			var response = new HALResponse(new EventResource(createdEvent))
				.AddLinks(new Link("update", "/api/Event/" + createdEvent.EventId + "", null, "PUT"))
				.AddLinks(new Link("delete", "/api/Event/" + createdEvent.EventId + "", null, "DELETE"));
			return Ok(response);
		}

		/// <summary>
		/// PUT a specific Event.
		/// </summary>
		/// 
		/// <param name="id"></param>
		/// <param name="e"></param>
		/// 
		/// <returns>
		/// An Event item.
		/// </returns>
		/// 
		/// <response code="200">Returns the updated Event item.</response>
		/// <response code="400">If the body is empty.</response>
		/// <response code="404">If the Event is not found.</response>
		[HttpPut("{id}")]
		[SwaggerResponse(200, Type = typeof(HALResponse))]
		[SwaggerResponseExample(200, typeof(EventExample))]
		public IActionResult Put(int id, [FromBody]EventPostPut e)
		{
			if (e == null)
			{
				return BadRequest();
			}

			if (id == 0)
			{
				return NotFound();
			}

			var ev = new Event { EventId = id, Archived = e.Archived, Name = e.Name, PeopleAmount = e.PeopleAmount };
			var sportIds = e.SportIds.ToList();

			_eventAccess.UpdateEvent(ev, sportIds);

			var response = new HALResponse(new EventResource(_eventAccess.GetEvent(id)))
				.AddLinks(new Link("update", "/api/Event/" + id + "", null, "PUT"))
				.AddLinks(new Link("delete", "/api/Event/" + id + "", null, "DELETE"));
			return Ok(response);
		}

		/// <summary>
		/// DELETE a specific Event.
		/// </summary>
		/// 
		/// <param name="id"></param>
		/// 
		/// <returns>
		/// An Event item.
		/// </returns>
		/// 
		/// <response code="200">Returns the archived Event item.</response>
		/// <response code="404">If the Event is not found.</response>
		[HttpDelete("{id}")]
		[SwaggerResponse(200, Type = typeof(HALResponse))]
		[SwaggerResponseExample(200, typeof(EventExample))]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return NotFound();
			}

			_eventAccess.DeleteEvent(id);

			var response = new HALResponse(new EventResource(_eventAccess.GetEvent(id)))
				.AddLinks(new Link("update", "/api/Event/" + id + "", null, "PUT"))
				.AddLinks(new Link("delete", "/api/Event/" + id + "", null, "DELETE"));
			return Ok(response);
		}
	}
}
