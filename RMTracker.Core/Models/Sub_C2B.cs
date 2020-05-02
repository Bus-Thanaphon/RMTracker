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
        public string C2BNo { get; set; }
        public string SubC2B { get; set; }
        public int countno { get; set; }
        [DisplayName("Station 1")]
        public string OrderID_Lamination { get; set; }
        [DisplayName("Station 2")]
        public string OrderID_Cut { get; set; }
        [DisplayName("Station 3")]
        public string OrderID_EdgeBanding { get; set; }
        [DisplayName("Station 4")]
        public string OrderID_Drill { get; set; }
        [DisplayName("Station 5")]
        public string OrderID_Packing { get; set; }
        [DisplayName("Station 6")]
        public string OrderID_Pickup { get; set; }

    }
}
