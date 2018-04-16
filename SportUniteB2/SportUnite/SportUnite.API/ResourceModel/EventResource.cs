using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
	public class EventResource
	{
		public EventResource() { }

		public EventResource(Event e)
		{
			EventId = e.EventId;
			Archived = e.Archived;
			Name = e.Name;
			PeopleAmount = e.PeopleAmount;

			Sports = new List<SportResource>();
			foreach (var es in e.EventSports)
			{
				Sports.Add(new SportResource(es.Sport));
			}
		}

		public int EventId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int PeopleAmount { get; set; }
		public bool Archived { get; set; }
		public List<SportResource> Sports { get; set; }
	}
}
