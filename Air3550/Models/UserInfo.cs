using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public Address Address { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public int CreditCard { get; set; }

    }
}
