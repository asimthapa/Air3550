using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Air3550.Services
{
    public class CustomerService
    {
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer"> customer to add</param>
        /// <returns>Newly created customer Id</returns>
        public long AddCustomer(Customer customer)
        {
            using var dbContext = new ApplicationDbContext();
            customer.Id = GenerateUserId();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            Debug.WriteLine("Customer Added: " + customer.Id);
            return customer.Id;
        }

        public long AddCustomer(Customer customer, bool defaultFlag)
        {
            using var dbContext = new ApplicationDbContext();
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
            Debug.WriteLine("Customer Added: " + customer.Id);
            return customer.Id;
        }

        /// <summary>
        /// Find customer by id
        /// </summary>
        /// <param name="id"> Customer id</param>
        /// <returns>Customer found, or null</returns>
        public Customer GetCustomerById(long id)
        {
            using var dbContext = new ApplicationDbContext();
            Customer customer = dbContext.Customers.Find(id);
            return customer;
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer"> customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            using var dbContext = new ApplicationDbContext();
            dbContext.Customers.Update(customer);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// generate unique 6 digit user id
        /// </summary>
        /// <returns> unique 6 digit id</returns>
        private long GenerateUserId()
        {
            using var dbContext = new ApplicationDbContext();
            long id;
            var rand = new Random();
            while (true)
            {
                id = rand.Next(100000, 1000000);
                var user = dbContext.Customers.Find(id);
                if (user == null) { return id; }
            }
        }
    }
}
