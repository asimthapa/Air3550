using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    /// <summary>
    /// Class to hold flight information
    /// </summary>
    public class Flight
    {
        public int Id { get; set; }
        public Plane Plane { get; set; }
        public List<User> Passengers { get; set; }
        public int Price { get; set; }
        public Airport SourceAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public int Distance { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
    }
}
