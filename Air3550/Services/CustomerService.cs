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
    class CustomerService
    {
        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer"> customer to add</param>
        public void AddCustomer(Customer customer)
        {
            customer.Id = GenerateUserId();
            using var db = new AppDBContext();
            db.Customers.Add(customer);
            db.SaveChanges();
            Debug.WriteLine("Customer Added " + customer.Id);
        }

        /// <summary>
        /// Find customer by id
        /// </summary>
        /// <param name="id"> Customer id</param>
        /// <returns>Customer found, or null</returns>
        public Customer GetCustomerById(long id)
        {
            using var db = new AppDBContext();
            Customer customer = db.Customers.Find(id);
            return customer;
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer"> customer to update</param>
        public void UpdateCustomer(Customer customer)
        {
            using var db = new AppDBContext();
            db.Customers.Update(customer);
            db.SaveChanges();
        }

        /// <summary>
        /// generate unique 6 digit user id
        /// </summary>
        /// <returns> unique 6 digit id</returns>
        private int GenerateUserId()
        {
            using var db = new AppDBContext();
            int id;
            var rand = new Random();
            while (true)
            {
                id = rand.Next(100000, 1000000);
                var user = db.Customers.Find(id);
                if (user == null) { return id; }
            }
        }

    }
}
