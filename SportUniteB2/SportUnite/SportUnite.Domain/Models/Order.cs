using System;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Order
    {

	    public int OrderId { get; set; }
	    [Required(ErrorMessage = "Please enter a description")]
		public string Description { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a positive number")]
		public double Cost { get; set; }
		public bool Archived { get; set; }

		[Required]
		public Hall Hall { get; set; }

	}
}
