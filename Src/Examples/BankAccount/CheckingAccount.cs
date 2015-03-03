namespace BankAccount
{
    public class CheckingAccount
    {
        private decimal _balance;

        public CheckingAccount(decimal balance)
        {
            this._balance = balance;
        }

        public FeeCalculator FeeCalculator { get; set; }

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
            var fee = this.FeeCalculator.DepositFee(amount);
            this._balance += amount - fee;
        }
    }
}