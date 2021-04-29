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
        private readonly ApplicationDbContext dbContext = new();
        private readonly PaymentService paymentService;

        public FlightService() {}

        /// <summary>
        /// get list of flights based on given queries
        /// </summary>
        /// <param name="sourceCity">source city</param>
        /// <param name="destCity">destination city</param>
        /// <param name="departDate">departure date</param>
        /// <returns> list of flights</returns>
        public List<Flight> GetFlights(string sourceCity, string destCity, DateTime departDate)
        {
            return dbContext.Flights
                .Where(
                flight => 
                flight.DepartureDate.Date == departDate.Date && 
                flight.SourceCity.ToLower() == sourceCity.ToLower() && 
                flight.DestCity.ToLower() == destCity.ToLower()).ToList<Flight>();
        }

        /// <summary>
        /// Book a flight
        /// </summary>
        /// <param name="flightBookInfo"> Flight info to book a flight</param>
        public void BookFlight(FlightBookInfo flightBookInfo)
        {
            var flight = dbContext.Flights.Find(flightBookInfo.FlightId);
            if (flight != null)
            {

                var plane = dbContext.Planes.Find(flight.PlaneModel);
                if (flight.SeatsBooked < plane.Capacity)
                {
                    flight.SeatsBooked++;
                    dbContext.Flights.Update(flight);
                    dbContext.FlightBookInfos.Add(flightBookInfo);
                    dbContext.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// Cancel booked flight
        /// </summary>
        /// <param name="flightBookInfo"></param>
        public void CancelFlight(FlightBookInfo flightBookInfo)
        {
            var flight = dbContext.Flights.Find(flightBookInfo.FlightId);
            if (flight != null && (flight.DepartureDate - DateTime.Now).TotalHours > 24)
            {
                flightBookInfo.FlightStatus = FlightStatus.CANCELLED;
                dbContext.FlightBookInfos.Update(flightBookInfo);

                flight.SeatsBooked--;
                dbContext.Flights.Update(flight);
                dbContext.SaveChanges();
                
                paymentService.Refund(flightBookInfo.UserId, flightBookInfo.PaymentType, flightBookInfo.PaymentAmount);
            }
        }
    
        /// <summary>
        /// Update flight status to taken from booked
        /// </summary>
        /// <param name="flightId"> id of take off flight</param>
        public void UpdateFlightStatusToTaken(int flightId)
        {
            var infos = dbContext.FlightBookInfos.Where(fbi => fbi.FlightId == flightId && fbi.FlightStatus == FlightStatus.BOOKED);
            foreach(var info in infos)
            {
                info.FlightStatus = FlightStatus.TAKEN;
            }
            dbContext.FlightBookInfos.UpdateRange(infos);
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// get flights
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <param name="status">flight status</param>
        /// <returns>list of flights based on passed queries</returns>
        public List<Flight> GetFlights(int userId, FlightStatus status)
        {
            return (List<Flight>)dbContext.FlightBookInfos.Where(fbi => fbi.UserId == userId && fbi.FlightStatus == status);
        }
    }
}
