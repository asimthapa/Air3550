using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class FlightManifest
    {
        public int FlightId { get; set; }
        public Dictionary<long, String> PassengerInfo { get; set; }
    }
}
