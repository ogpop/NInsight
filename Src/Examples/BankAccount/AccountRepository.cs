namespace BankAccount
{
    public class AccountRepository : IAccountRepository
    {
        public CheckingAccount Get(string accountId)
        {
            return new CheckingAccount(10000) { FeeCalculator = new FeeCalculator() };
        }

        public void Save(CheckingAccount account)
        {
            //
        }
    }
}