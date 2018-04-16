using System.Collections.Generic;
using SportUnite.BLL.Abstract;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Concrete
{
    public class InvoiceAccess : IInvoiceAccess
    {
	    private readonly IInvoiceRepository _repository;

	    public InvoiceAccess(IInvoiceRepository repo)
	    {
		    _repository = repo;
	    }

	    public IEnumerable<Invoice> GetInvoices()
	    {
		    return _repository.GetInvoices();
	    }

	    public IEnumerable<Invoice> GetArchivedInvoices()
	    {
		    return _repository.GetArchivedInvoices();
	    }

	    public Invoice GetInvoice(int id)
	    {
		    return _repository.GetInvoice(id);
	    }

	    public void AddInvoice(Invoice invoice)
	    {
		    _repository.AddInvoice(invoice);
	    }

	    public void UpdateInvoice(Invoice invoice)
	    {
		    _repository.UpdateInvoice(invoice);
	    }

	    public void DeleteInvoice(int id)
	    {
		    _repository.DeleteInvoice(id);
	    }

	    public void RestoreInvoice(int id)
	    {
		    _repository.RestoreInvoice(id);
	    }
	}
}