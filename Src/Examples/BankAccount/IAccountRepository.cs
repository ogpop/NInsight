namespace BankAccount
{
    public interface IAccountRepository
    {
        CheckingAccount Get(string accountId);

        void Save(CheckingAccount account);
    }
}