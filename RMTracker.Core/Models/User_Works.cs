using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class User_Works : BaseEntity
    {
        [DisplayName("C2B No.")]
        public string C2BNo { get; set; }

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        [DisplayName("Current Station")]
        public string Current_Station { get; set; }

        [DisplayName("Job Status")]
        public string Job_Status { get; set; }

        [DisplayName("Order Status")]
        public string Order_Status { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }

        public User_Works()
        {
            this.StartDate = DateTime.Now;
        }
    }
}
