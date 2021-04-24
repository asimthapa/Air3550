using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class Transaction
    {
        public long Id { get; set; }
        public int CustomerId { get; set; }
        public long CreditCardNumber { get; set; }
        public int Amount { get; set; }
    }
}
