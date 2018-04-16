using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models.CustomValidationAttributes;

namespace SportUnite.Domain.Models
{
    public class Hall
    {
	    public int HallId { get; set; }

        [Required(ErrorMessage = "Please enter a minimum capacity")]
        [Range(0, 1000, ErrorMessage = "Please enter a number below 1000")]
        public int CapacityMin { get; set; }

        [Required(ErrorMessage = "Please enter a maximum capacity")]
        [Range(0, 1000, ErrorMessage = "Please enter a number below 1000")]
        [GreaterThan("CapacityMin", "The maximum capacity must be higher than the minimum capacity")]
        public int CapacityMax { get; set; }

        [Required(ErrorMessage = "Please enter a name for the hall")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }

		public bool Archived { get; set; }

		public virtual List<SportAttribute> SportAttributes { get; set; }
		public virtual List<HallReservation> HallReservations { get; set; }
		[Required]
		public virtual SportComplex SportComplex { get; set; }
		public virtual List<Order> Orders { get; set; }

	}
}
