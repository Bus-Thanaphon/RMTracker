using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class WorksDenine : BaseEntity
    {
        public string SONO { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StationID { get; set; }
        public string Station { get; set; }
        [DisplayName("เหตุผล")]
        public string Reason { get; set; }
        [DisplayName("อื่นๆ")]
        public string Other_Reason { get; set; }
        public IEnumerable<ReasonDenine> Reason_List { get; set; }
        public bool CheckBoxDetail { get; set; }
    }
}
