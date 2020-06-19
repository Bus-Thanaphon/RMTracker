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
    public class User_Works : BaseEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("หมายเลขออเดอร์")]
        [Required(ErrorMessage = "โปรดกรอกข้อมูล")]
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
        [DisplayName("ฝ่ายขาย")]
        public string Name_Sale { get; set; }
        public IEnumerable<UserList> User_Sale { get; set; }

        [DisplayName("หมายเหตุ")]
        public string Comment { get; set; }
        [DisplayName("ชื่อลูกค้า")]
        public string Customer { get; set; }
        public string OnholdID { get; set; }
        public string PreviousStatus { get; set; }
        public int StartDateDay { get; set; }
        public int StartDateMonth { get; set; }
        public DateTime EndODate { get; set; }
        public int EndDateDay { get; set; }
        public int EndDateMonth { get; set; }

        public List<Sub_C2B> subindex { get; set; }
        public List<Sub_C2B> Status_Sub { get; set; }
        //public virtual ICollection<Sub_C2B> Sub_C2B { set; get; }


        public User_Works()
        {
            this.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
            this.EndODate = DateTime.Now;
            this.StartDateDay = CreateAt.Day;
            this.StartDateMonth = CreateAt.Month;
        }

    }
}
