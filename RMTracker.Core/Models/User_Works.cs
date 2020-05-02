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

        public string StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        [DisplayName("C2B ย่อย")]
        public int SubC2B { get; set; }

        [DisplayName("Job Status")]
        public string Job_Status { get; set; }

        [DisplayName("Order Status")]
        public string Order_Status { get; set; }

        [DisplayName("Comment")]
        public string Comment { get; set; }

        public List<Sub_C2B> subindex { get; set; }
        //public virtual ICollection<Sub_C2B> Sub_C2B { set; get; }


        public User_Works()
        {
            this.StartDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");
        }

    }
}
