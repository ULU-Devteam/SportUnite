using System;
using System.Collections.Generic;
using System.Text;
using SportUnite.BLL.Abstract;
using SportUnite.Domain.Models;
using SportUnite.DAL;
using SportUnite.DAL.Abstract;

namespace SportUnite.BLL.Concrete
{
    public class BankAccountAccess : IBankAccountAccess
    {
	    private readonly IBankAccountRepository _repository;

	    public BankAccountAccess(IBankAccountRepository repo)
	    {
		    _repository = repo;
	    }
		public IEnumerable<BankAccount> GetBankAccounts()
		{
			return _repository.GetBankAccounts();
		}

	    public BankAccount GetAccount(int id)
	    {
		    return _repository.GetAccount(id);
	    }

	    public void AddBankAccount(BankAccount bankAccount)
	    {
		    _repository.AddBankAccount(bankAccount);
	    }

	    public void UpdateBankAccount(BankAccount bankAccount)
	    {
		    _repository.UpdateBankAccount(bankAccount);
	    }

	    public void DeleteBankAccount(int id)
	    {
		    _repository.DeleteBankAccount(id);
	    }
    }
}
