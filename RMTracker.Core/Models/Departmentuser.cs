using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class Departmentuser : BaseEntity
    {
        public int Number { get; set; }
        public string Department { get; set; }
    }
}
