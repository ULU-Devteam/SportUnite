using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain.Models;
using SportUnite.Domain.Models.CustomValidationAttributes;

namespace SportUnite.API.ResourceModel
{
    public class HallResource
    {
	    public HallResource(Hall hall)
	    {
		    HallId = hall.HallId;
		    CapacityMin = hall.CapacityMin;
		    CapacityMax = hall.CapacityMax;
		    Name = hall.Name;
		    Archived = hall.Archived;
	    }

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
	}
}
