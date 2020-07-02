using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMTracker.Core.Models
{
    public class s_Lamination : BaseEntity
    {
        public string Id_lamination { get; set; }
        public string C2BNo { get; set; }
        [DisplayName("หมายเลขงานย่อย")]
        public string SubC2B { get; set; }
        [DisplayName("จำนวนหยุดงาน")]
        public int Time_pauses { get; set; }
        public string PauseID { get; set; }
        [DisplayName("หมายเหตุ")]
        public string Comment { get; set; }
        [DisplayName("ผู้รับผิดชอบงาน")]
        public string User { get; set; }
        public IEnumerable<UserList> User_Station { get; set; }
        public IEnumerable<MachineList> Machine_Station { get; set; }
        [DisplayName("ปิดผิว")]
        public int Quantity { get; set; }
        [DisplayName("เครื่องจักร")]
        public string Machine_Lamination { get; set; }
        [DisplayName("สถานะพิเศษ")]
        public string Special_status { get; set; }
        [DisplayName("สถานะ")]
        public string Status_Lamination { get; set; }
        public string Status_Show { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string DenineID { get; set; }
        public bool Status_Check { get; set; }
        public string Urgent_Status { get; set; }
        public string Denine_Check { get; set; }
        [DisplayName("ชื่อลูกค้า")]
        public string Customer { get; set; }
        [DisplayName("วันรับของ")]
        public string Duedate { get; set; }
        [DisplayName("ชื่อเซลล์")]
        public string NameSale { get; set; }
    }
}
