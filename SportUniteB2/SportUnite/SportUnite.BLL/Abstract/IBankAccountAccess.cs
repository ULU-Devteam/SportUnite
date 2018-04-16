using System;
using System.Collections.Generic;
using System.Text;
using SportUnite.Domain.Models;

namespace SportUnite.BLL.Abstract
{
    public interface IBankAccountAccess
    {
	    IEnumerable<BankAccount> GetBankAccounts();
	    BankAccount GetAccount(int id);
	    void AddBankAccount(BankAccount bankAccount);
	    void UpdateBankAccount(BankAccount bankAccount);
	    void DeleteBankAccount(int id);
    }
}
