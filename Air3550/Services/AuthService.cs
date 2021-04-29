using Air3550.Models;
using Air3550.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext dbContext;

        public AuthService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// log in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>true if user can be logged in, or false</returns>
        public Boolean Login(string id, string password)
        {
            User user;

            if (id[0] == 'A')
            {
                id = id[1..];
                user = dbContext.Employees.Find(id);
            }
            user = dbContext.Customers.Find(id);

            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
