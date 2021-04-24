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
        /// <summary>
        /// log in user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns>true if user can be logged in, or false</returns>
        public Boolean Login(string id, string password)
        {
            User user;
            using var db = new AppDBContext();

            if (id[0] == 'A')
            {
                id = id[1..];
                user = db.Employees.Find(id);
            }
            user = db.Customers.Find(id);

            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
