using System.Collections.Generic;
using SportUnite.Domain.Models;


namespace SportUnite.UI.Models
{
    public class SportComplexViewModel
    {
        public IEnumerable<SportComplex> Complexes { get; set; }
		public PagingViewModel PagingViewModel { get; set; }
		public int SelectedSportComplexId { get; set; }
    }
	
}