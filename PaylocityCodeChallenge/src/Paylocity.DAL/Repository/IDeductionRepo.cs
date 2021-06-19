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
        Task<HttpResponseMessage> AddEmployeeAsync(Employee employee);
        Task<HttpResponseMessage> GetEmployeeAsync();
        decimal CalcDeduction(Employee employee);

    }
}
