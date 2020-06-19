using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.ViewModels
{
    public class IndexView
    {
        public IEnumerable<Sub_C2B> C2BView { get; set; }
        public User_Works Userwork { get; set; }
        public Sub_C2B subc2b{ get; set; }

    }
}
