using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class SportHallenViewModel
    {
        public int SportComplexId { get; set; }
        public IEnumerable<Hall> SportHalls { get; set; }
        public Hall Hall { get; set; }
        public int SelectedHallId { get; set; }
		//public PagingViewModel pagingViewModel { get; set; }
	}
}