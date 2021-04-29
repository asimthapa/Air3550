using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Services
{
    public class FlightManagerService
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Generate Flight Manifest. Only Flight Manager is authorized.
        /// </summary>
        /// <param name="employee"> employee generating flight manifest</param>
        /// <param name="flightId"></param>
        /// <returns>Flight Manifest</returns>
        public FlightManifest GetFlightManifest(Employee employee, int flightId)
        {
            if (employee.Type != EmployeeType.FLIGHT_MANAGER)
            {
                return null;
            }

            List<FlightBookInfo> fbis = dbContext.FlightBookInfos.Where(fbi => fbi.FlightId == flightId && fbi.FlightStatus == FlightStatus.TAKEN).ToList();
            FlightManifest manifest = new()
            {
                FlightId = flightId,
                PassengerInfo = new Dictionary<long, string>()
            };
            foreach (var fInfo in fbis)
            {
                var passenger = dbContext.Customers.Find(fInfo.UserId);
                String fullName = passenger.FirstName + " " + passenger.LastName;
                manifest.PassengerInfo.Add(fInfo.UserId, fullName);
            }
            return manifest;
        }
    }
}
