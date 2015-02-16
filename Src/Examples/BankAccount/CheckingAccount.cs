using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class CheckingAccount 
    {
        public FeeCalculator FeeCalculator { get; set; }

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

            var fee = FeeCalculator.DepositFee(amount);
            this._balance += amount - fee;
        }
  
    }
}