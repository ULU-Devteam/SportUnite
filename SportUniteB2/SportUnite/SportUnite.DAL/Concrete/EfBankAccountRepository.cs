using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportUnite.DAL.Abstract;
using SportUnite.Domain.Models;

namespace SportUnite.DAL.Concrete
{
    public class EfBankAccountRepository : IBankAccountRepository
    {
	    private readonly DatabaseContext _context;

	    public EfBankAccountRepository(DatabaseContext context)
	    {
		    _context = context;
	    }

	    public IEnumerable<BankAccount> GetBankAccounts()
	    {
		    return _context.BankAccounts;
	    }

	    public BankAccount GetAccount(int id)
	    {
		    return _context.BankAccounts.SingleOrDefault(b => b.BankAccountId == id);
	    }

	    public void AddBankAccount(BankAccount bankAccount)
	    {
		    _context.BankAccounts.Add(bankAccount);
		    _context.SaveChanges();
	    }

	    public void UpdateBankAccount(BankAccount bankAccount)
	    {
		    BankAccount databaseAccount =
			    _context.BankAccounts.SingleOrDefault(b => b.BankAccountId == bankAccount.BankAccountId);

		    databaseAccount.Name = bankAccount.Name;
		    databaseAccount.AccountNumber = bankAccount.AccountNumber;

		    _context.SaveChanges();
	    }

	    public void DeleteBankAccount(int id)
	    {
		    
	    }
	}
}
