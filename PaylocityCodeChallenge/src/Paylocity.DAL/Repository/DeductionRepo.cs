using Paylocity.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using Paylocity.DAL.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Paylocity.DAL.Repository
{
    public class DeductionRepo : IDeductionRepo
    {
        private static HttpClient client = new HttpClient();
        private PaylocityDbContext _ctx;
        public DeductionRepo(PaylocityDbContext ctx)
        {
            _ctx = ctx;
        }

        public decimal CalcDeduction(Employee employee)
        {
            //NumberFormatInfo fixedTwo = new NumberFormatInfo();
            //fixedTwo.NumberDecimalDigits = 2;

            // paychecksYear = 2000(based paid) * 26(paychecks in a year)
            decimal paychecksYear = 52000;
            // cost of benefits for each employee = 1000/paychecksYear
            decimal costBenefits = (1000 * 100) / paychecksYear;            
            // employee dependent benefits
            decimal costDependentBenefits = (500 * 100) / paychecksYear;           
            // calculate each dependents if any
            if (employee != null)
            {
                // if an employee name starts with "A"
                if (employee.name.ToUpper().StartsWith("A"))
                {
                    costBenefits = (10 / 100) * costBenefits;
                }
                if (employee.Dependents.Any())
                {
                    foreach (Dependent dependent in employee.Dependents)
                    {
                        if (dependent.name.ToUpper().StartsWith("A"))
                        {
                            costBenefits += (10 / 100) * costDependentBenefits;
                        }
                        else
                        {
                            costBenefits += costDependentBenefits;
                        }
                        
                    }
                }

            }
            
            return costBenefits;
        }        

        public IEnumerable<Employee> GetEmployees()
        {
            return _ctx.Employees                
                .Include(e => e.Dependents)                
                .ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _ctx.Employees.Include(e => e.Dependents).FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _ctx.Employees.Add(employee);
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            _ctx.Entry(employee).State = EntityState.Modified;
            var putEmployee =
                    _ctx.Employees
                    .Where(e => e.Id == employee.Id)
                    .Include(e => e.Dependents)
                    .FirstOrDefault();
            try
            {
                if (putEmployee != null)
                {
                    putEmployee.name = employee.name;
                    putEmployee.lastname = employee.lastname;                   
                    putEmployee.Dependents = employee.Dependents;
                }
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    throw new ArgumentException($"Employee {employee.name} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() > 0);
        }

        private bool EmployeeExists(int id) => _ctx.Employees.Any(e => e.Id == id);


    }
}
