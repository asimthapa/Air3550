using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class Customer: User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public long PointsAvailable { get; set; }
        public long PointsUsed { get; set; }
        public int Credits { get; set; }
        public Address Address { get; set; }
        public long PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public long CreditCard { get; set; }
    }
}
