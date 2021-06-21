using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.DAL.DTOs
{
    public class DependentDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string relationshipWithEmployee { get; set; }
    }
}
