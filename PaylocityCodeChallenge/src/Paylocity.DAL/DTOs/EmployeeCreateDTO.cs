using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paylocity.DAL.DTOs
{
    public class EmployeeCreateDTO
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public decimal deduction { get; set; }
        [JsonPropertyName("dependents")]
        public virtual ICollection<DependentDTO> Dependents { get; set; }
    }
}
