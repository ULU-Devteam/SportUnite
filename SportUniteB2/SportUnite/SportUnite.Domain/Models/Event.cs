using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Event
    {
	    public int EventId { get; set; }
	    [Required(ErrorMessage = "Please enter a name")]
		public string Name { get; set; }
	    [Required(ErrorMessage = "Please enter the amount of people attending")]
		public int PeopleAmount { get; set; }
		public bool Archived { get; set; }

		public virtual Reservation Reservation { get; set; }
		[Required]
		public virtual List<EventSport> EventSports { get; set; }
	}
}
