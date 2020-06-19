using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
   public class ReasonPause : BaseEntity
    {
        public string No { get; set; }
        public string Reason { get; set; }
        public string Station { get; set; }
    }
}
