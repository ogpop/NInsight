namespace BankAccount
{
    public class TransferCommand
    {
        public string DebitAccountId { get; set; }

        public string CreditAccountId { get; set; }

        public decimal Amount { get; set; }
    }
}