using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using Air3550.Models;
using Air3550.Utils;
using static Air3550.Constants.AppConstants;

namespace Air3550.Data
{
    class DataSeed
    {
        public static void SeedInitData()
        {

            using var db = new AppDBContext();
            
            var airport = db.Airports.FirstOrDefault(a => a.Longitude != -1);
            if (airport == null)
            {
                Debug.WriteLine("SEEDING AIRPORTS");
                db.Airports.AddRange(AIRPORTS);
                db.SaveChangesAsync();
            }

            var plane = db.Planes.FirstOrDefault( p => p.Capacity != -1);
            if (plane == null)
            {
                Debug.WriteLine("SEEDING PLANES");
                db.Planes.AddRange(PLANES);
                db.SaveChangesAsync();
            }

            FlightUtils.InsertFlights();
        }
    }
}
