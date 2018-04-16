using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
	public class EventSportViewModel
	{
		public Event Event { get; set; }
		public IEnumerable<Sport> Sports { get; set; }
		[Required(ErrorMessage = "Please select at least one sport")]
		public IEnumerable<int> SportIds { get; set; }
		public int SelectedEventId { get; set; }
	}
}
