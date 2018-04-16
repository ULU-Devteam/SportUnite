using System.Collections.Generic;
using Halcyon.HAL;
using SportUnite.API.ResourceModel;
using Swashbuckle.AspNetCore.Examples;

namespace SportUnite.API.Examples
{
	public class EventListExample : IExamplesProvider
	{
		public object GetExamples()
		{
			var eventList = new List<EventResource>
			{
				new EventResource()
				{
					Archived = false,
					EventId = 1,
					Name = "Tennis Event 1",
					PeopleAmount = 4,
					Sports = new List<SportResource> { new SportResource { Archived = false, SportId = 1, Type = "Tennis"} }
					},
				new EventResource
				{
					Archived = false,
					EventId = 2,
					Name = "Hockey Event 2",
					PeopleAmount = 22,
					Sports = new List<SportResource> { new SportResource { Archived = false, SportId = 2, Type = "Hockey"} }
				}
			};

			var resources = new List<HALResponse>();
			foreach (var eventResource in eventList)
			{
				resources.Add(new HALResponse(eventResource)
					.AddLinks(new Link("self", "/api/sportcomplex/" + eventResource.EventId + "", null, "GET"))
					.AddLinks(new Link("update", "/api/sportcomplex/" + eventResource.EventId + "", null, "PUT"))
					.AddLinks(new Link("delete", "/api/sportcomplex/" + eventResource.EventId + "", null, "DELETE"))
					);
			}

			var response = new HALResponse(null)
				.AddLinks(new Link("self", "/api/sportcomplex/", null, "GET"))
				.AddEmbeddedCollection("events", resources)
				.AddLinks(new Link("create", "/api/sportcomplex/", null, "POST"));


			return response;
		}
	}
}
