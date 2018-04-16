using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Alert
    {

	    public int AlertId { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
		public string Description { get; set; }

	}
}
