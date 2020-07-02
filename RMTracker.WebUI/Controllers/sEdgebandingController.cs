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
    public class sEdgebandingController : Controller
    {
        // GET: sEdgebanding
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

        public sEdgebandingController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, IRepository<Sub_C2B> SubC2Bcontext,
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
                case "3333":
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
            model.VEdgebandings = SEdgebandings.Collection().Where(o => o.C2BNo != null && o.Status_Edgebanding != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            model.SEdgebandings = SEdgebandings.Find(Id);
            model.WorksPause = new WorksPause();
            model.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดขอบ");
            model.WorksDenine = new WorksDenine();
            model.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดขอบ");
            return View(model);
        }
        public ActionResult IndexSale(string Id)
        {
            if (Id != null)
            {
                List<s_Edgebanding> saedgebanding = SEdgebandings.Collection().Where(o => o.Id == Id).ToList();
                return View(saedgebanding);
            }
            List<s_Edgebanding> sedgebanding = SEdgebandings.Collection().Where(o => o.C2BNo != null && o.Status_Edgebanding != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(sedgebanding);
        }

        public ActionResult Start(string Id)
        {
            s_Edgebanding edgestart = SEdgebandings.Find(Id);
            if (edgestart == null)
            {
                return HttpNotFound();
            }
            else
            {
                edgestart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกปิดขอบ" || o.Department2 == "ผู้รับผิดชอบแผนกปิดขอบ" || o.Department3 == "ผู้รับผิดชอบแผนกปิดขอบ");
                edgestart.Machine_Station = MachineLists.Collection().Where(o => o.Department == "แผนกปิดขอบ");
                return View(edgestart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Edgebanding edges)
        {
            s_Edgebanding edgestart = SEdgebandings.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (edgestart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(edgestart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                edgestart.User = edges.User;
                edgestart.Machine_Edgebanding = edges.Machine_Edgebanding;
                edgestart.Comment = edges.Comment;
                edgestart.Status_Edgebanding = "กำลังผลิต";
                edgestart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SEdgebandings.Commit();

                return RedirectToAction("Index");
            }
        }
        //public ActionResult Pause(string Id)
        //{
        //    s_Edgebanding Pause = SEdgebandings.Find(Id);
        //    if (Pause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SEdgebandings = new s_Edgebanding();
        //        viewModel.WorksPause = new WorksPause();
        //        viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดขอบ");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Edgebanding EdgePause = SEdgebandings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (EdgePause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                EdgePause.Status_Edgebanding = "หยุดชั่วคราว";
                EdgePause.Time_pauses += 1;
                EdgePause.PauseID = Guid.NewGuid().ToString();
                SEdgebandings.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = EdgePause.PauseID;
                wpc.StationID = EdgePause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = EdgePause.SubC2B;
                wpc.Station = "sEdgebanding";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Edgebanding EdgePause = SEdgebandings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = EdgePause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (EdgePause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                EdgePause.Status_Edgebanding = "กำลังผลิต";
                SEdgebandings.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Edgebanding EdgePause = SEdgebandings.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (EdgePause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                EdgePause.Status_Edgebanding = "กำลังผลิต";
                SCuts.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Nodata(string Id)
        {
            return View();
        }
        //public ActionResult Denine(string Id)
        //{
        //    s_Edgebanding EdgeDenine = SEdgebandings.Find(Id);
        //    if (EdgeDenine == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        DenineView viewModel = new DenineView();

        //        viewModel.Subc2bs = new Sub_C2B();
        //        viewModel.SEdgebandings = new s_Edgebanding();
        //        viewModel.WorksDenine = new WorksDenine();
        //        viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดขอบ");
        //        return View(viewModel);
        //    }
        //}
        //[HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Edgebanding EdgeDenine = SEdgebandings.Find(Id);
            lamis = EdgeDenine.Id_Edgebanding;
            Sub_C2B FindDenine = Subc2bs.Find(lamis);
            if (EdgeDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Previousstatus = FindDenine.Status_AStation;
                FindDenine.Status_AStation = "ปฏิเสธ";
                Subc2bs.Commit();
                EdgeDenine.Status_Edgebanding = "ปฏิเสธ";
                EdgeDenine.DenineID = Guid.NewGuid().ToString();
                SEdgebandings.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = EdgeDenine.DenineID;
                wdc.StationID = EdgeDenine.Id;
                wdc.SONO = EdgeDenine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sEdgebanding";
                wdc.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult NewWork(string Id, string wpc)
        {
            s_Edgebanding EdgeNew = SEdgebandings.Find(Id);
            wpc = EdgeNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (EdgeNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                EdgeNew.Status_Edgebanding = "อยู่ในคิว";
                SEdgebandings.Commit();
                return RedirectToAction("Index");
            }
        }
        //public ActionResult Finish(string Id)
        //{
        //    s_Edgebanding edgebandingAccept = SEdgebandings.Find(Id);
        //    if (edgebandingAccept == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(edgebandingAccept);
        //    }
        //}
        //[HttpPost]
        public ActionResult Finish(s_Edgebanding edgebanding, string Id, string idupdate, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            s_Edgebanding edgeAccept = SEdgebandings.Find(Id);
            idupdate = edgeAccept.Id_Edgebanding;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
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
            string wait4 = "รอเจาะ";
            string wait5 = "รอทำสี";
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (edgeAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(edgebanding);
                }
                edgeAccept.Status_Edgebanding = finish;
                edgeAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                statuschange.Status_AStation = "กำลังผลิต";
                if (drillAccept.Status_Drill != null)
                {
                    if (edgeAccept.Status_Edgebanding != null)
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
                    else if (edgeAccept.Status_Edgebanding != null)
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
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null)
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
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null)
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
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null)
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
                    else if (drillAccept.Status_Drill != null && drillAccept.Status_Drill == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait4;
                    }
                    else if (edgeAccept.Status_Edgebanding != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
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
            s_Edgebanding edgeadd = SEdgebandings.Find(Id);
            if (edgeadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(edgeadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id, s_Edgebanding Edges)
        {
            s_Edgebanding edgeadd = SEdgebandings.Find(Id);

            if (edgeadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(edgeadd);
                }
                edgeadd.Comment = Edges.Comment;
                edgeadd.Machine_Edgebanding = Edges.Machine_Edgebanding;
                SEdgebandings.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            s_Edgebanding sedgebandingToDelete = SEdgebandings.Find(Id);
            if (sedgebandingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(sedgebandingToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            s_Edgebanding sedgebandingToDelete = SEdgebandings.Find(Id);
            if (sedgebandingToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SEdgebandings.Delete(Id);
                SEdgebandings.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}