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
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string name { get; set; }

        public string lastname { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal deduction { get; set; }
        #region Navigation Properties
        /// <summary>
        /// A list containing all the Dependents related to this Employee
        /// </summary>
        [JsonPropertyName("dependents")]
        public virtual ICollection<Dependent> Dependents { get; set; }
        #endregion
    }
}
