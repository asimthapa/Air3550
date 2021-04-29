using Air3550.Data;
using Air3550.Models;
using System.Diagnostics;

namespace Air3550.Services
{
    public class EmployeeService
    {
        public void AddEmployee(Employee employee)
        {
            using var db = new ApplicationDbContext();
            db.Employees.Add(employee);
            db.SaveChanges();
            Debug.WriteLine("Added new employee: " + employee.Type.ToString());
        }

        public Employee GetEmployeeById(long id)
        {
            using var db = new ApplicationDbContext();
            return db.Employees.Find(id);
        }
    }
}
