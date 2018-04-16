using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SportUnite.Domain.Models;

namespace SportUnite.API.ResourceModel
{
    public class InvoiceResource
    {

		public InvoiceResource(Invoice invoice)
	    {
		    InvoiceId = invoice.InvoiceId;
		    Date = invoice.Date;
		    Name = invoice.Name;
		    AmountExBtw = invoice.AmountExBtw;
		    Btw = invoice.Btw;
		    AmountIncBtw = invoice.AmountInclBtw;
		    Profit = invoice.Profit;
		    Archived = invoice.Archived;
		    AddressId = invoice.Address.AddressId;
		    var list = new List<BankAccount>();
		    foreach (var b in invoice.BankAccountsInvoices)
		    {
			    list.Add(b.BankAccount);
		    }

		    BankAccounts = list;
	    }
	    public int InvoiceId { get; set; }
	    [Required(ErrorMessage = "Please enter a date")]
		public DateTime Date { get; set; }
	    [Required(ErrorMessage = "Please enter a name")]
		public string Name { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
		public double AmountExBtw { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
		public double Btw { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
		public double AmountIncBtw { get; set; }
	    [Required(ErrorMessage = "Please enter a profit amount")]
		public double Profit { get; set; }
		public bool Archived { get; set; }
		public int AddressId { get; set; }
		public List <BankAccount> BankAccounts { get; set; }

	}
}
