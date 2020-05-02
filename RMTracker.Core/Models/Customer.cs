using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }
        public string NameUser { get; set; }
        public string Permission { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}
