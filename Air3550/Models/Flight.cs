using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public long Id { get; set; }
        public int PlaneModel { get; set; }
        public int SeatsBooked { get; set; }
        public double Price { get; set; }
        public int PointsGenerated { get; set; }
        public int Distance { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int SourceAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public string SourceCity { get; set; }
        public string DestCity { get; set; }
    }
}
