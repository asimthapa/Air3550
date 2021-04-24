using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Services
{
    public class AccountingManagerService
    {
        /// <summary>
        /// Generate Financial Report. Only Accounting Manager can do use this service.
        /// </summary>
        /// <param name="model">plane model to use</param>
        public static FinancialReport GenerateReport(Employee employee, DateTime startDate, DateTime endDate)
        {
            if (employee.Type != EmployeeType.ACCOUNTING_MANAGER)
            {
                return null;
            }
            using var db = new AppDBContext();
            FinancialReport report = new ();
            var flights = db.Flights.Where(flight => flight.DepartureDate.Date >= startDate && flight.DepartureDate.Date <= endDate).OrderBy(flight => flight.DepartureDate);
            double totalIncome = 0;
            foreach(var flight in flights)
            {
                report.FlightIds.Add(flight.Id);
                var totalCapacity = db.Planes.Find(flight.Id).Capacity;
                report.FlightOccupancyPercentages.Add(flight.SeatsBooked / totalCapacity * 100);
                var flightIncome = flight.Price * flight.SeatsBooked;
                report.IncomePerFlight.Add(flightIncome);
                totalIncome += flightIncome;
            }
            return report;
        }
    }
}
