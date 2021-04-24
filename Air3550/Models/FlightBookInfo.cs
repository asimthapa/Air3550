using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class FlightBookInfo
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public int FlightId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int PaymentAmount { get; set; }
        public FlightStatus FlightStatus { get; set; }
    }
}
