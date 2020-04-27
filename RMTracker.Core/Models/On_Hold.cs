using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class On_Hold : BaseEntity
    {
        public string OnholdNo { get; set; }
        public string Onhold_Start { get; set; }
        public string Onhold_End { get; set; }
        public string Onhold_Reason { get; set; }
    }
}
