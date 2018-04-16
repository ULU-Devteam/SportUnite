using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class BankAccount
    {

		public int BankAccountId { get; set; }
        [Required(ErrorMessage = "Please enter the name of the bank account holder")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
	    public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the accountnumber")]
        [RegularExpression(@"^NL\d{2}[A-Z]{4}0\d{9}$", ErrorMessage = "Please enter a valid account number")]
		public int AccountNumber { get; set; }

		[Required(ErrorMessage = "Please select atleast 1 invoice")]
		public virtual List<BankAccountInvoice> BankAccountInvoices { get; set; }

	}
}
