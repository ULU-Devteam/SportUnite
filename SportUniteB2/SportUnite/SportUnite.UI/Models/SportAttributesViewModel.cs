using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class SportAttributesViewModel
    {
	    public int SportHallId { get; set; }
        public int SportComplexId { get; set; }
        public int SportId { get; set; }
        public IEnumerable<SportAttribute> Attributes;
        public IEnumerable<Sport> Sports;
        public SportAttribute NewAttribute { get; set; }
	    public int SelectedSportAttributeId { get; set; }
		public PagingViewModel PagingViewModel { get; set; }
	}
}
