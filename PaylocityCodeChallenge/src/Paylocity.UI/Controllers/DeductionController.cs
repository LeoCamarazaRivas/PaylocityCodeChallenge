using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylocity.DAL.Data;
using Paylocity.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeductionController : ControllerBase
    {
        private readonly IDeductionRepo _repo;
        public DeductionController(IDeductionRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            var response = await _repo.GetEmployeeAsync();

            if (response.IsSuccessStatusCode)
            {
                var employeeResponse = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<Employee>>(employeeResponse);
            }

            return employees;

        }

        [HttpPost]
        public async Task<bool> Post(Employee employee)
        {
            bool flag = false;
            if (employee != null)
            {
                // calculate employee deduction
                employee.deduction = _repo.CalcDeduction(employee);
                // store employee on Firebase DB
                var response = await _repo.AddEmployeeAsync(employee);               
            }
            return flag;

        }        
    }
}
