using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class DepositHandler
    {
        public IAccountRepository AccountRepository { get; set; }

        public void Handle(DepositCommand command)
        {
            var account = AccountRepository.Get(command.AccountId);
            account.Deposit(command.Amount);
            AccountRepository.Save(account);
        }
    }
}
