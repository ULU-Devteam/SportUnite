using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
    public class AddressResource
    {
	    public AddressResource(Address address)
	    {
		    AddressId = address.AddressId;
		    City = address.City;
		    Street = address.Street;
		    PostalCode = address.PostalCode;
		    HouseNumber = address.HouseNumber;
	    }

	    public int AddressId { get; set; }

	    [Required(ErrorMessage = "Please enter a city")]
	    public string City { get; set; }
	    [Required(ErrorMessage = "Please enter a street")]
	    public string Street { get; set; }
	    [Required(ErrorMessage = "Please enter a postal code")]
	    [RegularExpression(@"^[1-9][0-9]{3}\s?[a-zA-Z]{2}$", ErrorMessage = "Please enter a valid postalcode")]
	    public string PostalCode { get; set; }
	    [Required(ErrorMessage = "Please enter a house number")]
	    // TODO: HouseNumber validation
	    public int HouseNumber { get; set; }
	}
}