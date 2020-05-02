using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.ViewModels
{
    public class RMC2BView
    {
        public Sub_C2B Subc2bs { get; set; }
        public User_Works Userwork { get; set; }
        public IEnumerable<User_Works> Userworks { get; set; }
        
    }
}
