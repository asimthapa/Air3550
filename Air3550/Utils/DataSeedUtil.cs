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
using Air3550.Data;

namespace Air3550.Services
{
    class DataSeedUtil
    {
        private readonly CustomerService customerService = new();
        private readonly EmployeeService employeeService = new();

        public void SeedInitData()
        {
            Debug.WriteLine("SEEDING DATA");

            using var db = new ApplicationDbContext();
            // seed airports
            var airport = db.Airports.FirstOrDefault(a => a.Id!= -1);
            if (airport == null)
            {
                Debug.WriteLine("SEEDING AIRPORTS");
                db.Airports.AddRange(AIRPORTS);
                db.SaveChanges();
            }

            // seed planes
            var plane = db.Planes.FirstOrDefault(p => p.Model != -1);
            if (plane == null)
            {
                Debug.WriteLine("SEEDING PLANES");
                db.Planes.AddRange(PLANES);
                db.SaveChanges();
            }

            // seed user and employees
            var customer = db.Customers.FirstOrDefault(c => c.Id != -1);
            if (customer == null)
            {
            }

            // seed flights
            Debug.WriteLine("SEEDING FLIGHTS");
            FlightUtil.InsertFlights();
        }
        
        public void SeedDefaultActors()
        {
            Debug.WriteLine("SEEDING DEFAULT USERS");

            if (customerService.GetCustomerById(555555) != null) { return; }

            // add customer
            customerService.AddCustomer(new() 
            {
                Id = 555555,
                FirstName = "geo",
                LastName = "hotz",
                PhoneNumber = 0000000000,
                BirthDate = DateTime.Now.Date,
                Email = "geohotz@gmail.com",
                Password = UserUtil.GenerateSHA512("password"),
                Address = new()
                {
                    Address1 = "earth 101",
                    Address2 = "u s of a",
                    State = "OH",
                    City = "rwaarr",
                    ZipCode = 11111
                },
                CreditCardNumber = 2222222222222222
            }, true);
            // add marketing manager
            employeeService.AddEmployee(new() 
            {
                Id = 111111,
                FirstName = "marketing",
                LastName = "manager",
                Email = "marketing.manager@gmail.com",
                Password = UserUtil.GenerateSHA512("password"),
                Type = EmployeeType.MARKETING_MANAGER
            });
            // add accounting manager
            employeeService.AddEmployee(new()
            {
                Id = 222222,
                FirstName = "accounting",
                LastName = "manager",
                Email = "accounting.manager@gmail.com",
                Password = UserUtil.GenerateSHA512("password"),
                Type = EmployeeType.ACCOUNTING_MANAGER
            });
            // add load engineer
            employeeService.AddEmployee(new()
            {
                Id = 333333,
                FirstName = "load",
                LastName = "engineer",
                Email = "load.engineer@gmail.com",
                Password = UserUtil.GenerateSHA512("password"),
                Type = EmployeeType.LOAD_ENGINEER
            });
            // add flight manager
            employeeService.AddEmployee(new()
            {
                Id = 444444,
                FirstName = "flight",
                LastName = "manager",
                Email = "flight.manager@gmail.com",
                Password = UserUtil.GenerateSHA512("password"),
                Type = EmployeeType.FLIGHT_MANAGER
            });
        }
        
    }
}
