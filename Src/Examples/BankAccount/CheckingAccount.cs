using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class CheckingAccount 
    {
         

        private decimal _balance;

        public CheckingAccount (decimal balance)
        {
                this._balance = balance;
        }
 

        public virtual decimal Balance
        {
            get
            {
                return this._balance;
            }
        }

        public virtual void Withdraw(decimal amount)
        {
            this._balance -= amount;
        }

        public virtual void Deposit(decimal amount)
        {
            this._balance += amount;
        }
  
    }
}