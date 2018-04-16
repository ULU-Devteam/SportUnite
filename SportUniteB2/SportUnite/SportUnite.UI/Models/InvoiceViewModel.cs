using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.UI.Models
{
    public class InvoiceViewModel
    {
		public IEnumerable<Invoice> Invoices { get; set; }
		public PagingViewModel PagingViewModel { get; set; }
        public int SelectedInvoiceId { get; set; }
	}
}
