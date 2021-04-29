using Air3550.Data;
using Air3550.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Air3550.Utils
{
    class UserUtil
    {
        /// <summary>
        /// Generate SHA512 hash for given string
        /// </summary>
        /// <param name="password"> string (preferrably password) to hash</param>
        /// <returns>String representation of hash.</returns>
        public static string GenerateSHA512(string password)
        {
            SHA512 shaM = SHA512.Create();
            byte[] data = Encoding.UTF8.GetBytes(password);
            byte[] result = shaM.ComputeHash(data);
            return BitConverter.ToString(result);
        }

        public static void StoreLoggedUser(long id, bool isCustomer)
        {
            ClearLoggedUser();
            using var db = new ApplicationDbContext();
            db.LoggedUser.Add(new()
            {
                Id = id,
                IsCustomer = isCustomer
            });
            db.SaveChanges();
            Debug.WriteLine($"USER LOGGED IN WITH ID: {id} ISCUSTOMER: {isCustomer}");
        }

        public static LoggedUser GetLoggedUser()
        {
            using var db = new ApplicationDbContext();
            return db.LoggedUser.First(s => s.Id != -1);
        }


        public static void ClearLoggedUser()
        {
            using var db = new ApplicationDbContext();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var cache = db.LoggedUser.Where(s => s.Id != -1).FirstOrDefault<LoggedUser>();
            if (cache != null)
            {
                db.LoggedUser.Remove(new() { Id = cache.Id });
                db.SaveChanges();
            }
        }

        public static bool IsUserLogged()
        {
            using var db = new ApplicationDbContext();
            return db.LoggedUser.Any();
        }
    }
}
