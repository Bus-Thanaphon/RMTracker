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
    public class sLaminationController : Controller
    {
        IRepository<UserList> UserLists;
        IRepository<MachineList> MachineLists;
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
        IRepository<WorksPause> WorksPauses;
        IRepository<WorksDenine> WorksDenines;
        IRepository<ReasonDenine> ReasonDenines;
        IRepository<ReasonPause> ReasonPauses;
        // GET: sLamination

        public sLaminationController(IRepository<ReasonDenine> Reasondcontext, IRepository<ReasonPause> Reasonpcontext, IRepository<s_Lamination> laminationcontext, IRepository<Sub_C2B> SubC2Bcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<UserList> userlcon, IRepository<WorksPause> wpcontext, IRepository<MachineList> mccon, IRepository<WorksDenine> worksdeninecontext)
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
                case "1111":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Index()
        {
            List<s_Lamination> slamination = SLaminations.Collection().Where(o => o.C2BNo != null && o.Status_Lamination != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(slamination);
        }
        public ActionResult IndexSale()
        {
            List<s_Lamination> slamination = SLaminations.Collection().Where(o => o.C2BNo != null && o.Status_Lamination != "เสร็จ" && o.Status_Show == "1").OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.CreateAt).ToList();
            return View(slamination);
        }
        public ActionResult Start(string Id)
        {
            s_Lamination Laminationstart= SLaminations.Find(Id);
            if (Laminationstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                Laminationstart.User_Station = UserLists.Collection().Where(o => o.Department1 == "ผู้รับผิดชอบแผนกปิดผิว" || o.Department2 == "ผู้รับผิดชอบแผนกปิดผิว" || o.Department3 == "ผู้รับผิดชอบแผนกปิดผิว");
                Laminationstart.Machine_Station = MachineLists.Collection().Where(o => o.Department == "แผนกปิดผิว");
                return View(Laminationstart);
            }
        }
        [HttpPost]
        public ActionResult Start(string Id, s_Lamination Laminations)
        {
            s_Lamination Laminationstart = SLaminations.Find(Id);
            Sub_C2B FindDenine = Subc2bs.Find(Id);

            if (Laminationstart == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(Laminationstart);
                }
                FindDenine.Status_AStation = "กำลังผลิต";
                Subc2bs.Commit();
                Laminationstart.User = Laminations.User;
                Laminationstart.Comment = Laminations.Comment;
                Laminationstart.Machine_Lamination = Laminations.Machine_Lamination;
                Laminationstart.Status_Lamination = "กำลังผลิต";
                Laminationstart.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                SLaminations.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Denine(string Id)
        {
            s_Lamination LamiDenine = SLaminations.Find(Id);
            if (LamiDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SLaminations = new s_Lamination();
                viewModel.WorksDenine = new WorksDenine();
                viewModel.WorksDenine.Reason_List = ReasonDenines.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดผิว");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Denine(string Id, string lamis, WorksDenine wdc, DenineView dnw)
        {
            s_Lamination LamiDenine = SLaminations.Find(Id);
            lamis = LamiDenine.Id_lamination;
            Sub_C2B FindDenine = Subc2bs.Find(lamis);
            if (LamiDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Previousstatus = FindDenine.Status_AStation;
                FindDenine.Status_AStation = "ปฏิเสธ";
                Subc2bs.Commit();
                LamiDenine.Status_Lamination = "ปฏิเสธ";
                LamiDenine.DenineID = Guid.NewGuid().ToString();
                SLaminations.Commit();
                WorksDenines.Insert(wdc);
                wdc.Id = LamiDenine.DenineID;
                wdc.StationID = LamiDenine.Id;
                wdc.SONO = LamiDenine.SubC2B;
                wdc.Reason = dnw.WorksDenine.Reason;
                wdc.Other_Reason = dnw.WorksDenine.Other_Reason;
                wdc.Station = "sLamination";
                wdc.StartDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult NewWork(string Id, string wpc)
        {
            s_Lamination LamiNew = SLaminations.Find(Id);
            wpc = LamiNew.DenineID;
            WorksDenine wp = WorksDenines.Find(wpc);
            Sub_C2B FindDenine = Subc2bs.Find(Id);
            if (LamiNew == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindDenine.Status_AStation = FindDenine.Previousstatus;
                Subc2bs.Commit();
                wp.EndDate = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksDenines.Commit();
                LamiNew.Status_Lamination = "อยู่ในคิว";
                SLaminations.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Pause(string Id)
        {
            s_Lamination LamiDenine = SLaminations.Find(Id);
            if (LamiDenine == null)
            {
                return HttpNotFound();
            }
            else
            {
                DenineView viewModel = new DenineView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.SLaminations = new s_Lamination();
                viewModel.WorksPause = new WorksPause();
                viewModel.WorksPause.Reason_List = ReasonPauses.Collection().OrderBy(o => o.No).Where(o => o.Station == "ปิดผิว");
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Pause(string Id, WorksPause wpc, DenineView Pw)
        {
            s_Lamination LaminationPause = SLaminations.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (LaminationPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Previousstatus = FindPause.Status_AStation;
                FindPause.Status_AStation = "หยุดชั่วคราว";
                Subc2bs.Commit();
                LaminationPause.Status_Lamination = "หยุดชั่วคราว";
                LaminationPause.Time_pauses += 1;
                LaminationPause.PauseID = Guid.NewGuid().ToString();
                SLaminations.Commit();
                WorksPauses.Insert(wpc);
                wpc.Id = LaminationPause.PauseID;
                wpc.StationID = LaminationPause.Id;
                wpc.Reason = Pw.WorksPause.Reason;
                wpc.Other_Reason = Pw.WorksPause.Other_Reason;
                wpc.Start_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                wpc.SONO = LaminationPause.SubC2B;
                wpc.Station = "sLamination";
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnPause(string Id, string wpc)
        {
            s_Lamination LaminationPause = SLaminations.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            wpc = LaminationPause.PauseID;
            WorksPause unpause = WorksPauses.Find(wpc);
            if (LaminationPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                LaminationPause.Status_Lamination = "กำลังผลิต";
                SLaminations.Commit();
                unpause.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                WorksPauses.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Back(string Id)
        {
            s_Lamination LaminationPause = SLaminations.Find(Id);
            Sub_C2B FindPause = Subc2bs.Find(Id);
            if (LaminationPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                FindPause.Status_AStation = FindPause.Previousstatus;
                Subc2bs.Commit();
                LaminationPause.Status_Lamination = "กำลังผลิต";
                SLaminations.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Finish(string Id)
        {
            s_Lamination laminationAccept = SLaminations.Find(Id);
            if (laminationAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(laminationAccept);
            }
        }
        [HttpPost]
        public ActionResult Finish(s_Lamination lamination, string Id,string idupdate,string cutu,string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            s_Lamination laminationAccept = SLaminations.Find(Id);
            idupdate = laminationAccept.Id_lamination;
            Sub_C2B statuschange = Subc2bs.Find(idupdate);
            cutu = statuschange.OrderID_Cut;
            s_Cut cutAccept = SCuts.Find(cutu);
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
            string wait2 = "รอตัด";
            string wait3 = "รอปิดขอบ";
            string wait4 = "รอเจาะ";
            string wait5 = "รอทำสี";
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (laminationAccept == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(lamination);
                }
                laminationAccept.End_Time = DateTime.Now.ToString("dd/MM/yyyy-HH:mm");
                laminationAccept.Status_Lamination = finish;
                statuschange.Status_AStation = "กำลังผลิต";
                if (cutAccept.Status_Cut != null)
                {
                    if (laminationAccept.Status_Lamination != null)
                    {
                        cutAccept.Status_Cut = inqueue;
                    }
                }

                if (edgeAccept.Status_Edgebanding != null)
                {
                    if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        edgeAccept.Status_Edgebanding = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
                    {
                        edgeAccept.Status_Edgebanding = inqueue;
                    }
                }
                if(drillAccept.Status_Drill != null)
                {
                    if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        drillAccept.Status_Drill = wait3;
                    }
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        drillAccept.Status_Drill = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
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
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        paintAccept.Status_Painting = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
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
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait3;
                    }
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        cleanAccept.Status_Cleaning = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
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
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait3;
                    }
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        packAccept.Status_Packing = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
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
                    else if(cleanAccept.Status_Cleaning != null && cleanAccept.Status_Cleaning == "อยู่ในคิว")
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
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait3;
                    }
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        qcAccept.Status_QC = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
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
                    else if(packAccept.Status_Packing != null && packAccept.Status_Packing == "อยู่ในคิว")
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
                    else if (edgeAccept.Status_Edgebanding != null && edgeAccept.Status_Edgebanding == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait3;
                    }
                    else if (cutAccept.Status_Cut != null && cutAccept.Status_Cut == "อยู่ในคิว")
                    {
                        pickAccept.Status_Pickup = wait2;
                    }
                    else if (laminationAccept.Status_Lamination != null)
                    {
                        statuschange.Status_AStation = "พร้อมส่ง";
                        pickAccept.Status_Pickup = inqueue;
                    }
                }
                Subc2bs.Commit();
                SLaminations.Commit();
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
            s_Lamination Laminationadd = SLaminations.Find(Id);
            if (Laminationadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                Laminationadd.User_Station = UserLists.Collection().Where(o => o.Department1=="เจ้าหน้าที่"|| o.Department2 == "เจ้าหน้าที่" || o.Department3 == "เจ้าหน้าที่");
                return View(Laminationadd);
            }
        }
        [HttpPost]
        public ActionResult AddData(string Id,s_Lamination Laminations)
        {
            s_Lamination Laminationadd = SLaminations.Find(Id);

            if (Laminationadd == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(Laminationadd);
                }
                Laminationadd.Comment = Laminations.Comment;
                Laminationadd.Machine_Lamination = Laminations.Machine_Lamination;
                SLaminations.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            s_Lamination slaminationToDelete = SLaminations.Find(Id);
            if (slaminationToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(slaminationToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id,string Id_lamination)
        {
            s_Lamination slaminationToDelete = SLaminations.Find(Id);
            if (slaminationToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SLaminations.Delete(Id);
                SLaminations.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}