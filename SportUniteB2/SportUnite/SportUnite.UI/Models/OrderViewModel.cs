using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class OrderViewModel
    {
	    public IEnumerable<SportComplex> SportComplexen { get; set; }
	    public IEnumerable<Hall> Halls { get; set; }
	    public IEnumerable<Order> Orders { get; set; }
	    public int SelectedSportComplexId { get; set; }
		[Required(ErrorMessage = "Please select a sporthal")]
		public int SelectedHallId { get; set; }
		public int SelectedOrderId { get; set; }
	    public Order Order { get; set; }
    }
}
