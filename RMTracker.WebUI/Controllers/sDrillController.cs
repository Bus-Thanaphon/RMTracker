using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using RMTracker.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class sDrillController : Controller
    {
        // GET: sDrill
        IRepository<Sub_C2B> Subc2bs;
        IRepository<s_Lamination> SLaminations;
        IRepository<s_Cut> SCuts;
        IRepository<s_Edgebanding> SEdgebandings;
        IRepository<s_Drill> SDrills;
        IRepository<s_Painting> SPaintings;
        IRepository<s_Cleaning> SCleanings;
        IRepository<s_Packing> SPackings;
        IRepository<s_QC> SQCs;
        IRepository<s_Pickup> SPickups;
        IRepository<UserList> UserLists;
        IRepository<MachineList> MachineLists;
        IRepository<WorksPause> WorksPauses;
        IRepository<WorksDenine> WorksDenines;
        IRepository<ReasonDenine> ReasonDenines;
        IRepository<ReasonPause> ReasonPauses;

        public sDrillController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext, IRepository<MachineList> mccon
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<UserList> userlcon, IRepository<WorksPause> wpcontext, IRepository<WorksDenine> worksdeninecontext)
        {
            Subc2bs = SubC2Bcontext;
            SLaminations = laminationcontext;
            SCuts = cutcontext;
            SEdgebandings = edgebandingcontext;
            SDrills = drillcontext;
            SPaintings = paintingcontext;
            SCleanings = cleaningcontext;
            SPackings = packingcontext;
            SQCs = qccontext;
            SPickups = pickupcontext;
            UserLists = userlcon;
            MachineLists = mccon;
            WorksPauses = wpcontext;
            WorksDenines = worksdeninecontext;
            ReasonDenines = Reasondcontext;
            ReasonPauses = Reasonpcontext;
        }
        public ActionResult CheckPassword(string Password)
        {
            switch (Password)
            {
                case "4444":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Index(string Id)
        {
            var model = new DenineView();
            model.VDrills = SDrills.Collection().Where(o => o.C2BNo != null && o.Status_Drill != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            model.SDrills = SDrills.Find(Id);
            model.WorksPause = new WorksPause();
            model.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "เจาะ");
            model.WorksDenine = new WorksDenine();
            model.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "เจาะ");
            return View(model);
        }
        public ActionResult IndexSale(string Id)
        {
            if (Id != null)
            {
                List<s_Drill> sadrill = SDrills.Collection().Where(o => o.Id == Id).ToList();
                return View(sadrill);
            }
            List<s_Drill> sdrill = SDrills.Collection().Where(o => o.C2BNo != null && o.Status_Drill != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(sdrill);
        }
        public ActionResult Start(string Id)
        {
            s_Drill drillstart = SDrills.Find(Id);
            if (drillstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                drillstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกเจาะ" || o.Department2 == "ผู้รับผิดชอบแผนกเจาะ" || o.Department3 == "ผู้รับผิดชอบแผนกเจาะ");
                drillstart.Machine_Station = MachineLists.Collection().Where(o => o.Department == "แผนกเจาะ");
                return View(drillstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Drill drills)
        {
            s_Drill drillstart = SDrills.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (drillstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(drillstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                drillstart.User = drills.User;
                drillstart.Comment = drills.Comment;
                drillstart.Machine_Drill = drills.Machine_Drill;
                drillstart.Status_Drill = "กำลังผลิต";
                drillstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SDrills.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Pause(string Id)
        //{
        //    s_Drill Pause = SDrills.Find(Id);
        //    if (Pause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SDrills = new s_Drill();
        //        viewModel.WorksPause = new WorksPause();
        //        viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "เจาะ");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Drill DrillPause = SDrills.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (DrillPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                DrillPause.Status_Drill = "หยุดชั่วคราว";
                DrillPause.Time_pauses += 1;
                DrillPause.PauseID = Guid.NewGuid().ToString();
                SDrills.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = DrillPause.PauseID;
                wpc.StationID = DrillPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = DrillPause.SubC2B;
                wpc.Station = "sDrill";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Drill DrillPause = SDrills.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = DrillPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (DrillPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                DrillPause.Status_Drill = "กำลังผลิต";
                SDrills.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Drill DrillBack = SDrills.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (DrillBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                DrillBack.Status_Drill = "กำลังผลิต";
                SDrills.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Denine(string Id)
        //{
        //    s_Drill Denine = SDrills.Find(Id);
        //    if (Denine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SDrills = new s_Drill();
        //        viewModel.WorksDenine = new WorksDenine();
        //        viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "เจาะ");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Drill Denine = SDrills.Find(Id);
            lamis = Denine.Id_Drill;
            Sub_C2B FindDenine = Subc2bs.Find(lamis);
            if (Denine == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Previousstatus = FindDenine.Status_AStation;
                FindDenine.Status_AStation = "ปฏิเสธ";
                Subc2bs.Commit();
                Denine.Status_Drill = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SDrills.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sDrill";
                wdc.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Nodata(string Id)
        {
            return View();
        }
        public ActionResult NewWork(string Id, string wpc)
        {
            s_Drill New = SDrills.Find(Id);
            wpc = New.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (New == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                New.Status_Drill = "อยู่ในคิว";
                SDrills.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Finish(string Id)
        //{
        //    s_Drill drillAccept = SDrills.Find(Id);
        //    if (drillAccept == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(drillAccept);
        //    }
        //}
        //[HttpPost]
        public ActionResult Finish(s_Drill drill, string Id, string idupdate, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            s_Drill drillAccept = SDrills.Find(Id);
            idupdate = drillAccept.Id_Drill;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            paintu = statuschange.OrderID_Painting;
            s_Painting paintAccept = SPaintings.Find(paintu);
            cleanu = statuschange.OrderID_Cleaning;
            s_Cleaning cleanAccept = SCleanings.Find(cleanu);
            packu = statuschange.OrderID_Packing;
            s_Packing packAccept = SPackings.Find(packu);
            qcu = statuschange.OrderID_QC;
            s_QC qcAccept = SQCs.Find(qcu);
            picku = statuschange.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);
            string finish = "เสร็จ";
            string inqueue = "อยู่ในคิว";
            string wait5 = "รอทำสี";
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (drillAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(drill);
                }
                drillAccept.Status_Drill = finish;
                drillAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (paintAccept.Status_Painting != null)
                {
                    if (drillAccept.Status_Drill != null)
                    {
                        paintAccept.Status_Painting = inqueue;
                    }
                }
                if (cleanAccept.Status_Cleaning != null)
                {
                    if (paintAccept.Status_Painting != null && paintAccept.Status_Painting == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait5;
                    }
                    else if (drillAccept.Status_Drill != null)
                    {
                        cleanAccept.Status_Cleaning = inqueue;
                    }
                }
                if (packAccept.Status_Packing != null)
                {
                    if (cleanAccept.Status_Cleaning != null && cleanAccept.Status_Cleaning == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait6;
                    }
                    else if (paintAccept.Status_Painting != null && paintAccept.Status_Painting == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait5;
                    }
                    else if (drillAccept.Status_Drill != null)
                    {
                        packAccept.Status_Packing = inqueue;
                    }
                }
                if (qcAccept.Status_QC != null)
                {
                    if (packAccept.Status_Packing != null && packAccept.Status_Packing == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait7;
                    }
                    else if (cleanAccept.Status_Cleaning != null && cleanAccept.Status_Cleaning == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait6;
                    }
                    else if (paintAccept.Status_Painting != null && paintAccept.Status_Painting == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait5;
                    }
                    else if (drillAccept.Status_Drill != null)
                    {
                        qcAccept.Status_QC = inqueue;
                    }
                }
                if (pickAccept.Status_Pickup != null)
                {
                    if (qcAccept.Status_QC != null && qcAccept.Status_QC == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait8;
                    }
                    else if (packAccept.Status_Packing != null && packAccept.Status_Packing == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait7;
                    }
                    else if (cleanAccept.Status_Cleaning != null && cleanAccept.Status_Cleaning == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait6;
                    }
                    else if (paintAccept.Status_Painting != null && paintAccept.Status_Painting == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait5;
                    }
                    else if (drillAccept.Status_Drill != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }

                Subc2bs.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult AddData(string Id)
        {
            s_Drill drilladd = SDrills.Find(Id);
            if (drilladd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(drilladd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Drill Drills)
        {
            s_Drill drilladd = SDrills.Find(Id);

            if (drilladd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(drilladd);
                }
                drilladd.Comment = Drills.Comment;
                drilladd.Machine_Drill = Drills.Machine_Drill;
                SDrills.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            s_Drill sdrillToDelete = SDrills.Find(Id);
            if (sdrillToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(sdrillToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Drill sdrillToDelete = SDrills.Find(Id);
            if (sdrillToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SDrills.Delete(Id);
                SDrills.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}