using AccountMicroservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AccountMicroservice.Repository
{
    public class AccountRep : IAccountRep
    {
        static int acid = 203;
        public AccountRep()
        {
        }
        public static List<CustomerAccount> CustomerAccounts = new List<CustomerAccount>()
        {
            new CustomerAccount{CustomerId="JhonSmith",CurrentAccountId=201,SavingsAccountId=202}
        };
        public static List<Account> Accounts = new List<Account>()
        {
            new Account{Id=201,Balance=1000},
            new Account{Id=202,Balance=500}
        };
        public static List<AccountStatement> accountStatements = new List<AccountStatement>()
        {
            new AccountStatement{AccountId=202,
            Statements= new List<Statement>()
            {
                new Statement{Date=DateTime.ParseExact("21/02/2022", "dd/MM/yyyy", null),Narration="Withdrawn",Withdrawal=0.00,Deposit=2000.00,ClosingBalance=2000.00},
                new Statement{Date=DateTime.ParseExact("27/05/2022","dd/MM/yyyy", null),Narration="Deposited",Withdrawal=1500.00,Deposit=2000.00,ClosingBalance=500.00}
                }
            },
            new AccountStatement{AccountId=201,
            Statements= new List<Statement>()
            {
                new Statement{Date=DateTime.ParseExact("21/02/2022", "dd/MM/yyyy", null),Narration="Deposited",Withdrawal=0.00,Deposit=500.00,ClosingBalance=500.00},
                new Statement{Date=DateTime.ParseExact("27/03/2022", "dd/MM/yyyy", null),Narration="Deposited",Withdrawal=0.00,Deposit=2000.00,ClosingBalance=2500.00},
                new Statement{Date=DateTime.ParseExact("21/06/2022", "dd/MM/yyyy", null),Narration="Withdrawn",Withdrawal=1500.00,Deposit=0.00,ClosingBalance=1000.00}
                }
            }
         };

        public List<AccountMsg> getCustomerAccounts(string id)
        {
            var a = CustomerAccounts.Find(c => c.CustomerId == id);
            var ca = Accounts.Find(cac => cac.Id == a.CurrentAccountId);
            var sa = Accounts.Find(sac => sac.Id == a.SavingsAccountId);
            var ac = new List<AccountMsg>
            {
                new AccountMsg{AccId=ca.Id,AccType="Current Account",AccBal=ca.Balance},
                new AccountMsg{AccId=sa.Id,AccType="Savings Account",AccBal=sa.Balance}
            };
            return ac;
        }
        public CustomerAccount createAccount(string id)
        {
            CustomerAccount a = new CustomerAccount
            {
                CustomerId = id,
                CurrentAccountId = acid,
                SavingsAccountId = (acid + 1)
            };
            CustomerAccounts.Add(a);
            Account ca = new Account
            {
                Id = a.CurrentAccountId,
                Balance = 0.00
            };
            Accounts.Add(ca);
            accountStatements.Add(
                    new AccountStatement
                    {
                        AccountId = ca.Id,
                        Statements = new List<Statement>() { }
                    }
                );
            Account sa = new Account
            {
                Id = a.SavingsAccountId,
                Balance = 0.00
            };
            Accounts.Add(sa);
            acid += 2;
            accountStatements.Add(
                    new AccountStatement
                    {
                        AccountId = sa.Id,
                        Statements = new List<Statement>() { }
                    }
                );
            return a;
        }
        public AccountMsg getAccount(int id)
        {
            var acc = Accounts.Find(a => a.Id == id);
            if(id % 2 != 0)
            {
                var accMsg = new AccountMsg
                {
                    AccId = acc.Id,
                    AccType = "Current Account",
                    AccBal = acc.Balance
                };
                return accMsg;
            }
            else
            {
                var accMsg = new AccountMsg
                {
                    AccId = acc.Id,
                    AccType = "Savings Account",
                    AccBal = acc.Balance
                };
                return accMsg;
            }
        }
        public TransactionMsg deposit(Transaction t)
        {
            var acc = Accounts.Find(a => a.Id == t.AccountId);
            acc.Balance = acc.Balance + t.Amount;
            TransactionMsg accMsg = new TransactionMsg
            {
                AccountId = t.AccountId,
                Message = "Sucessfully Deposited Amount",
                Balance = acc.Balance
            };
            var accStatement = accountStatements.Find(a => a.AccountId == t.AccountId);
            accStatement.Statements.Add(
                    new Statement { Date = DateTime.Today, Narration = "Deposited", Withdrawal = 0.00,Deposit = t.Amount, ClosingBalance = acc.Balance }
                );
            return accMsg;
        }

        public TransactionMsg withdraw(Transaction t)
        {
            var acc = Accounts.Find(a => a.Id == t.AccountId);
            if (acc.Balance >= t.Amount)
            {
                acc.Balance = acc.Balance - t.Amount;
                TransactionMsg accMsg = new TransactionMsg
                {
                    AccountId = t.AccountId,
                    Message = "Sucessfully Withdrawn Amount",
                    Balance = acc.Balance
                };
                var accStatement = accountStatements.Find(a => a.AccountId == t.AccountId);
                accStatement.Statements.Add(
                        new Statement { Date = DateTime.Today, Narration = "Withdrawn", Withdrawal = t.Amount, Deposit = 0.0, ClosingBalance = acc.Balance }
                    );
                return accMsg;
            }
            else
            {
                TransactionMsg accMsg = new TransactionMsg
                {
                    AccountId = t.AccountId,
                    Message = "Inscufficient Balance",
                    Balance = acc.Balance
                };
                return accMsg;
            }            
        }
        public List<Statement> getAccountStatement(int AccountId, string from_date, string to_date)
        {
            var accs = accountStatements.Find(a => a.AccountId == AccountId);
            var s = accs.Statements;
            DateTime from = DateTime.ParseExact(from_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime to = DateTime.ParseExact(to_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            List<Statement> res = new List<Statement>();
            foreach (var n in s)
            {
            if (n.Date >= from && n.Date <= to)
                {
                    res.Add(n);
                }
            }
            return res;
        }

    }
}
