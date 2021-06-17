using Paylocity.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;

namespace Paylocity.DAL.Repository
{
    public class DeductionRepo : IDeductionRepo
    {
        private static HttpClient client = new HttpClient();
        public async Task<HttpResponseMessage> AddEmployeeAsync(Employee employee)
        {
            var jsonData = new JsonHttpContent(employee);            
            return await client.PostAsync($"https://paylocitycodingchallenge-default-rtdb.firebaseio.com/employee.json", jsonData);
        }

        public decimal CalcDeduction(Employee employee)
        {
            //NumberFormatInfo fixedTwo = new NumberFormatInfo();
            //fixedTwo.NumberDecimalDigits = 2;

            // total paycheck, with a based paid = 2000 and 26 paychecks in a year
            decimal paychecksYear = 2000 * 26;
            // cost of benefits for each employee = 1000/paychecksYear
            decimal costBenefits = (1000 / 12) * paychecksYear;
            // employee dependent benefits
            decimal costDependentBenefits = (500 / 12) * paychecksYear;
            // calculate each dependents if any
            if (employee != null)
            {
                // if an employee name starts with "A"
                if (employee.firstName.ToUpper().StartsWith("A"))
                {
                    costBenefits = (costBenefits / 100) * 10;
                }
                if (employee.dependents.Any())
                {
                    foreach (Dependent dependent in employee.dependents)
                    {
                        if (dependent.firstName.ToUpper().StartsWith("A"))
                        {
                            costBenefits += (costDependentBenefits / 100) * 10;
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

        public async Task<HttpResponseMessage> GetEmployeeAsync()
        {
            return await client.GetAsync($"https://paylocitycodingchallenge-default-rtdb.firebaseio.com/employee.json");
        }
    }
}
