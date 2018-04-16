using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class SportAttribute
    {

	    public int SportAttributeId { get; set; }
	    [Required(ErrorMessage = "Please enter an attribute type")]
		[StringLength(50, ErrorMessage = "Type must be less than 50 characters long")]
		public string Type { get; set; }
        [Required(ErrorMessage = "Please enter a detailed name for the attribute")]
		[StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }
		public bool Approved { get; set; }
		public bool Archived { get; set; }

		[Required]
		public virtual Hall Hall { get; set; }
		[Required]
	    public virtual Sport Sport { get; set; }

    }
}