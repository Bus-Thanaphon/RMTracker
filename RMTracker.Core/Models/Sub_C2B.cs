using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RMTracker.Core.Models
{
    public class Sub_C2B : BaseEntity
    {
        public string SubID { get; set; }
        [DisplayName("หมายเลข SO")]
        public string C2BNo { get; set; }
        [DisplayName("หมายเลขงานย่อย")]
        public string SubC2B { get; set; }
        public string SubC2B2 { get; set; }
        public string Urgent_Status { get; set; }
        public string Order_PauseID { get; set; }
        public string Check_PauseID { get; set; }
        public bool CheckBoxLamination { get; set; }

        [DisplayName("ปิดผิว")]
        public string OrderID_Lamination { get; set; }
        public s_Lamination Status_Lamination { get; set; }
        public bool CheckBoxCut { get; set; }
        [DisplayName("ตัด")]
        public string OrderID_Cut { get; set; }
        public s_Cut Status_Cut { get; set; }
        public bool CheckBoxEdgeBanding { get; set; }
        [DisplayName("ปิดขอบ")]
        public string OrderID_EdgeBanding { get; set; }
        public s_Edgebanding Status_EdgeBanding { get; set; }
        public bool CheckBoxDrill { get; set; }
        [DisplayName("เจาะ")]
        public string OrderID_Drill { get; set; }
        public s_Drill Status_Drill { get; set; }
        public bool CheckBoxPainting { get; set; }
        [DisplayName("ทำสี")]
        public string OrderID_Painting { get; set; }
        public s_Painting Status_Painting { get; set; }
        public bool CheckBoxCleaning { get; set; }
        [DisplayName("ทำความสะอาด")]
        public string OrderID_Cleaning { get; set; }
        public s_Cleaning Status_Cleaning { get; set; }
        public bool CheckBoxPacking { get; set; }
        [DisplayName("แพ็ค")]
        public string OrderID_Packing { get; set; }
        public s_Packing Status_Packing { get; set; }
        public bool CheckBoxQC { get; set; }
        [DisplayName("QC")]
        public string OrderID_QC { get; set; }
        public s_QC Status_QC { get; set; }
        public bool CheckBoxPickup { get; set; }
        [DisplayName("จัดส่ง")]
        public string OrderID_Pickup { get; set; }
        public s_Pickup Status_Pickup { get; set; }
        public string Status_AStation { get; set; }
        [DisplayName("เหตุผลยกเลิก")]
        public string Cancel_Reason { get; set; }
        public string StartDate { get; set; }
        public int StartDateDay { get; set; }
        public int StartDateMonth { get; set; }
        public string EndtDate { get; set; }
        public DateTime EndSDate { get; set; }
        public int EndDateDay { get; set; }
        public int EndDateMonth { get; set; }
        public int CountSend { get; set; }
        [DisplayName("หมายเหตุ")]
        public string Comment { get; set; }
        public string Previousstatus { get; set; }

        public Sub_C2B()
        {
            this.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
            this.EndtDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
            this.EndSDate = DateTime.Now;
        }
    }
}
