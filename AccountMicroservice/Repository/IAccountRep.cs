using AccountMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public interface IAccountRep
    {
        public List<AccountMsg> getCustomerAccounts(string id);
        public CustomerAccount createAccount(string id);
        public AccountMsg getAccount(int id);
        public TransactionMsg deposit(Transaction t);
        public TransactionMsg withdraw(Transaction t);
        public List<Statement> getAccountStatement(int AccountId, string from_date, string to_date);

    }
}
