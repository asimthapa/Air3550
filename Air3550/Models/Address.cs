using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
#nullable enable
        public string? Address2 { get; set; }
#nullable disable
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
