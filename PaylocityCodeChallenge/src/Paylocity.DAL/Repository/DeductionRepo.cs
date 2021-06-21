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

            // total paycheck, with a based paid = 2000 and 26 paychecks in a year
            decimal paychecksYear = 2000 * 26;
            // cost of benefits for each employee = 1000/paychecksYear
            decimal costBenefits = (1000 / paychecksYear)*200;            
            // employee dependent benefits
            decimal costDependentBenefits = (500 / paychecksYear)*200;           
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
            return _ctx.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _ctx.Employees.Add(employee);
        }

        public bool SaveChanges()
        {
            return (_ctx.SaveChanges() > 0);
        }

        #region Old Code Remove before upload to GitHub
        public async Task<HttpResponseMessage> GetEmployeeAsync()
        {
            return await client.GetAsync($"https://paylocitycodingchallenge-default-rtdb.firebaseio.com/employee.json");
        }

        public async Task<HttpResponseMessage> AddEmployeeAsync(Employee employee)
        {
            var jsonData = new JsonHttpContent(employee);
            return await client.PostAsync($"https://paylocitycodingchallenge-default-rtdb.firebaseio.com/employee.json", jsonData);
        }
        #endregion
    }
}
