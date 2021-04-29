using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Services
{
    public class LoadEngineerService
    {

        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Update Flight Date. Only Load Engineer can do it
        /// </summary>
        /// <param name="employee">employee making change</param>
        /// <param name="flightId">flight id</param>
        /// <param name="arrivalDate">new arrival date</param>
        /// <param name="departDate"> new depart date</param>
        public void UpdateFlightDate(Employee employee, int flightId, DateTime arrivalDate, DateTime departDate)
        {
            if (employee.Type != EmployeeType.LOAD_ENGINEER)
            {
                return;
            }
            var flight = dbContext.Flights.Find(flightId);
            if (flight != null)
            {
                flight.ArrivalDate = arrivalDate;
                flight.DepartureDate = departDate;
                dbContext.Flights.Update(flight);
                dbContext.SaveChanges();
            }
        }
    }
}
