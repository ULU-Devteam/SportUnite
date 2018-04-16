using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class SportComplex
    {

	    public int SportComplexId { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }
		public bool Archived { get; set; }

		public virtual List<Hall> Halls { get; set; }
		[Required]
	    public virtual Address Address { get; set; }
	    public int AddressId { get; set; }

    }
}
