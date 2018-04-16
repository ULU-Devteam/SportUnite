namespace SportUnite.Domain.Models
{
    public class BankAccountInvoice
    {

	    public int BankAccountId { get; set; }
	    public virtual BankAccount BankAccount { get; set; }
	    public int InvoiceId { get; set; }
	    public virtual Invoice Invoice { get; set; }

	}
}
