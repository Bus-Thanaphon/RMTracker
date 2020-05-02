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
        [DisplayName("หมายเลขออเดอร์")]
        public string C2BNo { get; set; }
        [DisplayName("วันที่เริ่ม")]
        public string StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        [DisplayName("จำนวนงานย่อย")]
        public int SubC2B { get; set; }

        [DisplayName("สถานะงาน")]
        public string Job_Status { get; set; }

        [DisplayName("สถานะออเดอร์")]
        public string Order_Status { get; set; }

        [DisplayName("คอมเม้นท์")]
        public string Comment { get; set; }
        [DisplayName("ชื่อลูกค้า")]
        public string Customer { get; set; }

        public List<Sub_C2B> subindex { get; set; }
        //public virtual ICollection<Sub_C2B> Sub_C2B { set; get; }


        public User_Works()
        {
            this.StartDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");
        }

    }
}
