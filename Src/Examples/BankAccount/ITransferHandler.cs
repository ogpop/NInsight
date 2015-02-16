namespace BankAccount
{
    public interface ITransferHandler
    {
        void Handle(TransferCommand command);
    }
}