using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Sport
    {

	    public int SportId { get; set; }
	    [Required(ErrorMessage = "Please enter a type")]
		[StringLength(50, ErrorMessage = "Name of the sport must be less than 50 characters long")]
		public string Type { get; set; }
		public bool Archived { get; set; }

		public virtual List<SportAttribute> SportAttributes { get; set; }
		public virtual List<EventSport> EventSports { get; set; }

	}
}
