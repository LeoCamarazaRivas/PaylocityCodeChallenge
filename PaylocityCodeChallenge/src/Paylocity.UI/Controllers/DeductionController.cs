using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paylocity.DAL.Data;
using Paylocity.DAL.Data.Model;
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
        public ActionResult<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            var response = _repo.GetEmployees();

            return Ok(response);

        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var employeeItem = _repo.GetEmployeeById(id);
            if (employeeItem == null)
            {
                return NotFound();
            }
            return Ok(employeeItem);
        }

        [HttpPost]
        public ActionResult<Employee> Post(Employee employee)
        {            
            if (employee != null)
            {
                // calculate employee deduction
                employee.deduction = _repo.CalcDeduction(employee);
                // store employee on Firebase DB
                _repo.AddEmployee(employee);
                _repo.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException(nameof(employee));
            }
            return CreatedAtRoute(nameof(GetEmployeeById), new { id = employee.Id }, employee);

        }        
    }
}
