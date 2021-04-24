using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Services
{
    public class FlightService
    {
        /// <summary>
        /// get list of flights based on given queries
        /// </summary>
        /// <param name="sourceCity">source city</param>
        /// <param name="destCity">destination city</param>
        /// <param name="departDate">departure date</param>
        /// <returns> list of flights</returns>
        public List<Flight> GetFlights(string sourceCity, string destCity, DateTime departDate)
        {
            using var db = new AppDBContext();
            return (List<Flight>)db.Flights.Where(flight => flight.DepartureDate.Date == departDate.Date && flight.SourceCity == sourceCity && flight.DestCity == destCity);
        }

        /// <summary>
        /// Book a flight
        /// </summary>
        /// <param name="flightBookInfo"> Flight info to book a flight</param>
        public void BookFlight(FlightBookInfo flightBookInfo)
        {
            using var db = new AppDBContext();
            var flight = db.Flights.Find(flightBookInfo.FlightId);
            if (flight != null)
            {
                var plane = db.Planes.Find(flight.PlaneModel);
                if (flight.SeatsBooked < plane.Capacity)
                {
                    flight.SeatsBooked++;
                    db.Flights.Update(flight);
                    db.FlightBookInfos.Add(flightBookInfo);
                    db.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// Cancel booked flight
        /// </summary>
        /// <param name="flightBookInfo"></param>
        public void CancelFlight(FlightBookInfo flightBookInfo)
        {
            using var db = new AppDBContext();
            var flight = db.Flights.Find(flightBookInfo.FlightId);
            if (flight != null && (flight.DepartureDate - DateTime.Now).TotalHours > 24)
            {
                flightBookInfo.FlightStatus = FlightStatus.CANCELLED;
                db.FlightBookInfos.Update(flightBookInfo);

                flight.SeatsBooked--;
                db.Flights.Update(flight);
                db.SaveChanges();
                
                PaymentService.Refund(flightBookInfo.UserId, flightBookInfo.PaymentType, flightBookInfo.PaymentAmount);
            }
        }
    
        /// <summary>
        /// Update flight status to taken from booked
        /// </summary>
        /// <param name="flightId"> id of take off flight</param>
        public void UpdateFlightStatusToTaken(int flightId)
        {
            using var db = new AppDBContext();
            var infos = db.FlightBookInfos.Where(fbi => fbi.FlightId == flightId && fbi.FlightStatus == FlightStatus.BOOKED);
            foreach(var info in infos)
            {
                info.FlightStatus = FlightStatus.TAKEN;
            }
            db.FlightBookInfos.UpdateRange(infos);
            db.SaveChangesAsync();
        }

        /// <summary>
        /// get flights
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <param name="status">flight status</param>
        /// <returns>list of flights based on passed queries</returns>
        public static List<Flight> GetFlights(int userId, FlightStatus status)
        {
            using var db = new AppDBContext();
            return (List<Flight>)db.FlightBookInfos.Where(fbi => fbi.UserId == userId && fbi.FlightStatus == status);
        }
    }
}
