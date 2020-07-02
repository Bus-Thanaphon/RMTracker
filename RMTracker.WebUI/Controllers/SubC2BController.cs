using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using RMTracker.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class SubC2BController : Controller
    {
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
        IRepository<SO_PAUSE> SOPAUSEs;

        public SubC2BController(IRepository<User_Works> userwcontext,IRepository<Sub_C2B> SubC2BContext, 
            IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext, 
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<SO_PAUSE> sopausecon)
        {
            Userworks = userwcontext;
            Subc2bs = SubC2BContext;
            SLaminations = laminationcontext;
            SCuts = cutcontext;
            SEdgebandings = edgebandingcontext;
            SDrills = drillcontext;
            SPaintings = paintingcontext;
            SCleanings = cleaningcontext;
            SPackings = packingcontext;
            SQCs = qccontext;
            SPickups = pickupcontext;
            SOPAUSEs = sopausecon;
        }
        // GET: UserWorks
        public ActionResult Index(string Id)
        {
            var model = new IndexView();
            model.Userwork = Userworks.Find(Id);
            model.subc2b = new Sub_C2B();
            model.C2BView = Subc2bs.Collection().Where(o => o.SubID == Id).OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.SubC2B).ToList();

            List<Sub_C2B> subc2b = Subc2bs.Collection().Where(o => o.SubID == Id).OrderByDescending(o => o.Urgent_Status).ThenBy(x => x.SubC2B).ToList();
            var SubId_lamination = model.C2BView.Select(o => o.OrderID_Lamination).ToList();
            var SubId_cut = model.C2BView.Select(o => o.OrderID_Cut).ToList();
            var SubId_edge = model.C2BView.Select(o => o.OrderID_EdgeBanding).ToList();
            var SubId_drill = model.C2BView.Select(o => o.OrderID_Drill).ToList();
            var SubId_paint = model.C2BView.Select(o => o.OrderID_Painting).ToList();
            var SubId_clean = model.C2BView.Select(o => o.OrderID_Cleaning).ToList();
            var SubId_pack = model.C2BView.Select(o => o.OrderID_Packing).ToList();
            var SubId_qc = model.C2BView.Select(o => o.OrderID_QC).ToList();
            var SubId_pickup = model.C2BView.Select(o => o.OrderID_Pickup).ToList();

            var laminationId = SLaminations.Collection().Where(o => SubId_lamination.Contains(o.Id)).ToList();
            var cutId = SCuts.Collection().Where(o => SubId_cut.Contains(o.Id)).ToList();
            var edgeId = SEdgebandings.Collection().Where(o => SubId_edge.Contains(o.Id)).ToList();
            var drillId = SDrills.Collection().Where(o => SubId_drill.Contains(o.Id)).ToList();
            var paintId = SPaintings.Collection().Where(o => SubId_paint.Contains(o.Id)).ToList();
            var cleanId = SCleanings.Collection().Where(o => SubId_clean.Contains(o.Id)).ToList();
            var packId = SPackings.Collection().Where(o => SubId_pack.Contains(o.Id)).ToList();
            var qcId = SQCs.Collection().Where(o => SubId_qc.Contains(o.Id)).ToList();
            var pickId = SPickups.Collection().Where(o => SubId_pickup.Contains(o.Id)).ToList();

            foreach (var i in model.C2BView)
            {
                i.Status_Lamination = laminationId.Where(o => o.Id == i.OrderID_Lamination).FirstOrDefault();
                i.Status_Cut = cutId.Where(o => o.Id == i.OrderID_Cut).FirstOrDefault();
                i.Status_EdgeBanding = edgeId.Where(o => o.Id == i.OrderID_EdgeBanding).FirstOrDefault();
                i.Status_Drill = drillId.Where(o => o.Id == i.OrderID_Drill).FirstOrDefault();
                i.Status_Painting = paintId.Where(o => o.Id == i.OrderID_Painting).FirstOrDefault();
                i.Status_Cleaning = cleanId.Where(o => o.Id == i.OrderID_Cleaning).FirstOrDefault();
                i.Status_Packing = packId.Where(o => o.Id == i.OrderID_Packing).FirstOrDefault();
                i.Status_QC = qcId.Where(o => o.Id == i.OrderID_QC).FirstOrDefault();
                i.Status_Pickup = pickId.Where(o => o.Id == i.OrderID_Pickup).FirstOrDefault();
            }

            return View(model);
        }

        public ActionResult ShowWork(Sub_C2B subc2bc, string Id,string lamis,string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bShow = Subc2bs.Find(Id);
            lamis = subc2bShow.OrderID_Lamination;
            s_Lamination LamiAccept = SLaminations.Find(lamis);
            cutu = subc2bShow.OrderID_Cut;
            s_Cut cutAccept = SCuts.Find(cutu);
            edgeu = subc2bShow.OrderID_EdgeBanding;
            s_Edgebanding edgeAccept = SEdgebandings.Find(edgeu);
            drillu = subc2bShow.OrderID_Drill;
            s_Drill drillAccept = SDrills.Find(drillu);
            paintu = subc2bShow.OrderID_Painting;
            s_Painting paintAccept = SPaintings.Find(paintu);
            cleanu = subc2bShow.OrderID_Cleaning;
            s_Cleaning cleanAccept = SCleanings.Find(cleanu);
            packu = subc2bShow.OrderID_Packing;
            s_Packing packAccept = SPackings.Find(packu);
            qcu = subc2bShow.OrderID_QC;
            s_QC qcAccept = SQCs.Find(qcu);
            picku = subc2bShow.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);
            if (subc2bShow == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (subc2bShow.CountSend == 0 && LamiAccept.Status_Show == "0") 
                {
                    subc2bShow.StartDate = DateTime.Now.ToString();
                    subc2bShow.CountSend += 1;
                }
                else if(subc2bShow.CountSend != 0 && LamiAccept.Status_Show == "0")
                {
                    subc2bShow.CountSend += 1;
                }
                if (LamiAccept.Status_Show == "0")
                {
                    LamiAccept.Status_Show = "1";
                    cutAccept.Status_Show = "1";
                    edgeAccept.Status_Show = "1";
                    drillAccept.Status_Show = "1";
                    paintAccept.Status_Show = "1";
                    cleanAccept.Status_Show = "1";
                    packAccept.Status_Show = "1";
                    qcAccept.Status_Show = "1";
                    pickAccept.Status_Show = "1";
                }
                else
                {
                    LamiAccept.Status_Show = "0";
                    cutAccept.Status_Show = "0";
                    edgeAccept.Status_Show = "0";
                    drillAccept.Status_Show = "0";
                    paintAccept.Status_Show = "0";
                    cleanAccept.Status_Show = "0";
                    packAccept.Status_Show = "0";
                    qcAccept.Status_Show = "0";
                    pickAccept.Status_Show = "0";
                }
                if (subc2bShow.Status_AStation != "หยุด" && subc2bShow.Status_AStation != "หยุดชั่วคราว" && subc2bShow.Status_AStation != "ปฏิเสธ" && subc2bShow.Status_AStation != "พร้อมส่ง" && subc2bShow.Status_AStation != "เสร็จงาน")
                {
                    subc2bShow.Status_AStation = "อยู่ในคิว";
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
                return RedirectToAction("Index", new { id = subc2bShow.SubID });
            }
        }

        public ActionResult Create(Sub_C2B subc2bc,string Id)
        {
            User_Works userwToEdit = Userworks.Find(Id);
            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                RMC2BView viewModel = new RMC2BView();

                viewModel.Subc2bs = new Sub_C2B();
                viewModel.Userwork = userwToEdit;
                viewModel.SLaminations = new s_Lamination();
                viewModel.SCuts = new s_Cut();
                viewModel.SEdgebandings = new s_Edgebanding();
                viewModel.SDrills = new s_Drill();
                viewModel.SPaintings = new s_Painting();
                viewModel.SCleanings = new s_Cleaning();
                viewModel.SPackings = new s_Packing();
                viewModel.SQCs = new s_QC();
                viewModel.SPickups = new s_Pickup();
                viewModel.Userworks = Userworks.Collection();
                return View(viewModel);
            }
            //Sub_C2B subC2B = new Sub_C2B();
            //return View(subC2B);
        }
        [HttpPost]
        public ActionResult Create(Sub_C2B subc2bs, User_Works userw, string Id,s_Lamination slaminations, s_Cut scuts,
            s_Edgebanding sedgebandings,s_Drill sdrills, s_Painting spaintings, s_Cleaning scleanings, s_Packing spackings, s_QC sqcs,s_Pickup spickups)
        {
            User_Works userwToEdit = Userworks.Find(Id);
            string wait1 = "รอปิดผิว";
            string wait2 = "รอตัด";
            string wait3 = "รอปิดขอบ";
            string wait4 = "รอเจาะ";
            string wait5 = "รอทำสี";
            string wait6 = "รอทำความสะอาด";
            string wait7 = "รอแพ็ค";
            string wait8 = "รอQC";

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                //if (!ModelState.IsValid)
                //{
                //    return View(subc2bs);
                //}
                //else
                //{
                    userwToEdit.Order_Status = "อยู่ในคิว";
                    Subc2bs.Insert(subc2bs);
                    subc2bs.C2BNo = userwToEdit.C2BNo;
                    subc2bs.SubC2B = $"{userwToEdit.C2BNo}-{subc2bs.SubC2B}/{subc2bs.SubC2B2}";
                    subc2bs.SubID = userwToEdit.Id;
                    subc2bs.Status_AStation = "งานใหม่";
                    subc2bs.Check_PauseID = "0";
                    subc2bs.Urgent_Status = "0";
                    subc2bs.StartDateDay = subc2bs.CreateAt.Day;
                    subc2bs.StartDateMonth = subc2bs.CreateAt.Month;
                    if (subc2bs.CheckBoxLamination == true)
                    {
                        subc2bs.OrderID_Lamination = subc2bs.Id;

                        SLaminations.Insert(slaminations);
                        slaminations.Id = subc2bs.OrderID_Lamination;
                        slaminations.C2BNo = subc2bs.C2BNo;
                        slaminations.SubC2B = subc2bs.SubC2B;
                        slaminations.Status_Lamination = "อยู่ในคิว";
                        slaminations.Id_lamination = subc2bs.Id;
                        slaminations.Duedate = userwToEdit.Comment;
                        slaminations.NameSale = userwToEdit.Name_Sale;
                        slaminations.Status_Show = "0";
                        slaminations.Status_Check = subc2bs.CheckBoxLamination;
                        slaminations.Customer = userwToEdit.Customer;
                        SLaminations.Commit();
                    }
                    else if(subc2bs.CheckBoxLamination == false)
                    {
                        subc2bs.OrderID_Lamination = subc2bs.Id;
                        SLaminations.Insert(slaminations);
                        slaminations.Id = subc2bs.OrderID_Lamination;
                        slaminations.Id_lamination = subc2bs.Id;
                        slaminations.Status_Show = "0";
                        slaminations.Status_Check = subc2bs.CheckBoxLamination;
                        SLaminations.Commit();
                    }
                    if (subc2bs.CheckBoxCut== true)
                    {
                        subc2bs.OrderID_Cut = subc2bs.Id;

                        SCuts.Insert(scuts);
                        scuts.Id = subc2bs.OrderID_Cut;
                        scuts.C2BNo = subc2bs.C2BNo;
                        scuts.SubC2B = subc2bs.SubC2B;
                        if(subc2bs.CheckBoxLamination == true)
                        {
                            scuts.Status_Cut = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false)
                        {
                            scuts.Status_Cut = "อยู่ในคิว";
                        }
                        scuts.Id_Cut = subc2bs.Id;
                        scuts.Duedate = userwToEdit.Comment;
                        scuts.NameSale = userwToEdit.Name_Sale;
                        scuts.Status_Show = "0";
                        scuts.Status_Check = subc2bs.CheckBoxCut;
                        scuts.Customer = userwToEdit.Customer;
                        SCuts.Commit();
                    }
                    else if (subc2bs.CheckBoxCut == false)
                    {
                        subc2bs.OrderID_Cut = subc2bs.Id;
                        SCuts.Insert(scuts);
                        scuts.Id = subc2bs.OrderID_Cut;
                        scuts.Id_Cut = subc2bs.Id;
                        scuts.Status_Show = "0";
                        scuts.Status_Check = subc2bs.CheckBoxCut;
                        SCuts.Commit();
                    }
                    if (subc2bs.CheckBoxEdgeBanding == true)
                    {
                        subc2bs.OrderID_EdgeBanding = subc2bs.Id;

                        SEdgebandings.Insert(sedgebandings);
                        sedgebandings.Id = subc2bs.OrderID_EdgeBanding;
                        sedgebandings.C2BNo = subc2bs.C2BNo;
                        sedgebandings.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            sedgebandings.Status_Edgebanding = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            sedgebandings.Status_Edgebanding = wait2;
                        }
                        else
                        {
                            sedgebandings.Status_Edgebanding = "อยู่ในคิว";
                        }
                        sedgebandings.Id_Edgebanding = subc2bs.Id;
                        sedgebandings.Status_Show = "0";
                        sedgebandings.Duedate = userwToEdit.Comment;
                        sedgebandings.NameSale = userwToEdit.Name_Sale;
                        sedgebandings.Status_Check = subc2bs.CheckBoxEdgeBanding;
                        sedgebandings.Customer = userwToEdit.Customer;
                        SEdgebandings.Commit();
                    }
                    else if (subc2bs.CheckBoxEdgeBanding == false)
                    {
                        subc2bs.OrderID_EdgeBanding = subc2bs.Id;
                        SEdgebandings.Insert(sedgebandings);
                        sedgebandings.Id = subc2bs.OrderID_EdgeBanding;
                        sedgebandings.Id_Edgebanding = subc2bs.Id;
                        sedgebandings.Status_Show = "0";
                        sedgebandings.Status_Check = subc2bs.CheckBoxEdgeBanding;
                        SEdgebandings.Commit();
                    }
                    if (subc2bs.CheckBoxDrill == true)
                    {
                        subc2bs.OrderID_Drill = subc2bs.Id;

                        SDrills.Insert(sdrills);
                        sdrills.Id = subc2bs.OrderID_Drill;
                        sdrills.C2BNo = subc2bs.C2BNo;
                        sdrills.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            sdrills.Status_Drill = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            sdrills.Status_Drill = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            sdrills.Status_Drill = wait3;
                        }
                        else
                        {
                            sdrills.Status_Drill = "อยู่ในคิว";
                        }
                        sdrills.Id_Drill = subc2bs.Id;
                        sdrills.Duedate = userwToEdit.Comment;
                        sdrills.NameSale = userwToEdit.Name_Sale;
                        sdrills.Status_Show = "0";
                        sdrills.Status_Check = subc2bs.CheckBoxDrill;
                        sdrills.Customer = userwToEdit.Customer;
                        SDrills.Commit();
                    }
                    else if (subc2bs.CheckBoxDrill == false)
                    {
                        subc2bs.OrderID_Drill = subc2bs.Id;
                        SDrills.Insert(sdrills);
                        sdrills.Id = subc2bs.OrderID_Drill;
                        sdrills.Id_Drill = subc2bs.Id;
                        sdrills.Status_Show = "0";
                        sdrills.Status_Check = subc2bs.CheckBoxDrill;
                        SDrills.Commit();
                    }
                    if (subc2bs.CheckBoxPainting == true)
                    {
                        subc2bs.OrderID_Painting = subc2bs.Id;

                        SPaintings.Insert(spaintings);
                        spaintings.Id = subc2bs.OrderID_Painting;
                        spaintings.C2BNo = subc2bs.C2BNo;
                        spaintings.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            spaintings.Status_Painting = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            spaintings.Status_Painting = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            spaintings.Status_Painting = wait3;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == true)
                        {
                            spaintings.Status_Painting = wait4;
                        }
                        else
                        {
                            spaintings.Status_Painting = "อยู่ในคิว";
                        }
                        spaintings.Id_Painting = subc2bs.Id;
                        spaintings.Duedate = userwToEdit.Comment;
                        spaintings.NameSale = userwToEdit.Name_Sale;
                        spaintings.Status_Show = "0";
                        spaintings.Status_Check = subc2bs.CheckBoxPainting;
                        spaintings.Customer = userwToEdit.Customer;
                        SPaintings.Commit();
                    }
                    else if (subc2bs.CheckBoxPainting == false)
                    {
                        subc2bs.OrderID_Painting = subc2bs.Id;
                        SPaintings.Insert(spaintings);
                        spaintings.Id = subc2bs.OrderID_Painting;
                        spaintings.Id_Painting = subc2bs.Id;
                        spaintings.Status_Show = "0";
                        spaintings.Status_Check = subc2bs.CheckBoxPainting;
                        SPaintings.Commit();
                    }
                    if (subc2bs.CheckBoxCleaning == true)
                    {
                        subc2bs.OrderID_Cleaning = subc2bs.Id;

                        SCleanings.Insert(scleanings);
                        scleanings.Id = subc2bs.OrderID_Cleaning;
                        scleanings.C2BNo = subc2bs.C2BNo;
                        scleanings.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            scleanings.Status_Cleaning = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            scleanings.Status_Cleaning = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            scleanings.Status_Cleaning = wait3;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == true)
                        {
                            scleanings.Status_Cleaning = wait4;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == true)
                        {
                            scleanings.Status_Cleaning = wait5;
                        }
                        else
                        {
                            scleanings.Status_Cleaning = "อยู่ในคิว";
                        }
                        scleanings.Id_Cleaning = subc2bs.Id;
                        scleanings.Duedate = userwToEdit.Comment;
                        scleanings.NameSale = userwToEdit.Name_Sale;
                        scleanings.Status_Show = "0";
                        scleanings.Status_Check = subc2bs.CheckBoxCleaning;
                        scleanings.Customer = userwToEdit.Customer;
                        SCleanings.Commit();
                    }
                    else if (subc2bs.CheckBoxCleaning == false)
                    {
                        subc2bs.OrderID_Cleaning = subc2bs.Id;
                        SCleanings.Insert(scleanings);
                        scleanings.Id = subc2bs.OrderID_Cleaning;
                        scleanings.Id_Cleaning = subc2bs.Id;
                        scleanings.Status_Show = "0";
                        scleanings.Status_Check = subc2bs.CheckBoxCleaning;
                        SCleanings.Commit();
                    }
                    if (subc2bs.CheckBoxPacking == true)
                    {
                        subc2bs.OrderID_Packing = subc2bs.Id;

                        SPackings.Insert(spackings);
                        spackings.Id = subc2bs.OrderID_Packing;
                        spackings.C2BNo = subc2bs.C2BNo;
                        spackings.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            spackings.Status_Packing = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            spackings.Status_Packing = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            spackings.Status_Packing = wait3;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == true)
                        {
                            spackings.Status_Packing = wait4;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == true)
                        {
                            spackings.Status_Packing = wait5;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == true)
                        {
                            spackings.Status_Packing = wait6;
                        }
                        else
                        {
                            spackings.Status_Packing = "อยู่ในคิว";
                        }
                        spackings.Id_Packing = subc2bs.Id;
                        spackings.Duedate = userwToEdit.Comment;
                        spackings.NameSale = userwToEdit.Name_Sale;
                        spackings.Status_Show = "0";
                        spackings.Status_Check = subc2bs.CheckBoxPacking;
                        spackings.Customer = userwToEdit.Customer;
                        SPackings.Commit();
                    }
                    else if (subc2bs.CheckBoxPacking == false)
                    {
                        subc2bs.OrderID_Packing = subc2bs.Id;
                        SPackings.Insert(spackings);
                        spackings.Id = subc2bs.OrderID_Packing;
                        spackings.Id_Packing = subc2bs.Id;
                        spackings.Status_Show = "0";
                        spackings.Status_Check = subc2bs.CheckBoxPacking;
                        SPackings.Commit();
                    }
                    if (subc2bs.CheckBoxQC == true)
                    {
                        subc2bs.OrderID_QC = subc2bs.Id;

                        SQCs.Insert(sqcs);
                        sqcs.Id = subc2bs.OrderID_QC;
                        sqcs.C2BNo = subc2bs.C2BNo;
                        sqcs.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            sqcs.Status_QC = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            sqcs.Status_QC = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            sqcs.Status_QC = wait3;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == true)
                        {
                            sqcs.Status_QC = wait4;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == true)
                        {
                            sqcs.Status_QC = wait5;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == true)
                        {
                            sqcs.Status_QC = wait6;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == false && subc2bs.CheckBoxPacking == true)
                        {
                            sqcs.Status_QC = wait7;
                        }
                        else
                        {
                            sqcs.Status_QC = "อยู่ในคิว";
                        }
                        sqcs.Id_QC = subc2bs.Id;
                        sqcs.Duedate = userwToEdit.Comment;
                        sqcs.NameSale = userwToEdit.Name_Sale;
                        sqcs.Status_Show = "0";
                        sqcs.Status_Check = subc2bs.CheckBoxQC;
                        sqcs.Customer = userwToEdit.Customer;
                        SQCs.Commit();
                    }
                    else if (subc2bs.CheckBoxQC == false)
                    {
                        subc2bs.OrderID_QC = subc2bs.Id;
                        SQCs.Insert(sqcs);
                        sqcs.Id = subc2bs.OrderID_QC;
                        sqcs.Id_QC = subc2bs.Id;
                        sqcs.Status_Show = "0";
                        sqcs.Status_Check = subc2bs.CheckBoxQC;
                        SQCs.Commit();
                    }
                    //if (subc2bs.CheckBoxPickup == true)
                    //{
                        subc2bs.OrderID_Pickup = subc2bs.Id;

                        SPickups.Insert(spickups);
                        spickups.Id = subc2bs.OrderID_Pickup;
                        spickups.C2BNo = subc2bs.C2BNo;
                        spickups.SubC2B = subc2bs.SubC2B;
                        if (subc2bs.CheckBoxLamination == true)
                        {
                            spickups.Status_Pickup = wait1;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == true)
                        {
                            spickups.Status_Pickup = wait2;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == true)
                        {
                            spickups.Status_Pickup = wait3;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == true)
                        {
                            spickups.Status_Pickup = wait4;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == true)
                        {
                            spickups.Status_Pickup = wait5;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == true)
                        {
                            spickups.Status_Pickup = wait6;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == false && subc2bs.CheckBoxPacking == true)
                        {
                            spickups.Status_Pickup = wait7;
                        }
                        else if (subc2bs.CheckBoxLamination == false && subc2bs.CheckBoxCut == false && subc2bs.CheckBoxEdgeBanding == false && subc2bs.CheckBoxDrill == false && subc2bs.CheckBoxPainting == false && subc2bs.CheckBoxCleaning == false && subc2bs.CheckBoxPacking == false && subc2bs.CheckBoxQC == true)
                        {
                            spickups.Status_Pickup = wait8;
                        }
                        else
                        {
                            spickups.Status_Pickup = "อยู่ในคิว";
                        }
                        spickups.Id_Pickup = subc2bs.Id;
                        spickups.Duedate = userwToEdit.Comment;
                        spickups.NameSale = userwToEdit.Name_Sale;
                        spickups.Status_Show = "0";
                        spickups.Status_Check = subc2bs.CheckBoxPickup;
                        spickups.Customer = userwToEdit.Customer;
                        SPickups.Commit();
                    //}
                    //else if (subc2bs.CheckBoxPickup == false)
                    //{
                    //    subc2bs.OrderID_Pickup = subc2bs.Id;
                    //    SPickups.Insert(spickups);
                    //    spickups.Id = subc2bs.OrderID_Pickup;
                    //    spickups.Id_Pickup = subc2bs.Id;
                    //    spickups.Status_Show = "0";
                    //    spickups.Status_Check = subc2bs.CheckBoxPickup;
                    //    SPickups.Commit();
                    //}
                    Subc2bs.Commit();
                    userwToEdit.SubC2B = userwToEdit.SubC2B + 1;
                    Userworks.Commit();

                    return RedirectToAction("Index", "SubC2B", new { id = subc2bs.SubID });
                //}
            }
        }

        public ActionResult Details(string Id)
        {
            Sub_C2B subc2bToView = Subc2bs.Find(Id);
            if (subc2bToView == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(subc2bToView);
            }

        }
        //public ActionResult Pause(string Id)
        //{
        //    Sub_C2B subc2bToPause = Subc2bs.Find(Id);
        //    if (subc2bToPause == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        SO_PAUSE NSopause = new SO_PAUSE();
        //        return View(subc2bToPause);
        //    }
        //}
        //[HttpPost]
        public ActionResult Urgent(Sub_C2B subc2bs, SO_PAUSE sopauses, string Id, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bToUrgent = Subc2bs.Find(Id);
            lamis = subc2bToUrgent.OrderID_Lamination;
            cutu = subc2bToUrgent.OrderID_Cut;
            edgeu = subc2bToUrgent.OrderID_EdgeBanding;
            drillu = subc2bToUrgent.OrderID_Drill;
            paintu = subc2bToUrgent.OrderID_Painting;
            cleanu = subc2bToUrgent.OrderID_Cleaning;
            packu = subc2bToUrgent.OrderID_Packing;
            qcu = subc2bToUrgent.OrderID_QC;
            picku = subc2bToUrgent.OrderID_Pickup;
            s_Lamination Lamiu = SLaminations.Find(lamis);
            s_Cut Cutu = SCuts.Find(cutu);
            s_Edgebanding Edgeu = SEdgebandings.Find(edgeu);
            s_Drill Drillu = SDrills.Find(drillu);
            s_Painting Paintu = SPaintings.Find(paintu);
            s_Cleaning Cleanu = SCleanings.Find(cleanu);
            s_Packing Packu = SPackings.Find(packu);
            s_QC QCu = SQCs.Find(qcu);
            s_Pickup Picku = SPickups.Find(picku);

            if (subc2bToUrgent == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                subc2bToUrgent.Urgent_Status = "1";
                Lamiu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Cutu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Edgeu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Drillu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Paintu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Cleanu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Packu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                QCu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Picku.Urgent_Status = subc2bToUrgent.Urgent_Status;

                Subc2bs.Commit();
                SOPAUSEs.Commit();
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                return RedirectToAction("Index", new { id = subc2bToUrgent.SubID });
            }
        }
        public ActionResult UnUrgent(Sub_C2B subc2bs, SO_PAUSE sopauses, string Id, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bToUrgent = Subc2bs.Find(Id);
            lamis = subc2bToUrgent.OrderID_Lamination;
            cutu = subc2bToUrgent.OrderID_Cut;
            edgeu = subc2bToUrgent.OrderID_EdgeBanding;
            drillu = subc2bToUrgent.OrderID_Drill;
            paintu = subc2bToUrgent.OrderID_Painting;
            cleanu = subc2bToUrgent.OrderID_Cleaning;
            packu = subc2bToUrgent.OrderID_Packing;
            qcu = subc2bToUrgent.OrderID_QC;
            picku = subc2bToUrgent.OrderID_Pickup;
            s_Lamination Lamiu = SLaminations.Find(lamis);
            s_Cut Cutu = SCuts.Find(cutu);
            s_Edgebanding Edgeu = SEdgebandings.Find(edgeu);
            s_Drill Drillu = SDrills.Find(drillu);
            s_Painting Paintu = SPaintings.Find(paintu);
            s_Cleaning Cleanu = SCleanings.Find(cleanu);
            s_Packing Packu = SPackings.Find(packu);
            s_QC QCu = SQCs.Find(qcu);
            s_Pickup Picku = SPickups.Find(picku);

            if (subc2bToUrgent == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                subc2bToUrgent.Urgent_Status = "0";
                Lamiu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Cutu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Edgeu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Drillu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Paintu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Cleanu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Packu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                QCu.Urgent_Status = subc2bToUrgent.Urgent_Status;
                Picku.Urgent_Status = subc2bToUrgent.Urgent_Status;

                Subc2bs.Commit();
                SOPAUSEs.Commit();
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                return RedirectToAction("Index", new { id = subc2bToUrgent.SubID });
            }
        }
        public ActionResult Refresh(Sub_C2B subc2bs, SO_PAUSE sopauses, string Id, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bToUrgent = Subc2bs.Find(Id);
            lamis = subc2bToUrgent.OrderID_Lamination;
            cutu = subc2bToUrgent.OrderID_Cut;
            edgeu = subc2bToUrgent.OrderID_EdgeBanding;
            drillu = subc2bToUrgent.OrderID_Drill;
            paintu = subc2bToUrgent.OrderID_Painting;
            cleanu = subc2bToUrgent.OrderID_Cleaning;
            packu = subc2bToUrgent.OrderID_Packing;
            qcu = subc2bToUrgent.OrderID_QC;
            picku = subc2bToUrgent.OrderID_Pickup;
            s_Lamination Lamiu = SLaminations.Find(lamis);
            s_Cut Cutu = SCuts.Find(cutu);
            s_Edgebanding Edgeu = SEdgebandings.Find(edgeu);
            s_Drill Drillu = SDrills.Find(drillu);
            s_Painting Paintu = SPaintings.Find(paintu);
            s_Cleaning Cleanu = SCleanings.Find(cleanu);
            s_Packing Packu = SPackings.Find(packu);
            s_QC QCu = SQCs.Find(qcu);
            s_Pickup Picku = SPickups.Find(picku);

            if (subc2bToUrgent == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }

                if (Lamiu.Status_Lamination == "อยู่ในคิว" || Cutu.Status_Cut == "อยู่ในคิว" || Drillu.Status_Drill == "อยู่ในคิว" || Paintu.Status_Painting == "อยู่ในคิว" || Cleanu.Status_Cleaning == "อยู่ในคิว" || Packu.Status_Packing == "อยู่ในคิว" || QCu.Status_QC == "อยู่ในคิว" || Picku.Status_Pickup == "อยู่ในคิว")
                {
                    subc2bToUrgent.Status_AStation = "อยู่ในคิว";
                }
                else if (Lamiu.Status_Lamination == "กำลังผลิต" || Cutu.Status_Cut == "กำลังผลิต" || Drillu.Status_Drill == "กำลังผลิต" || Paintu.Status_Painting == "กำลังผลิต" || Cleanu.Status_Cleaning == "กำลังผลิต" || Packu.Status_Packing == "กำลังผลิต" || QCu.Status_QC == "กำลังผลิต" || Picku.Status_Pickup == "กำลังผลิต")
                {
                    subc2bToUrgent.Status_AStation = "กำลังผลิต";
                }
                else if (Lamiu.Status_Lamination == "ปฏิเสธ" || Cutu.Status_Cut == "ปฏิเสธ" || Drillu.Status_Drill == "ปฏิเสธ" || Paintu.Status_Painting == "ปฏิเสธ" || Cleanu.Status_Cleaning == "ปฏิเสธ" || Packu.Status_Packing == "ปฏิเสธ" || QCu.Status_QC == "ปฏิเสธ" || Picku.Status_Pickup == "ปฏิเสธ")
                {
                    subc2bToUrgent.Status_AStation = "ปฏิเสธ";
                }
                else if (Lamiu.Status_Lamination == "หยุดชั่วคราว" || Cutu.Status_Cut == "หยุดชั่วคราว" || Drillu.Status_Drill == "หยุดชั่วคราว" || Paintu.Status_Painting == "หยุดชั่วคราว" || Cleanu.Status_Cleaning == "หยุดชั่วคราว" || Packu.Status_Packing == "หยุดชั่วคราว" || QCu.Status_QC == "หยุดชั่วคราว" || Picku.Status_Pickup == "หยุดชั่วคราว")
                {
                    subc2bToUrgent.Status_AStation = "หยุดชั่วคราว";
                }
                else if (Lamiu.Status_Lamination == "หยุด" || Cutu.Status_Cut == "หยุด" || Drillu.Status_Drill == "หยุด" || Paintu.Status_Painting == "หยุด" || Cleanu.Status_Cleaning == "หยุด" || Packu.Status_Packing == "หยุด" || QCu.Status_QC == "หยุด" || Picku.Status_Pickup == "หยุด")
                {
                    subc2bToUrgent.Status_AStation = "หยุด";
                }
                else if (Picku.Status_Pickup == "เสร็จ")
                {
                    subc2bToUrgent.Status_AStation = "เสร็จ";
                }

                Subc2bs.Commit();
                SOPAUSEs.Commit();
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                return RedirectToAction("Index", new { id = subc2bToUrgent.SubID });
            }
        }
        public ActionResult Pause(Sub_C2B subc2bs, SO_PAUSE sopauses, string Id, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bToPause = Subc2bs.Find(Id);
            lamis = subc2bToPause.OrderID_Lamination;
            cutu = subc2bToPause.OrderID_Cut;
            edgeu = subc2bToPause.OrderID_EdgeBanding;
            drillu = subc2bToPause.OrderID_Drill;
            paintu = subc2bToPause.OrderID_Painting;
            cleanu = subc2bToPause.OrderID_Cleaning;
            packu = subc2bToPause.OrderID_Packing;
            qcu = subc2bToPause.OrderID_QC;
            picku = subc2bToPause.OrderID_Pickup;
            s_Lamination Lamipause = SLaminations.Find(lamis);
            s_Cut Cutpause = SCuts.Find(cutu);
            s_Edgebanding Edgepause = SEdgebandings.Find(edgeu);
            s_Drill Drillpause = SDrills.Find(drillu);
            s_Painting Paintpause = SPaintings.Find(paintu);
            s_Cleaning Cleanpause = SCleanings.Find(cleanu);
            s_Packing Packpause = SPackings.Find(packu);
            s_QC QCpause = SQCs.Find(qcu);
            s_Pickup Pickpause = SPickups.Find(picku);

            if (subc2bToPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                //SO_PAUSE sopauses = new SO_PAUSE();
                subc2bToPause.Order_PauseID = Guid.NewGuid().ToString();
                subc2bToPause.Check_PauseID = "1";
                SOPAUSEs.Insert(sopauses);
                sopauses.Id = subc2bToPause.Order_PauseID;
                sopauses.SOID = subc2bToPause.Id;
                sopauses.Lamination = Lamipause.Status_Lamination;
                if (Lamipause.Status_Lamination != null)
                {
                    Lamipause.Status_Lamination = "หยุด";
                }
                sopauses.Cut = Cutpause.Status_Cut;
                if (Cutpause.Status_Cut != null)
                {
                    Cutpause.Status_Cut = "หยุด";
                }
                sopauses.Edgebamding = Edgepause.Status_Edgebanding;
                if (Edgepause.Status_Edgebanding != null)
                {
                    Edgepause.Status_Edgebanding = "หยุด";
                }
                sopauses.Drill = Drillpause.Status_Drill;
                if (Drillpause.Status_Drill != null)
                {
                    Drillpause.Status_Drill = "หยุด";
                }
                sopauses.Painting = Paintpause.Status_Painting;
                if (Paintpause.Status_Painting != null)
                {
                    Paintpause.Status_Painting = "หยุด";
                }
                sopauses.Cleaning = Cleanpause.Status_Cleaning;
                if (Cleanpause.Status_Cleaning != null)
                {
                    Cleanpause.Status_Cleaning = "หยุด";
                }
                sopauses.Packing = Packpause.Status_Packing;
                if (Packpause.Status_Packing != null)
                {
                    Packpause.Status_Packing = "หยุด";
                }
                sopauses.QC = QCpause.Status_QC;
                if (QCpause.Status_QC != null)
                {
                    QCpause.Status_QC = "หยุด";
                }
                sopauses.Pickup = Pickpause.Status_Pickup;
                if (Pickpause.Status_Pickup != null)
                {
                    Pickpause.Status_Pickup = "หยุด";
                }
                subc2bToPause.Previousstatus = subc2bToPause.Status_AStation;
                subc2bToPause.Status_AStation = "หยุด";
                Subc2bs.Commit();
                SOPAUSEs.Commit();
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                return RedirectToAction("Index", new { id = subc2bToPause.SubID });
            }
        }
        public ActionResult UnPause(Sub_C2B subc2bs, SO_PAUSE sopauses,string unpause, string Id, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            Sub_C2B subc2bToPause = Subc2bs.Find(Id);
            lamis = subc2bToPause.OrderID_Lamination;
            cutu = subc2bToPause.OrderID_Cut;
            edgeu = subc2bToPause.OrderID_EdgeBanding;
            drillu = subc2bToPause.OrderID_Drill;
            paintu = subc2bToPause.OrderID_Painting;
            cleanu = subc2bToPause.OrderID_Cleaning;
            packu = subc2bToPause.OrderID_Packing;
            qcu = subc2bToPause.OrderID_QC;
            picku = subc2bToPause.OrderID_Pickup;
            s_Lamination Lamipause = SLaminations.Find(lamis);
            s_Cut Cutpause = SCuts.Find(cutu);
            s_Edgebanding Edgepause = SEdgebandings.Find(edgeu);
            s_Drill Drillpause = SDrills.Find(drillu);
            s_Painting Paintpause = SPaintings.Find(paintu);
            s_Cleaning Cleanpause = SCleanings.Find(cleanu);
            s_Packing Packpause = SPackings.Find(packu);
            s_QC QCpause = SQCs.Find(qcu);
            s_Pickup Pickpause = SPickups.Find(picku);
            unpause = subc2bToPause.Order_PauseID;
            SO_PAUSE souspauses = SOPAUSEs.Find(unpause);

            if (subc2bToPause == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                if (Lamipause.Status_Lamination != null)
                {
                    Lamipause.Status_Lamination = souspauses.Lamination;
                }
                if (Cutpause.Status_Cut != null)
                {
                    Cutpause.Status_Cut = souspauses.Cut;
                }
                if (Edgepause.Status_Edgebanding != null)
                {
                    Edgepause.Status_Edgebanding = souspauses.Edgebamding;
                }
                if (Drillpause.Status_Drill != null)
                {
                    Drillpause.Status_Drill = souspauses.Drill;
                }
                if (Paintpause.Status_Painting != null)
                {
                    Paintpause.Status_Painting = souspauses.Painting;
                }
                if (Cleanpause.Status_Cleaning != null)
                {
                    Cleanpause.Status_Cleaning = souspauses.Cleaning;
                }
                if (Packpause.Status_Packing != null)
                {
                    Packpause.Status_Packing = souspauses.Packing;
                }
                if (QCpause.Status_QC != null)
                {
                    QCpause.Status_QC = souspauses.QC;
                }
                if (Pickpause.Status_Pickup != null)
                {
                    Pickpause.Status_Pickup = souspauses.Pickup;
                }
                subc2bToPause.Status_AStation = subc2bToPause.Previousstatus;
                subc2bToPause.Check_PauseID = "0";
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                Subc2bs.Commit();
                return RedirectToAction("Index", new { id = subc2bToPause.SubID });
            }
        }
        //public ActionResult Cancel(string Id)
        //{
        //    Sub_C2B subc2bToCancel = Subc2bs.Find(Id);
        //    if (subc2bToCancel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        subc2bToCancel.Status_AStation = "ยกเลิก";
        //        return View(subc2bToCancel);
        //    }
        //}
        //[HttpPost]
        [ValidateInput(false)]
        public ActionResult Cancel(Sub_C2B subc2bs,IndexView idv,string reason, string Id,string IdSub, string lamis, string cutu, string edgeu, string drillu, string paintu, string cleanu, string packu, string qcu, string picku)
        {
            idv.subc2b = Subc2bs.Find(IdSub);
            Sub_C2B subc2bToCancel = Subc2bs.Find(IdSub);
            lamis = subc2bToCancel.OrderID_Lamination;
            s_Lamination LamiAccept = SLaminations.Find(lamis);
            cutu = subc2bToCancel.OrderID_Cut;
            s_Cut cutAccept = SCuts.Find(cutu);
            edgeu = subc2bToCancel.OrderID_EdgeBanding;
            s_Edgebanding edgeAccept = SEdgebandings.Find(edgeu);
            drillu = subc2bToCancel.OrderID_Drill;
            s_Drill drillAccept = SDrills.Find(drillu);
            paintu = subc2bToCancel.OrderID_Painting;
            s_Painting paintAccept = SPaintings.Find(paintu);
            cleanu = subc2bToCancel.OrderID_Cleaning;
            s_Cleaning cleanAccept = SCleanings.Find(cleanu);
            packu = subc2bToCancel.OrderID_Packing;
            s_Packing packAccept = SPackings.Find(packu);
            qcu = subc2bToCancel.OrderID_QC;
            s_QC qcAccept = SQCs.Find(qcu);
            picku = subc2bToCancel.OrderID_Pickup;
            s_Pickup pickAccept = SPickups.Find(picku);

            if (subc2bToCancel == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                subc2bToCancel.Status_AStation = "ยกเลิก";
                LamiAccept.Status_Show = "0";
                cutAccept.Status_Show = "0";
                edgeAccept.Status_Show = "0";
                drillAccept.Status_Show = "0";
                paintAccept.Status_Show = "0";
                cleanAccept.Status_Show = "0";
                packAccept.Status_Show = "0";
                qcAccept.Status_Show = "0";
                pickAccept.Status_Show = "0";
                subc2bToCancel.Cancel_Reason = reason;
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

                return RedirectToAction("Index", new { id = subc2bToCancel.SubID });
            }
        }

        public ActionResult Edit(string Id)
        {
            Sub_C2B subc2bToEdit = Subc2bs.Find(Id);
            if (subc2bToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(subc2bToEdit);
            }
        }
        [HttpPost]
        public ActionResult Edit(Sub_C2B subc2bs, string Id)
        {
            Sub_C2B subc2bToEdit = Subc2bs.Find(Id);

            if (subc2bToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                subc2bToEdit.C2BNo = subc2bs.C2BNo;
                subc2bToEdit.SubC2B = subc2bs.SubC2B;
                subc2bToEdit.OrderID_Lamination = subc2bs.OrderID_Lamination;

                Subc2bs.Commit();

                return RedirectToAction("Index", new { id = subc2bToEdit.SubID });
            }
        }

        public ActionResult Delete(string Id)
        {
            Sub_C2B subc2bToDelete = Subc2bs.Find(Id);
            if (subc2bToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(subc2bToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(User_Works userw, string Id,string SubID,string Sub2CBID)
        {
            Sub_C2B subc2bToDelete = Subc2bs.Find(Id);
            SubID = subc2bToDelete.SubID;
            Sub2CBID = subc2bToDelete.Id;
            User_Works userToDelete = Userworks.Find(SubID);
            if (subc2bToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                SLaminations.Delete(Sub2CBID);
                SCuts.Delete(Sub2CBID);
                SEdgebandings.Delete(Sub2CBID);
                SDrills.Delete(Sub2CBID);
                SPaintings.Delete(Sub2CBID);
                SCleanings.Delete(Sub2CBID);
                SPackings.Delete(Sub2CBID);
                SQCs.Delete(Sub2CBID);
                SPickups.Delete(Sub2CBID);
                Subc2bs.Delete(Id);
                SLaminations.Commit();
                SCuts.Commit();
                SEdgebandings.Commit();
                SDrills.Commit();
                SPaintings.Commit();
                SCleanings.Commit();
                SPackings.Commit();
                SQCs.Commit();
                SPickups.Commit();
                Subc2bs.Commit();
                userToDelete.SubC2B = userToDelete.SubC2B - 1;
                Userworks.Commit();
                return RedirectToAction("Index", new { id = subc2bToDelete.SubID });
            }
        }
    }
}