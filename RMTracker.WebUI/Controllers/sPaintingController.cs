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
    public class sPaintingController : Controller
    {
        // GET: sPainting
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

        public sPaintingController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext,IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
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
                case "5555":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Index()
        {
            List<s_Painting> spainting = SPaintings.Collection().Where(o => o.C2BNo != null && o.Status_Painting != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(spainting);
        }
        public ActionResult IndexSale()
        {
            List<s_Painting> spainting = SPaintings.Collection().Where(o => o.C2BNo != null && o.Status_Painting != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(spainting);
        }
        public ActionResult Start(string Id)
        {
            s_Painting paintstart = SPaintings.Find(Id);
            if (paintstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                paintstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกทำสี" || o.Department2 == "ผู้รับผิดชอบแผนกทำสี" || o.Department3 == "ผู้รับผิดชอบแผนกทำสี");
                return View(paintstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Painting paints)
        {
            s_Painting paintstart = SPaintings.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (paintstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(paintstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                paintstart.User = paints.User;
                paintstart.Comment = paints.Comment;
                paintstart.Status_Painting = "กำลังผลิต";
                paintstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SPaintings.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Pause(string Id)
        {
            s_Painting LamiDenine = SPaintings.Find(Id);
            if (LamiDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SPaintings = new s_Painting();
                viewModel.WorksPause = new WorksPause();
                viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำสี");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Painting PaintPause = SPaintings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PaintPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                PaintPause.Status_Painting = "หยุดชั่วคราว";
                PaintPause.Time_pauses += 1;
                PaintPause.PauseID = Guid.NewGuid().ToString();
                SPaintings.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = PaintPause.PauseID;
                wpc.StationID = PaintPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = PaintPause.SubC2B;
                wpc.Station = "sPainting";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Painting PaintPause = SPaintings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = PaintPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (PaintPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PaintPause.Status_Painting = "กำลังผลิต";
                SPaintings.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Painting PaintBack = SPaintings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (PaintBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                PaintBack.Status_Painting = "กำลังผลิต";
                SPaintings.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Denine(string Id)
        {
            s_Painting Denine = SPaintings.Find(Id);
            if (Denine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SCuts = new s_Cut();
                viewModel.WorksDenine = new WorksDenine();
                viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ทำสี");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Painting Denine = SPaintings.Find(Id);
            lamis = Denine.Id_Painting;
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
                Denine.Status_Painting = "ปฏิเสธ";
                Denine.DenineID = Guid.NewGuid().ToString();
                SPaintings.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = Denine.DenineID;
                wdc.StationID = Denine.Id;
                wdc.SONO = Denine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sPainting";
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
            s_Painting New = SPaintings.Find(Id);
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
                New.Status_Painting = "อยู่ในคิว";
                SPaintings.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Finish(string Id)
        {
            s_Painting paintingAccept = SPaintings.Find(Id);
            if (paintingAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(paintingAccept);
            }
        }
        [HttpPost]
        public ActionResult Finish(s_Painting painting, string Id, string idupdate, string cleanu, string packu, string qcu, string picku)
        {
            s_Painting paintingAccept = SPaintings.Find(Id);
            idupdate = paintingAccept.Id_Painting;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
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
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (paintingAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(paintingAccept);
                }
                paintingAccept.Status_Painting = finish;
                paintingAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (cleanAccept.Status_Cleaning != null)
                {
                    if (paintingAccept.Status_Painting != null)
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
                    else if (paintingAccept.Status_Painting != null)
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
                    else if (paintingAccept.Status_Painting != null)
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
                    else if (paintingAccept.Status_Painting != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                
                Subc2bs.Commit();
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
            s_Painting paintadd = SPaintings.Find(Id);
            if (paintadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(paintadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Painting Paints)
        {
            s_Painting paintadd = SPaintings.Find(Id);

            if (paintadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(paintadd);
                }
                paintadd.Comment = Paints.Comment;
                SPaintings.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            s_Painting spaintingToDelete = SPaintings.Find(Id);
            if (spaintingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(spaintingToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Painting spaintingToDelete = SPaintings.Find(Id);
            if (spaintingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SPaintings.Delete(Id);
                SPaintings.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}