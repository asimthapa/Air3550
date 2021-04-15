using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class SystemInfo
    {
        [Key]
        public SystemInfoType Type { get; set; }
        public DateTime FlightsGeneratedTill { get; set; }
    }
}
