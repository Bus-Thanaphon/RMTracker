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
    public class sCutController : Controller
    {
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

        public sCutController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<Sub_C2B> SubC2Bcontext, IRepository<s_Edgebanding> edgebandingcontext,
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
        // GET: sCut
        public ActionResult CheckPassword(string Password)
        {
            switch (Password)
            {
                case "2222":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Index()
        {
            List<s_Cut> scut = SCuts.Collection().Where(o => o.C2BNo != null && o.Status_Cut != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(scut);
        }
        public ActionResult IndexSale()
        {
            List<s_Cut> scut = SCuts.Collection().Where(o => o.C2BNo != null && o.Status_Cut != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(scut);
        }

        public ActionResult Start(string Id)
        {
            s_Cut cutstart = SCuts.Find(Id);
            if (cutstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                cutstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกตัด" || o.Department2 == "ผู้รับผิดชอบแผนกตัด" || o.Department3 == "ผู้รับผิดชอบแผนกตัด");
                return View(cutstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Cut Cuts)
        {
            s_Cut cutstart = SCuts.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (cutstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cutstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                cutstart.User = Cuts.User;
                cutstart.Comment = Cuts.Comment;
                cutstart.Status_Cut = "กำลังผลิต";
                cutstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SCuts.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Pause(string Id)
        {
            s_Cut Pause = SCuts.Find(Id);
            if (Pause == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SCuts = new s_Cut();
                viewModel.WorksPause = new WorksPause();
                viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ตัด");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Cut CutPause = SCuts.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (CutPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                CutPause.Status_Cut = "หยุดชั่วคราว";
                CutPause.Time_pauses += 1;
                CutPause.PauseID = Guid.NewGuid().ToString();
                SCuts.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = CutPause.PauseID;
                wpc.StationID = CutPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = CutPause.SubC2B;
                wpc.Station = "sCut";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Cut CutPause = SCuts.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = CutPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (CutPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                CutPause.Status_Cut = "กำลังผลิต";
                SCuts.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Back(string Id)
        {
            s_Cut CutPause = SCuts.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (CutPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                CutPause.Status_Cut = "กำลังผลิต";
                SCuts.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Denine(string Id)
        {
            s_Cut CutDenine = SCuts.Find(Id);
            if (CutDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SCuts = new s_Cut();
                viewModel.WorksDenine = new WorksDenine();
                viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ตัด");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Cut CutDenine = SCuts.Find(Id);
            lamis = CutDenine.Id_Cut;
            Sub_C2B FindDenine = Subc2bs.Find(lamis);
            if (CutDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Previousstatus = FindDenine.Status_AStation;
                FindDenine.Status_AStation = "ปฏิเสธ";
                Subc2bs.Commit();
                CutDenine.Status_Cut = "ปฏิเสธ";
                CutDenine.DenineID = Guid.NewGuid().ToString();
                SCuts.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = CutDenine.DenineID;
                wdc.StationID = CutDenine.Id;
                wdc.SONO = CutDenine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sCut";
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
            s_Cut CutNew = SCuts.Find(Id);
            wpc = CutNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (CutNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                CutNew.Status_Cut = "อยู่ในคิว";
                SCuts.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Finish(string Id)
        {
            s_Cut cutAccept = SCuts.Find(Id);
            if (cutAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cutAccept);
            }
        }
        [HttpPost]
        public ActionResult Finish(s_Cut cut, string Id, string idupdate, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            s_Cut cutAccept = SCuts.Find(Id);
            idupdate = cutAccept.Id_Cut;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            edgeu = statuschange.OrderID_EdgeBanding;
            s_Edgebanding edgeAccept = SEdgebandings.Find(edgeu);
            drillu = statuschange.OrderID_Drill;
            s_Drill drillAccept = SDrills.Find(drillu);
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
            string wait3 = "รอปิดขอบ";
            string wait4 = "รอเจาะ";
            string wait5 = "รอทำสี";
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (cutAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cut);
                }
                cutAccept.Status_Cut = finish;
                cutAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (edgeAccept.Status_Edgebanding != null)
                {
                    if (cutAccept.Status_Cut != null)
                    {
                        edgeAccept.Status_Edgebanding = inqueue;
                    }
                }
                if (drillAccept.Status_Drill != null)
                {
                    if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        drillAccept.Status_Drill = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        drillAccept.Status_Drill = inqueue;
                    }
                }

                if (paintAccept.Status_Painting != null)
                {
                    if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        paintAccept.Status_Painting = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        paintAccept.Status_Painting = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        paintAccept.Status_Painting = inqueue;
                    }
                }
                if (cleanAccept.Status_Cleaning != null)
                {
                    if (paintAccept.Status_Painting != null)
                    {
                        cleanAccept.Status_Cleaning = wait5;
                    }
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        cleanAccept.Status_Cleaning = inqueue;
                    }
                }
                if (packAccept.Status_Packing != null)
                {
                    if (cleanAccept.Status_Cleaning != null)
                    {
                        packAccept.Status_Packing = wait6;
                    }
                    else if (paintAccept.Status_Painting != null)
                    {
                        packAccept.Status_Packing = wait5;
                    }
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        packAccept.Status_Packing = inqueue;
                    }
                }
                if (qcAccept.Status_QC != null)
                {
                    if (packAccept.Status_Packing != null)
                    {
                        qcAccept.Status_QC = wait7;
                    }
                    else if (cleanAccept.Status_Cleaning != null)
                    {
                        qcAccept.Status_QC = wait6;
                    }
                    else if (paintAccept.Status_Painting != null)
                    {
                        qcAccept.Status_QC = wait5;
                    }
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        qcAccept.Status_QC = inqueue;
                    }
                }
                if (pickAccept.Status_Pickup != null)
                {
                    if (qcAccept.Status_QC != null)
                    {
                        pickAccept.Status_Pickup = wait8;
                    }
                    else if (packAccept.Status_Packing != null)
                    {
                        pickAccept.Status_Pickup = wait7;
                    }
                    else if (cleanAccept.Status_Cleaning != null)
                    {
                        pickAccept.Status_Pickup = wait6;
                    }
                    else if (paintAccept.Status_Painting != null)
                    {
                        pickAccept.Status_Pickup = wait5;
                    }
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait3;
                    }
                    else if (cutAccept.Status_Cut != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
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
            s_Cut cutadd = SCuts.Find(Id);
            if (cutadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(cutadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Cut Cuts)
        {
            s_Cut cutadd = SCuts.Find(Id);

            if (cutadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(cutadd);
                }
                cutadd.Comment = Cuts.Comment;
                cutadd.Machine_Cut = Cuts.Machine_Cut;
                SCuts.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            s_Cut scutToDelete = SCuts.Find(Id);
            if (scutToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(scutToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Cut scutToDelete = SCuts.Find(Id);
            if (scutToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SCuts.Delete(Id);
                SCuts.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}