using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfInvoiceRepository : IInvoiceRepository
    {
	    private readonly DatabaseContext _context;

	    public EfInvoiceRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

	    public IEnumerable<Invoice> GetInvoices()
	    {
		    return _context.Invoices.Include(b => b.Address).Include(b => b.BankAccountsInvoices).Where(i => i.Archived == false);
	    }

	    public IEnumerable<Invoice> GetArchivedInvoices()
	    {
		    return _context.Invoices.Where(h => h.Archived);
	    }

	    public Invoice GetInvoice(int id)
	    {
		    return _context.Invoices.Include(b => b.Address).Include(b => b.BankAccountsInvoices).FirstOrDefault(b => b.InvoiceId == id);
	    }

	    public void AddInvoice(Invoice invoice)
	    {
		    _context.Invoices.Add(invoice);
		    _context.SaveChanges();
	    }

	    public void UpdateInvoice(Invoice invoice)
	    {
		    var databaseInvoice = _context.Invoices.SingleOrDefault(i => i.InvoiceId == invoice.InvoiceId);
		    databaseInvoice.Address = invoice.Address;
		    databaseInvoice.AmountExBtw = invoice.AmountExBtw;
		    databaseInvoice.AmountInclBtw = invoice.AmountInclBtw;
		    databaseInvoice.BankAccountsInvoices = invoice.BankAccountsInvoices;
		    databaseInvoice.Btw = invoice.Btw;
		    databaseInvoice.Date = invoice.Date;
		    databaseInvoice.Name = invoice.Name;
		    databaseInvoice.Reservation = invoice.Reservation;
		    _context.SaveChanges();
	    }

	    public void DeleteInvoice(int id)
	    {
		    var databaseInvoice = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);
		    databaseInvoice.Archived = true;
		    _context.SaveChanges();
	    }

	    public void RestoreInvoice(int id)
	    {
		    var databaseInvoice = _context.Invoices.SingleOrDefault(i => i.InvoiceId == id);
		    databaseInvoice.Archived = false;
		    _context.SaveChanges();
	    }
	}
}