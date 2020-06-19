using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class UserList : BaseEntity
    {
        public string ID_User { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public IEnumerable<Departmentuser> Department { get; set; }
        public string Department1 { get; set; }
        public string Department2 { get; set; }
        public string Department3 { get; set; }
    }
}
