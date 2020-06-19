using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class MachineList : BaseEntity
    {
        public string ID_Machine { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Department { get; set; }
    }
}
