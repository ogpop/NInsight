using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class TransferCommand
    {
        public string DebitAccountId { get; set; }
        public string CreditAccountId { get; set; }
        public decimal Amount { get; set; }
        
    }
}
