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
    public class sPickupController : Controller
    {
        // GET: sPickup
        IRepository<User_Works> Userworks;
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

        public sPickupController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<User_Works> userw, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<UserList> userlcon, IRepository<WorksPause> wpcontext, IRepository<WorksDenine> worksdeninecontext)
        {
            Userworks = userw;
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
                case "9999":
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
            model.VPickups = SPickups.Collection().Where(o => o.C2BNo != null && o.Status_Pickup != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            model.SPickups = SPickups.Find(Id);
            model.WorksPause = new WorksPause();
            model.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "จัดส่ง");
            model.WorksDenine = new WorksDenine();
            model.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "จัดส่ง");
            return View(model);
        }
        public ActionResult IndexSale(string Id)
        {
            if (Id != null)
            {
                List<s_Pickup> sapickup = SPickups.Collection().Where(o => o.Id == Id).ToList();
                return View(sapickup);
            }
            List<s_Pickup> spickup = SPickups.Collection().Where(o => o.C2BNo != null && o.Status_Pickup != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(spickup);
        }
        public ActionResult Start(string Id)
        {
            s_Pickup pickstart = SPickups.Find(Id);
            if (pickstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                pickstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกจัดส่ง" || o.Department2 == "ผู้รับผิดชอบแผนกจัดส่ง" || o.Department3 == "ผู้รับผิดชอบแผนกจัดส่ง");
                return View(pickstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Pickup picks)
        {
            s_Pickup pickstart = SPickups.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (pickstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(pickstart);
                }
                pickstart.User = picks.User;
                pickstart.Comment = picks.Comment;
                pickstart.Status_Pickup = "กำลังผลิต";
                pickstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SPickups.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Pause(string Id)
        //{
        //    s_Pickup Pause = SPickups.Find(Id);
        //    if (Pause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SPickups = new s_Pickup();
        //        viewModel.WorksPause = new WorksPause();
        //        viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "จัดส่ง");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Pickup PickPause = SPickups.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PickPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                PickPause.Status_Pickup = "หยุดชั่วคราว";
                PickPause.Time_pauses += 1;
                PickPause.PauseID = Guid.NewGuid().ToString();
                SPickups.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = PickPause.PauseID;
                wpc.StationID = PickPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = PickPause.SubC2B;
                wpc.Station = "sPickup";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Pickup PickPause = SPickups.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = PickPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (PickPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PickPause.Status_Pickup = "กำลังผลิต";
                SPickups.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Pickup PickBack = SPickups.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PickBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PickBack.Status_Pickup = "กำลังผลิต";
                SPickups.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Denine(string Id)
        //{
        //    s_Pickup Denine = SPickups.Find(Id);
        //    if (Denine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SPickups = new s_Pickup();
        //        viewModel.WorksDenine = new WorksDenine();
        //        viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "จัดส่ง");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Pickup Denine = SPickups.Find(Id);
            lamis = Denine.Id_Pickup;
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
                Denine.Status_Pickup = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SPickups.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sPickup";
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
            s_Pickup PickNew = SPickups.Find(Id);
            wpc = PickNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (PickNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                PickNew.Status_Pickup = "อยู่ในคิว";
                SPickups.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Finish(string Id)
        //{
        //    s_Pickup pickAccept = SPickups.Find(Id);
        //    if (pickAccept == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(pickAccept);
        //    }
        //}
        //[HttpPost]
        public ActionResult Finish(s_Pickup pickup, string Id, string idupdate, string oderupdate)
        {
            s_Pickup pickAccept = SPickups.Find(Id);
            idupdate = pickAccept.Id_Pickup;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            oderupdate = statuschange.SubID;
            User_Works orderu = Userworks.Find(oderupdate);
            string finish = "เสร็จ";

            if (pickAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(pickAccept);
                }
                pickAccept.Status_Pickup = finish;
                pickAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "เสร็จงาน";
                statuschange.EndSDate = DateTime.Now;
                statuschange.EndDateDay = statuschange.EndSDate.Day;
                statuschange.EndDateMonth = statuschange.EndSDate.Month;
                List<Sub_C2B> CheckStatus = Subc2bs.Collection().Where(o => o.SubID == orderu.Id).ToList();
                foreach (var i in CheckStatus)
                {
                    if (i.Status_AStation != "เสร็จงาน" && i.Status_AStation != "ยกเลิก")
                    {
                        Subc2bs.Commit();
                        SPickups.Commit();
                        Subc2bs.Commit();
                        return RedirectToAction("Index");
                    }
                }
                Subc2bs.Commit();
                SPickups.Commit();
                Subc2bs.Commit();
                orderu.Order_Status = "เสร็จแล้ว";
                orderu.EndODate = DateTime.Now;
                orderu.EndDateDay = orderu.EndODate.Day;
                orderu.EndDateMonth = orderu.EndODate.Month;
                Userworks.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult AddData(string Id)
        {
            s_Pickup pickadd = SPickups.Find(Id);
            if (pickadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(pickadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Pickup Picks)
        {
            s_Pickup pickadd = SPickups.Find(Id);

            if (pickadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(pickadd);
                }
                pickadd.Comment = Picks.Comment;
                SPickups.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            s_Pickup spickupToDelete = SPickups.Find(Id);
            if (spickupToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(spickupToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Pickup spickupToDelete = SPickups.Find(Id);
            if (spickupToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SPickups.Delete(Id);
                SPickups.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}