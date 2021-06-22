using Paylocity.DAL.Data;
using Paylocity.DAL.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.DAL.Repository
{
    public interface IDeductionRepo
    {
        bool SaveChanges();
        IEnumerable<Employee> GetEmployees();
        void AddEmployee(Employee employee);
        void UpdateEmployee(int id, Employee employee);
        Employee GetEmployeeById(int id);
        decimal CalcDeduction(Employee employee);

    }
}
