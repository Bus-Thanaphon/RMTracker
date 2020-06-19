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
    public class sQCController : Controller
    {
        // GET: sQC
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
        IRepository<WorksPause> WorksPauses;
        IRepository<WorksDenine> WorksDenines;
        IRepository<ReasonDenine> ReasonDenines;
        IRepository<ReasonPause> ReasonPauses;

        public sQCController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
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
            WorksPauses = wpcontext;
            WorksDenines = worksdeninecontext;
            ReasonDenines = Reasondcontext;
            ReasonPauses = Reasonpcontext;
        }
        public ActionResult CheckPassword(string Password)
        {
            switch (Password)
            {
                case "8888":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Index()
        {
            List<s_QC> sqc = SQCs.Collection().Where(o => o.C2BNo != null && o.Status_QC != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(sqc);
        }
        public ActionResult IndexSale()
        {
            List<s_QC> sqc = SQCs.Collection().Where(o => o.C2BNo != null && o.Status_QC != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(sqc);
        }
        public ActionResult Start(string Id)
        {
            s_QC qcstart = SQCs.Find(Id);
            if (qcstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                qcstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกQC" || o.Department2 == "ผู้รับผิดชอบแผนกQC" || o.Department3 == "ผู้รับผิดชอบแผนกQC");
                return View(qcstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_QC QCS)
        {
            s_QC qcstart = SQCs.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (qcstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(qcstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                qcstart.User = QCS.User;
                qcstart.Comment = QCS.Comment;
                qcstart.Status_QC = "กำลังผลิต";
                qcstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SQCs.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Pause(string Id)
        {
            s_QC QcPause = SQCs.Find(Id);
            if (QcPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SQCs = new s_QC();
                viewModel.WorksPause = new WorksPause();
                viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "QC");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_QC QcPause = SQCs.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (QcPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                QcPause.Status_QC = "หยุดชั่วคราว";
                QcPause.Time_pauses += 1;
                QcPause.PauseID = Guid.NewGuid().ToString();
                SQCs.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = QcPause.PauseID;
                wpc.StationID = QcPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = QcPause.SubC2B;
                wpc.Station = "sQC";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_QC QcPause = SQCs.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = QcPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (QcPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                QcPause.Status_QC = "กำลังผลิต";
                SQCs.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_QC QcBack = SQCs.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (QcBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                QcBack.Status_QC = "กำลังผลิต";
                SQCs.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Denine(string Id)
        {
            s_QC Denine = SQCs.Find(Id);
            if (Denine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SQCs = new s_QC();
                viewModel.WorksDenine = new WorksDenine();
                viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "QC");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_QC Denine = SQCs.Find(Id);
            lamis = Denine.Id_QC;
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
                Denine.Status_QC = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SQCs.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sQC";
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
            s_QC QCNew = SQCs.Find(Id);
            wpc = QCNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (QCNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                QCNew.Status_QC = "อยู่ในคิว";
                SQCs.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Finish(string Id)
        {
            s_QC qcAccept = SQCs.Find(Id);
            if (qcAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(qcAccept);
            }
        }
        [HttpPost]
        public ActionResult Finish(s_Painting painting, string Id, string idupdate, string cleanu, string packu, string qcu, string picku)
        {
            s_QC qcAccept = SQCs.Find(Id);
            idupdate = qcAccept.Id_QC;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            picku = statuschange.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);
            string finish = "เสร็จ";
            string inqueue = "อยู่ในคิว";

            if (qcAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(qcAccept);
                }
                qcAccept.Status_QC = finish;
                qcAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (pickAccept.Status_Pickup != null)
                {
                    if (qcAccept.Status_QC != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
                SQCs.Commit();
                SPickups.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult AddData(string Id)
        {
            s_QC qcadd = SQCs.Find(Id);
            if (qcadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(qcadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Packing Packs)
        {
            s_QC qcadd = SQCs.Find(Id);

            if (qcadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(qcadd);
                }
                qcadd.Comment = qcadd.Comment;
                SQCs.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            s_QC qcToDelete = SQCs.Find(Id);
            if (qcToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(qcToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_QC qcToDelete = SQCs.Find(Id);
            if (qcToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SQCs.Delete(Id);
                SQCs.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}