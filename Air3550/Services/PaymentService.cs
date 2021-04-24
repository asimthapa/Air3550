using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Air3550.Models;
using Air3550.Data;

namespace Air3550.Services
{
    public class PaymentService
    {
        public void PayUsingMoney(Transaction ts)
        {
            using var db = new AppDBContext();
            db.Transactions.Add(ts);
            db.SaveChanges();
        }

        public void PayUsingCredit(int customerId, int creditAmount)
        {
            using var db = new AppDBContext();
            Customer customer = db.Customers.Find(customerId);
            if (customer != null && creditAmount <= customer.Credits)
            {
                customer.Credits -= creditAmount;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
        }

        public void PayUsingPoints(int customerId, int points)
        {
            using var db = new AppDBContext();
            Customer customer = db.Customers.Find(customerId);
            if (customer != null && points <= customer.PointsAvailable)
            {
                customer.PointsAvailable -= points;
                customer.PointsUsed += points;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
        }

        public static void RefundMoneyOrCredit(int customerId, int creditAmount)
        {
            using var db = new AppDBContext();
            Customer customer = db.Customers.Find(customerId);
            if (customer != null)
            {
                customer.Credits += creditAmount;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
        }

        public static void RefundPoints(int customerId, int points)
        {
            using var db = new AppDBContext();
            Customer customer = db.Customers.Find(customerId);
            if (customer != null)
            {
                customer.PointsAvailable += points;
                customer.PointsUsed -= points;
                db.Customers.Update(customer);
                db.SaveChanges();
            }
        }

        public static void Refund(int userId, PaymentType paymentType, int amount)
        {
            switch(paymentType)
            {
                case PaymentType.CREDIT:
                case PaymentType.MONEY:
                    RefundMoneyOrCredit(userId, amount);
                    break;
                case PaymentType.POINTS:
                    RefundPoints(userId, amount);
                    break;
            }
        }
    }
}
