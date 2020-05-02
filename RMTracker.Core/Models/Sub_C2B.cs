using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class Sub_C2B : BaseEntity
    {
        public string SubID { get; set; }
        [DisplayName("หมายเลข C2B")]
        public string C2BNo { get; set; }
        [DisplayName("หมายเลขงานย่อย")]
        public string SubC2B { get; set; }
        public int countno { get; set; }
        [DisplayName("Lamination")]
        public string OrderID_Lamination { get; set; }
        [DisplayName("Cut")]
        public string OrderID_Cut { get; set; }
        [DisplayName("EdgeBanding")]
        public string OrderID_EdgeBanding { get; set; }
        [DisplayName("Drill")]
        public string OrderID_Drill { get; set; }
        [DisplayName("Packing")]
        public string OrderID_Packing { get; set; }
        [DisplayName("Pickup")]
        public string OrderID_Pickup { get; set; }

    }
}
