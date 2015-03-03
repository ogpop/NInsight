namespace BankAccount
{
    public class FeeCalculator
    {
        public decimal DepositFee(decimal amount)
        {
            return amount * 0.001m;
        }
    }
}