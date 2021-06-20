using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Paylocity.DAL.Data.Model
{
    [Table("Dependents")]
    public class Dependent
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string relationshipWithEmployee { get; set; }
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        #region Navigation Properties
        /// <summary>
        /// The Employee related to this Dependent
        /// </summary>
        [JsonPropertyName("employee")]
        public Employee Employee { get; set; }
        #endregion
    }
}
