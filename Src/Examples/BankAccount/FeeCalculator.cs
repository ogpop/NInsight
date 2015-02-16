using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    public class FeeCalculator
    {
        public  decimal DepositFee(decimal amount)
        {
           return amount * 0.001m;
        }
    }
}
