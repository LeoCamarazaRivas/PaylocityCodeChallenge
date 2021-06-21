using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paylocity.DAL.DTOs
{
    public class DependentCreateDTO
    {
        public string name { get; set; }
        public string lastname { get; set; }
        public string relationshipWithEmployee { get; set; }
        [ForeignKey(nameof(EmployeeCreateDTO))]
        public int EmployeeId { get; set; }       
        [JsonPropertyName("employee")]
        public EmployeeCreateDTO Employee { get; set; }
    }
}
