using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportUnite.Domain.Models
{
    public class Invoice
    {

	    public int InvoiceId { get; set; }
        [Required(ErrorMessage = "Please enter a date")]
		[DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date")]
		public DateTime Date { get; set; }
	    [Required(ErrorMessage = "Please enter a name")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters long")]
		public string Name { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a positive number")]
		public double AmountExBtw { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a positive number")]
		public double Btw { get; set; }
	    [Required(ErrorMessage = "Please enter an amount")]
        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a positive number")]
		public double AmountInclBtw { get; set; }
	    [Required(ErrorMessage = "Please enter a profit amount")]
		public double Profit { get; set; }
		public bool Archived { get; set; }

		[Required]
		public virtual List<BankAccountInvoice> BankAccountsInvoices { get; set; }
		[Required]
		public virtual Reservation Reservation { get; set; }
		[Required]
	    public virtual Address Address { get; set; }

	}
}
