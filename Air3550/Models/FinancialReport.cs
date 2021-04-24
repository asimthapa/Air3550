using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air3550.Models
{
    public class FinancialReport
    {
        public List<long> FlightIds { get; set; }
        public List<double> FlightOccupancyPercentages { get; set; }
        public List<double> IncomePerFlight { get; set; }
        public double TotalIncome { get; set; }
    }
}
