using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using static Air3550.Constants.AppConstants;
using Air3550.Models;
using Air3550.Data;

namespace Air3550.Utils
{
    /// <summary>
    /// Util class for Flight
    /// </summary>
    public class FlightUtils
    {
        static Random random = new();

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

        private static double FlightCost(DateTime departureTime, DateTime arrivalTime, int distance)
        {
            int fixedCost = 50;
            double distanceCost = 0.12 * distance;
            int tsaFee = 8;

            return 50 + 0.12 * distance + 8;
        }

        private static double FlightTime(int distance)
        {
            return distance;
        }

        /// <summary>
        /// Generate a tuple of departure DateTime and arrival DateTime
        /// </summary>
        /// <param name="startDate"> arrival date (not time)</param>
        /// <param name="distance"> distance between two airports</param>
        /// <returns>Tuple of departure time and arrival time</returns>
        private static Tuple<DateTime, DateTime> generateFlightTime(DateTime startDate ,int distance)
        {
            DateTime departureDateTime = startDate.AddMinutes(random.Next(0, 1439));
            DateTime arrivalDateTime = departureDateTime.AddMinutes((30 + 60 / 500 * distance));

            return new Tuple<DateTime, DateTime>(departureDateTime, arrivalDateTime);
        }

        /// <summary>
        /// Generates flights for upto 6 month in advance
        /// </summary>
        public static void InsertFlights()
        {
            using var db = new AppDBContext();
            var tillDate = db.SystemInfos.FirstOrDefault(s => s.Type == SystemInfoType.FLIGHTS_GENERATED_TILL);
            int totalDays = 180;
            DateTime startDate;
            if (tillDate == null || (tillDate.FlightsGeneratedTill - DateTime.Now).Days < 0)
            {
                startDate = DateTime.Now;
            }
            else
            {
                startDate = tillDate.FlightsGeneratedTill.AddDays(1);
                totalDays = 180 - (tillDate.FlightsGeneratedTill - DateTime.Now).Days;
            }

            
            Airport sourceAirport;
            Airport destAirport;
            int flightDistance;
            int price;
            Plane plane;
            DateTime departureTime;
            DateTime arrivalTime;

            for (int i = 0; i < totalDays; i++)
            {
                // 40 flights everyday
                for (int j = 0; j < 40; j++)
                {
                    sourceAirport = AIRPORTS[random.Next(0, 10)];
                    List<Airport> temp = AIRPORTS.Where(a => a.Name != sourceAirport.Name).ToList();
                    destAirport = temp[random.Next(0, 9)];
                    flightDistance = DistanceBetweenAirports(sourceAirport, destAirport);
                    if (flightDistance < 500)
                    {
                        plane = PLANES[0];
                    }
                    else if (flightDistance < 1000)
                    {
                        plane = PLANES[1];
                    }
                    else
                    {
                        plane = PLANES[2];
                    }
                    (departureTime, arrivalTime) = generateFlightTime(startDate.Date.AddDays(i), flightDistance);
                    if (departureTime.Date != arrivalTime.Date)
                    {
                        Debug.WriteLine(sourceAirport.Name + " : " + departureTime + "   -------------   " + destAirport.Name + " : " + arrivalTime);
                    }
                }
            }
        }
    }
}
