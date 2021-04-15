using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Pasword { get; set; }
        public UserType Type { get; set; }
#nullable enable
        public UserInfo? UserInfo { get; set; }
#nullable disable
    }
}
