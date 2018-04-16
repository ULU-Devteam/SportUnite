using System.Collections.Generic;
using Halcyon.HAL;
using SportUnite.API.ResourceModel;
using Swashbuckle.AspNetCore.Examples;

namespace SportUnite.API.Examples
{
	public class EventExample : IExamplesProvider
	{
		public object GetExamples()
		{
			var resource = new EventResource
			{
				Archived = false,
				EventId = 1,
				Name = "Tennis Event 1",
				PeopleAmount = 4,
				Sports = new List<SportResource> { new SportResource { Archived = false, SportId = 1, Type = "Tennis" } }
			};

			var response = new HALResponse(resource)
				.AddLinks(new Link("self", "/api/sportcomplex/" + resource.EventId + "", null, "GET"))
				.AddLinks(new Link("update", "/api/sportcomplex/" + resource.EventId + "", null, "PUT"))
				.AddLinks(new Link("delete", "/api/sportcomplex/" + resource.EventId + "", null, "DELETE"));


			return response;
		}
	}
}
