using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class DepositCommand
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }

    }
}
