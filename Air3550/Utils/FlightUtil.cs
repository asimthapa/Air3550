using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static Air3550.Constants.AppConstants;
using Air3550.Models;
using Air3550.Data;
using Microsoft.EntityFrameworkCore;

namespace Air3550.Utils
{
    /// <summary>
    /// Util class for Flight
    /// </summary>
    public class FlightUtil
    {
        static readonly Random random = new();

        /// <summary>
        /// Get straight line distance between two airports in miles
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns></returns>
        public static int DistanceBetweenAirports(Airport a1, Airport a2)
        {
            double _equatorialEarthRadius = 6378.1370D;
            double _d2r = (Math.PI / 180D);

            double lat1 = a1.Latitude;
            double long1 = a1.Longitude;
            double lat2 = a2.Latitude;
            double long2 = a2.Longitude;

            double dlong = (long2 - long1) * _d2r;
            double dlat = (lat2 - lat1) * _d2r;
            double a = Math.Pow(Math.Sin(dlat / 2D), 2D) + Math.Cos(lat1 * _d2r) * Math.Cos(lat2 * _d2r) * Math.Pow(Math.Sin(dlong / 2D), 2D);
            double c = 2D * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1D - a));
            double d = _equatorialEarthRadius * c;

            return (int) (d * 0.62137119224);
        }

        /// <summary>
        /// Calculate total cost for the flight
        /// 7pm - 12am || 5am - 8 am= 10% discout, 12 am - 5am = 20% discount
        /// </summary>
        /// <param name="departureTime"></param>
        /// <param name="arrivalTime"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        private static double FlightCost(DateTime departureTime, DateTime arrivalTime, int distance)
        {
            int fixedCost = 50;
            double distanceCost = 0.12 * distance;
            int tsaFee = 8;
            double totalCost = fixedCost + distanceCost + tsaFee;
            
            if (departureTime.Hour < 5)
            {
                totalCost -= 0.2 * totalCost;
            }
            else if ((departureTime.Hour >= 5 && departureTime.Hour < 8) || arrivalTime.Hour >= 19)
            {
                totalCost -= 0.1 * totalCost;
            }

            return totalCost;
        }

        /// <summary>
        /// Generate a tuple of departure DateTime and arrival DateTime
        /// </summary>
        /// <param name="startDate"> arrival date (not time)</param>
        /// <param name="distance"> distance between two airports</param>
        /// <returns>Tuple of departure date and arrival date</returns>
        private static Tuple<DateTime, DateTime> GenerateFlightDate(DateTime startDate ,int distance)
        {
            DateTime departureDateTime = startDate.AddMinutes(random.Next(0, 1439));
            DateTime arrivalDateTime = departureDateTime.AddMinutes((int)(30 + 60d / 500 * distance));
            return new Tuple<DateTime, DateTime>(departureDateTime, arrivalDateTime);
        }

        /// <summary>
        /// Generate and insert flights for upto 6 month in advance
        /// </summary>
        public static void InsertFlights()
        {
            using var dbContext = new ApplicationDbContext();
            int count = 0;
            int totalDays = 180;
            DateTime startDate = DateTime.Now.Date;

            var flightGeneratedInfo = dbContext.SystemInfos.Where(s => s.Type == SystemInfoType.FLIGHTS_GENERATED_TILL).FirstOrDefault();
            if (flightGeneratedInfo == null)
            {
                dbContext.SystemInfos.Add(new SystemInfo() { Type = SystemInfoType.FLIGHTS_GENERATED_TILL, FlightsGeneratedTill = DateTime.Now.Date.AddDays(totalDays) });
                dbContext.SaveChanges();
            }
            else if (flightGeneratedInfo.FlightsGeneratedTill.AddDays(180).Date != DateTime.Now.Date)
            {
                startDate = flightGeneratedInfo.FlightsGeneratedTill.AddDays(1);
                totalDays = 180 - (flightGeneratedInfo.FlightsGeneratedTill.Date - DateTime.Now.Date).Days;
            }
            else
            {
                return;
            }
            
            int sourceAirportId;
            int destAirportId;
            Airport sourceAirport = default;
            Airport destAirport = default;
            int flightDistance;
            double price;
            int planeModel;
            DateTime departureDate;
            DateTime arrivalDate;

            for (int i = 0; i < totalDays; i++)
            {
                // 40 flights everyday
                for (int j = 0; j < 40; j++)
                {
                    count++;
                    flightDistance = 0;
                    while (flightDistance < 500)
                    {
                        sourceAirport = AIRPORTS[random.Next(0, 10)];
                        List<Airport> temp = AIRPORTS.Where(a => a.Name != sourceAirport.Name).ToList();
                        destAirport = temp[random.Next(0, 9)];
                        flightDistance = DistanceBetweenAirports(sourceAirport, destAirport);
                    }
                    sourceAirportId = sourceAirport.Id;
                    destAirportId = destAirport.Id;
                    
                    if (flightDistance < 1000)
                    {
                        planeModel = PLANES[0].Model;
                    }
                    else if (flightDistance < 1500)
                    {
                        planeModel = PLANES[1].Model;
                    }
                    else
                    {
                        planeModel = PLANES[2].Model;
                    }
                    (departureDate, arrivalDate) = GenerateFlightDate(startDate.Date.AddDays(i), flightDistance);
                    price = FlightCost(departureDate, arrivalDate, flightDistance);
                    Flight flight = new()
                    {
                        PlaneModel = planeModel,
                        ArrivalDate = arrivalDate,
                        DepartureDate = departureDate,
                        SourceAirportId = sourceAirportId,
                        DestinationAirportId = destAirportId,
                        Price = price,
                        PointsGenerated = (int)(10 * price),
                        Distance = flightDistance,
                        SourceCity = sourceAirport.City,
                        DestCity = destAirport.City
                    };
                    dbContext.Flights.Add(flight);
                }
            }
            dbContext.SaveChanges();
            Debug.WriteLine(count + " FLIGHTS ADDED");
        }
    }
}
