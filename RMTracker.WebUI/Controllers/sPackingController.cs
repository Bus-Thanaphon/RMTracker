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
    public class sPackingController : Controller
    {
        // GET: sPacking
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

        public sPackingController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
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
                case "7777":
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
            model.VPackings = SPackings.Collection().Where(o => o.C2BNo != null && o.Status_Packing != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            model.SPackings = SPackings.Find(Id);
            model.WorksPause = new WorksPause();
            model.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "แพ็ค");
            model.WorksDenine = new WorksDenine();
            model.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "แพ็ค");
            return View(model);
        }
        public ActionResult IndexSale(string Id)
        {
            if (Id != null)
            {
                List<s_Packing> sapacking = SPackings.Collection().Where(o => o.Id == Id).ToList();
                return View(sapacking);
            }
            List<s_Packing> spacking = SPackings.Collection().Where(o => o.C2BNo != null && o.Status_Packing != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(spacking);
        }
        public ActionResult Start(string Id)
        {
            s_Packing packstart = SPackings.Find(Id);
            if (packstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                packstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกแพ็ค" || o.Department2 == "ผู้รับผิดชอบแผนกแพ็ค" || o.Department3 == "ผู้รับผิดชอบแผนกแพ็ค");
                return View(packstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Packing packs)
        {
            s_Packing packstart = SPackings.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (packstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(packstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                packstart.User = packs.User;
                packstart.Comment = packs.Comment;
                packstart.Machine_Pack = "แพ็ค";
                packstart.Status_Packing = "กำลังผลิต";
                packstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SPackings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Pause(string Id)
        //{
        //    s_Packing Pause = SPackings.Find(Id);
        //    if (Pause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SPackings = new s_Packing();
        //        viewModel.WorksPause = new WorksPause();
        //        viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "แพ็ค");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Packing PackPause = SPackings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PackPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                PackPause.Status_Packing = "หยุดชั่วคราว";
                PackPause.Time_pauses += 1;
                PackPause.PauseID = Guid.NewGuid().ToString();
                SPackings.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = PackPause.PauseID;
                wpc.StationID = PackPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = PackPause.SubC2B;
                wpc.Station = "sPacking";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Packing PackPause = SPackings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = PackPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (PackPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PackPause.Status_Packing = "กำลังผลิต";
                SPackings.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Packing PackBack = SPackings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PackBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PackBack.Status_Packing = "กำลังผลิต";
                SPackings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Denine(string Id)
        //{
        //    s_Packing Denine = SPackings.Find(Id);
        //    if (Denine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SPackings = new s_Packing();
        //        viewModel.WorksDenine = new WorksDenine();
        //        viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "แพ็ค");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Packing Denine = SPackings.Find(Id);
            lamis = Denine.Id_Packing;
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
                Denine.Status_Packing = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SPackings.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sPacking";
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
            s_Packing PackNew = SPackings.Find(Id);
            wpc = PackNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (PackNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                PackNew.Status_Packing = "อยู่ในคิว";
                SPackings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Finish(string Id)
        //{
        //    s_Packing packAccept = SPackings.Find(Id);
        //    if (packAccept == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(packAccept);
        //    }
        //}
        //[HttpPost]
        public ActionResult Finish(s_Painting painting, string Id, string idupdate, string cleanu, string packu, string qcu, string picku)
        {
            s_Packing packAccept = SPackings.Find(Id);
            idupdate = packAccept.Id_Packing;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            qcu = statuschange.OrderID_QC;
            s_QC qcAccept = SQCs.Find(qcu);
            picku = statuschange.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);
            string finish = "เสร็จ";
            string inqueue = "อยู่ในคิว";
            string wait8 = "รอQC";

            if (packAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(packAccept);
                }
                packAccept.Status_Packing = finish;
                packAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (qcAccept.Status_QC != null)
                {
                    if (packAccept.Status_Packing != null)
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
                    else if (packAccept.Status_Packing != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult AddData(string Id)
        {
            s_Packing packadd = SPackings.Find(Id);
            if (packadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(packadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Packing Packs)
        {
            s_Packing packadd = SPackings.Find(Id);

            if (packadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(packadd);
                }
                packadd.Comment = Packs.Comment;
                SPackings.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            s_Packing spackingToDelete = SPackings.Find(Id);
            if (spackingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(spackingToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Packing spackingToDelete = SPackings.Find(Id);
            if (spackingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SPackings.Delete(Id);
                SPackings.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}