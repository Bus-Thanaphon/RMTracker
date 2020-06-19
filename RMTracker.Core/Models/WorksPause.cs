using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class WorksPause : BaseEntity
    {
        public string SONO { get; set; }
        public string Station { get; set; }
        public string StationID { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        [DisplayName("เหตุผล")]
        public string Reason { get; set; }
        [DisplayName("อื่นๆ")]
        public string Other_Reason { get; set; }
        public IEnumerable<ReasonPause> Reason_List { get; set; }

    }
}
