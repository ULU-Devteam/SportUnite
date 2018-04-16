using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Log
    {

	    public int LogId { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
		public string Description { get; set; }

	}
}
