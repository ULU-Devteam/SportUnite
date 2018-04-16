using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
    public class SportComplexResource
    {
	    public SportComplexResource(SportComplex complex)
	    {
		    SportComplexId = complex.SportComplexId;
		    Name = complex.Name;
		    Archived = complex.Archived;
		    AddressId = complex.AddressId;
	    }

	    public int SportComplexId { get; set; }
	    [Required(ErrorMessage = "Please enter a name")]
	    [StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
	    public string Name { get; set; }
	    public bool Archived { get; set; }

	    public int AddressId { get; set; }
	}
}
