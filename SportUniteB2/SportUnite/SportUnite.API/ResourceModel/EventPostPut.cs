using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
	public class EventPostPut
	{
		public EventPostPut() { }

		public EventPostPut(Event e)
		{
			Name = e.Name;
			PeopleAmount = e.PeopleAmount;
			Archived = e.Archived;

			SportIds = new List<int>();
			foreach (var es in e.EventSports)
			{
				SportIds.Add(es.Sport.SportId);
			}
		}

		[Required]
		public string Name { get; set; }
		[Required]
		public int PeopleAmount { get; set; }
		public bool Archived { get; set; }
		public List<int> SportIds { get; set; }
	}
}
