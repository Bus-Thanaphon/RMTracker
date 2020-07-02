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
    public class sCleaningController : Controller
    {
        // GET: sCleaning
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

        public sCleaningController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
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
                case "6666":
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
            model.VCleanings = SCleanings.Collection().Where(o => o.C2BNo != null && o.Status_Cleaning != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            model.SCleanings = SCleanings.Find(Id);
            model.WorksPause = new WorksPause();
            model.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำความสะอาด");
            model.WorksDenine = new WorksDenine();
            model.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำความสะอาด");
            return View(model);
        }
        public ActionResult IndexSale(string Id)
        {
            if (Id != null)
            {
                List<s_Cleaning> sacleaning = SCleanings.Collection().Where(o => o.Id == Id).ToList();
                return View(sacleaning);
            }
            List<s_Cleaning> scleaning = SCleanings.Collection().Where(o => o.C2BNo != null && o.Status_Cleaning != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenByDescending(x => x.CreateAt).ToList();
            return View(scleaning);
        }
        public ActionResult Start(string Id)
        {
            s_Cleaning cleanstart = SCleanings.Find(Id);
            if (cleanstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                cleanstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกทำความสะอาด" || o.Department2 == "ผู้รับผิดชอบแผนกทำความสะอาด" || o.Department3 == "ผู้รับผิดชอบแผนกทำความสะอาด");
                return View(cleanstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Cleaning cleans)
        {
            s_Cleaning cleanstart = SCleanings.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (cleanstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cleanstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                cleanstart.User = cleans.User;
                cleanstart.Comment = cleans.Comment;
                cleanstart.Status_Cleaning = "กำลังผลิต";
                cleanstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SCleanings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Pause(string Id)
        //{
        //    s_Cleaning Pause = SCleanings.Find(Id);
        //    if (Pause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SCleanings = new s_Cleaning();
        //        viewModel.WorksPause = new WorksPause();
        //        viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำความสะอาด");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Cleaning CleanPause = SCleanings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (CleanPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                CleanPause.Status_Cleaning = "หยุดชั่วคราว";
                CleanPause.Time_pauses += 1;
                CleanPause.PauseID = Guid.NewGuid().ToString();
                SCleanings.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = CleanPause.PauseID;
                wpc.StationID = CleanPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = CleanPause.SubC2B;
                wpc.Station = "sCleaning";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Cleaning CleanPause = SCleanings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = CleanPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (CleanPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                CleanPause.Status_Cleaning = "กำลังผลิต";
                SCleanings.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Cleaning CleanBack = SCleanings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (CleanBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                CleanBack.Status_Cleaning = "กำลังผลิต";
                SCleanings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Denine(string Id)
        //{
        //    s_Cleaning Denine = SCleanings.Find(Id);
        //    if (Denine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SCleanings = new s_Cleaning();
        //        viewModel.WorksDenine = new WorksDenine();
        //        viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำความสะอาด");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Cleaning Denine = SCleanings.Find(Id);
            lamis = Denine.Id_Cleaning;
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
                Denine.Status_Cleaning = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SCleanings.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sCleaning";
                wdc.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult NewWork(string Id, string wpc)
        {
            s_Cleaning CleanNew = SCleanings.Find(Id);
            wpc = CleanNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (CleanNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                CleanNew.Status_Cleaning = "อยู่ในคิว";
                SCleanings.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Nodata(string Id)
        {
            return View();
        }
        public ActionResult DenineCheck(string Id, WorksDenine workd, string Idback, string DBack)
        {
            //WorksDenine WID = WorksDenines.Find(Id);
            //DBack = WID.StationID;
            s_Cleaning cleanD = SCleanings.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (cleanD == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(workd);
                }
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                cleanD.Status_Cleaning = "อยู่ในคิว";
                SCleanings.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult DetailCheck(string Id, WorksDenine workd, string Idback, string DBack)
        {
            s_Cleaning back = SCleanings.Find(Id);
            DBack = back.DenineID;
            WorksDenine workD = WorksDenines.Find(DBack);
            if (back == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(workd);
                }
                WorksDenines.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Finish(string Id)
        //{
        //    s_Cleaning cleanAccept = SCleanings.Find(Id);
        //    if (cleanAccept == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(cleanAccept);
        //    }
        //}
        //[HttpPost]
        public ActionResult Finish(s_Painting painting, string Id, string idupdate, string cleanu, string packu, string qcu, string picku)
        {
            s_Cleaning cleanAccept = SCleanings.Find(Id);
            idupdate = cleanAccept.Id_Cleaning;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            packu = statuschange.OrderID_Packing;
            s_Packing packAccept = SPackings.Find(packu);
            qcu = statuschange.OrderID_QC;
            s_QC qcAccept = SQCs.Find(qcu);
            picku = statuschange.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);
            string finish = "เสร็จ";
            string inqueue = "อยู่ในคิว";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (cleanAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cleanAccept);
                }
                cleanAccept.Status_Cleaning = finish;
                cleanAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (packAccept.Status_Packing != null)
                {
                    if (cleanAccept.Status_Cleaning != null)
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
                    else if (cleanAccept.Status_Cleaning != null)
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
                    else if (cleanAccept.Status_Cleaning != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult AddData(string Id)
        {
            s_Cleaning cleanadd = SCleanings.Find(Id);
            if (cleanadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cleanadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Cleaning Cleans)
        {
            s_Cleaning cleanadd = SCleanings.Find(Id);

            if (cleanadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cleanadd);
                }
                cleanadd.Comment = Cleans.Comment;
                SCleanings.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            s_Cleaning scleaningToDelete = SCleanings.Find(Id);
            if (scleaningToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(scleaningToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Cleaning scleaningToDelete = SCleanings.Find(Id);
            if (scleaningToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SCleanings.Delete(Id);
                SCleanings.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}