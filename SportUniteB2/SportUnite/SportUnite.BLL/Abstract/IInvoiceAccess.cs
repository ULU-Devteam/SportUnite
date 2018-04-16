using System.Collections.Generic;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface IInvoiceAccess
    {
	    IEnumerable<Invoice> GetInvoices();
	    IEnumerable<Invoice> GetArchivedInvoices();
	    Invoice GetInvoice(int id);
	    void AddInvoice(Invoice invoice);
	    void UpdateInvoice(Invoice invoice);
	    void DeleteInvoice(int id);
	    void RestoreInvoice(int id);
	}
}