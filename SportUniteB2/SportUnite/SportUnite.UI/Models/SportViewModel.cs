using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class SportViewModel
    {
		public IEnumerable<Sport> Sports { get; set; }
		public PagingViewModel PagingViewModel { get; set; }
        public int SelectedSportId { get; set; }
	}
}
