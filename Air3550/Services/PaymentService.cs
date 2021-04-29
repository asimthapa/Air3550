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

        private readonly ApplicationDbContext dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void PayUsingMoney(Transaction ts, int pointsGenerated)
        {
            dbContext.Transactions.Add(ts);
            dbContext.SaveChanges();
        }

        public void PayUsingCredit(int customerId, int creditAmount)
        {
            Customer customer = dbContext.Customers.Find(customerId);
            if (customer != null && creditAmount <= customer.Credits)
            {
                customer.Credits -= creditAmount;
                dbContext.Customers.Update(customer);
                dbContext.SaveChanges();
            }
        }

        public void PayUsingPoints(int customerId, int points)
        {
            Customer customer = dbContext.Customers.Find(customerId);
            if (customer != null && points <= customer.PointsAvailable)
            {
                customer.PointsAvailable -= points;
                customer.PointsUsed += points;
                dbContext.Customers.Update(customer);
                dbContext.SaveChanges();
            }
        }

        public void RefundMoneyOrCredit(int customerId, int creditAmount)
        {
            Customer customer = dbContext.Customers.Find(customerId);
            if (customer != null)
            {
                customer.Credits += creditAmount;
                dbContext.Customers.Update(customer);
                dbContext.SaveChanges();
            }
        }

        public void RefundPoints(int customerId, int points)
        {
            Customer customer = dbContext.Customers.Find(customerId);
            if (customer != null)
            {
                customer.PointsAvailable += points;
                customer.PointsUsed -= points;
                dbContext.Customers.Update(customer);
                dbContext.SaveChanges();
            }
        }

        public void Refund(int userId, PaymentType paymentType, int amount)
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
