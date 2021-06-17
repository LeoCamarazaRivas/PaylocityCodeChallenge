using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.DAL.Data
{
    public class Employee
    {
        //public int employeeId { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("deduction")]
        public decimal deduction { get; set; }
        [JsonProperty("dependents")]
        public List<Dependent> dependents { get; set; }
                
    }

    public class Dependent
    {
        //public int dependentId { get; set; }
        
        public string firstName { get; set; }
        
        public string lastName { get; set; }
                
        public string relationship { get; set; }
    }
}
