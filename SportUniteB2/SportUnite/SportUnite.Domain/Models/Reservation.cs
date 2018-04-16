using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Reservation
    {

	    public int ReservationId { get; set; }
		[Required(ErrorMessage = "Please enter a date")]
	    [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date")]
		public DateTime DateTime { get; set; }
		[Required(ErrorMessage = "Please specify how long to reserve")]
		[Range(1, 12)]
		public int HoursAmount { get; set; }
	    [Required(ErrorMessage = "Please enter a valid name")]
		[StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }
		public bool Archived { get; set; }

		[Required]
		public virtual Invoice Invoice { get; set; }
		public int InvoiceId { get; set; }
		[Required(ErrorMessage = "Please specify an event this reservation is linked to")]
		public virtual Event Event { get; set; }
		public int EventId { get; set; }
		[Required]
		public virtual List<HallReservation> HallReservations { get; set; }

	}
}
