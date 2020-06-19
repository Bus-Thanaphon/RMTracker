using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMTracker.Core.Models;
using RMTracker.Core.Contracts;
using RMTracker.Core.ViewModels;
using System.Drawing;
using System.Data.Entity.Core.Metadata.Edm;
using System.Windows;

namespace RMTracker.WebUI.Controllers
{
    public class UserWorksController : Controller
    {
        IRepository<User_Works> context;
        IRepository<Sub_C2B> C2Bcontext;
        IRepository<UserList> UserLists;
        IRepository<s_Lamination> SLaminations;
        IRepository<s_Cut> SCuts;
        IRepository<s_Edgebanding> SEdgebandings;
        IRepository<s_Drill> SDrills;
        IRepository<s_Painting> SPaintings;
        IRepository<s_Cleaning> SCleanings;
        IRepository<s_Packing> SPackings;
        IRepository<s_QC> SQCs;
        IRepository<s_Pickup> SPickups;
        IRepository<On_Hold> Onhold;

        public UserWorksController(IRepository<User_Works> UserWorksContext, IRepository<Sub_C2B> C2BContext, IRepository<UserList> userlcon, IRepository<s_Lamination> laminationcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<On_Hold> OHcontext)
        {
            context = UserWorksContext;
            C2Bcontext = C2BContext;
            UserLists = userlcon;
            SLaminations = laminationcontext;
            SCuts = cutcontext;
            SEdgebandings = edgebandingcontext;
            SDrills = drillcontext;
            SPaintings = paintingcontext;
            SCleanings = cleaningcontext;
            SPackings = packingcontext;
            SQCs = qccontext;
            SPickups = pickupcontext;
            Onhold = OHcontext;
        }
        // GET: UserWorks
        public ActionResult CheckPassword(string Password)
        {
            switch (Password)
            {
                case "0000":
                    return RedirectToAction("Index");

                default:
                    // If they've submitted the form without a submitButton, 
                    // just return the view again.
                    return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Index(User_Works userw, string sortOrder, string searchBy, string search, string Namesearch, string searchByW, string Customersearch, bool checkResp = false)
        {
            //List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderByDescending(o => o.StartDate).ToList();
            //var userworksId = userworks.Select(o => o.C2BNo).ToList();

            //var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

            //foreach (var i in userworks) 
            //{
            //    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
            //}
            
            //return View(userworks);
            if (searchBy == "SOID" && checkResp == false)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.C2BNo.Contains(search) || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            if (searchBy == "SOID" && checkResp == true)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.C2BNo == search || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (searchBy == "Name" && checkResp == false)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.Name_Sale.Contains(search) || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (searchBy == "Name" && checkResp == true)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.Name_Sale == search || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (searchBy == "Customer" && checkResp == false)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.Customer.Contains(search) || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (searchBy == "Customer" && checkResp == true)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderBy(o => o.StartDate).Where(x => x.Customer == search || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (searchByW == "FinishStatus")
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status == "เสร็จแล้ว").OrderByDescending(o => o.StartDate).Where(x => x.Order_Status.Contains(search) || search == null).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (Namesearch != null)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderByDescending(o => o.StartDate).Where(x => x.Name_Sale == Namesearch).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (Customersearch != null && checkResp == false)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderByDescending(o => o.StartDate).Where(x => x.Customer == Customersearch).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else if (Customersearch != null && checkResp == true)
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderByDescending(o => o.StartDate).Where(x => x.Customer.Contains(Customersearch)).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
                }

                return View(userworks);
            }
            else
            {
                List<User_Works> userworks = context.Collection().Where(o => o.Order_Status != "เสร็จแล้ว").OrderByDescending(o => o.StartDate).ToList();
                var userworksId = userworks.Select(o => o.C2BNo).ToList();

                var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).OrderBy(o => o.SubC2B).ToList();

                foreach (var i in userworks)
                {
                    i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).OrderBy(o => o.SubC2B).ToList();
                }

                return View(userworks);
            }
        }
        public ActionResult Finish(string Id)
        {
            User_Works userwToEdit = context.Find(Id);
            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(userwToEdit);
            }
        }
        [HttpPost]
        public ActionResult Finish(User_Works userw, string Id)
        {
            User_Works userwToEdit = context.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userwToEdit.Order_Status = "เสร็จแล้ว";
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Create()
        {
            User_Works userW = new User_Works();
            userW.User_Sale = UserLists.Collection().Where(o => o.Department1 == "ฝ่ายขาย" || o.Department2 == "ฝ่ายขาย" || o.Department3 == "ฝ่ายขาย");
            List<User_Works> CheckSO = context.Collection().ToList();
            return View(userW);
        }
        [HttpPost]
        public ActionResult Create(User_Works userw,string link)
        {
            if (!ModelState.IsValid)
            {
                return View(userw);
            }
            else
            {
                context.Insert(userw);
                userw.C2BNo = $"SO-{userw.C2BNo}";
                userw.Name_Sale = userw.Name_Sale;
                userw.Order_Status = "อยู่ในคิว";
                List<User_Works> CheckSO = context.Collection().ToList();
                foreach (var i in CheckSO)
                {
                    if (i.C2BNo == userw.C2BNo)
                    {
                        ViewBag.showAlert = true;
                        ViewBag.alertMessage = "พบเลข SO ซ้ำในฐานข้อมูล!";
                        link = "Alert";
                        return View(link);
                    }
                }
                link = "Index";
                context.Commit();
                return RedirectToAction(link);
            }
        }

        public ActionResult Details(string Id)
        {
            User_Works userworkToView = context.Find(Id);
            if (userworkToView == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(userworkToView);
            }

        }

        public ActionResult Edit(string Id)
        {
            User_Works userwToEdit = context.Find(Id);
            userwToEdit.User_Sale = UserLists.Collection().Where(o => o.Department1 == "ฝ่ายขาย" || o.Department2 == "ฝ่ายขาย" || o.Department3 == "ฝ่ายขาย");
            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(userwToEdit);
            }
        }
        [HttpPost]
        public ActionResult Edit(User_Works userw,string Id)
        {
            User_Works userwToEdit = context.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userwToEdit.EndDate = userw.EndDate;
                userwToEdit.SubC2B = userw.SubC2B;
                userwToEdit.Name_Sale = userw.Name_Sale;
                userwToEdit.Customer = userw.Customer;
                userwToEdit.Comment = userw.Comment;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Hold(string Id)
        {
            User_Works userwToEdit = context.Find(Id);
            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                On_Hold onhold = new On_Hold();
                return View(userwToEdit);
            }
        }
        [HttpPost]
        public ActionResult Hold(User_Works userw,On_Hold onho, string Id)
        {
            User_Works userwToEdit = context.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(userw);
                }
                userwToEdit.OnholdID = Guid.NewGuid().ToString();
                userwToEdit.PreviousStatus = userwToEdit.Order_Status;
                userwToEdit.Order_Status = "พักงาน";
                Onhold.Insert(onho);
                onho.Id = userwToEdit.OnholdID;
                onho.OnholdNo = userwToEdit.Id;
                onho.Onhold_Start = DateTime.Now.ToString();
                context.Commit();
                Onhold.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult UnHold(string Id,string onhoID)
        {
            User_Works userwToEdit = context.Find(Id);
            onhoID = userwToEdit.OnholdID;
            On_Hold onholdedit = Onhold.Find(onhoID);
            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userwToEdit.Order_Status = userwToEdit.PreviousStatus;
                
                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            User_Works userwToDelete = context.Find(Id);
            if (userwToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(userwToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(User_Works userw, string Id,string idsub, string Dsub)
        {
            User_Works userwToDelete = context.Find(Id);
            idsub = userwToDelete.Id;
            List<Sub_C2B> subselect = C2Bcontext.Collection().Where(o => o.SubID == idsub).ToList();
            if (userwToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                foreach (var i in subselect)
                {
                    SLaminations.Delete(i.Id);
                    SCuts.Delete(i.Id);
                    SEdgebandings.Delete(i.Id);
                    SDrills.Delete(i.Id);
                    SPaintings.Delete(i.Id);
                    SCleanings.Delete(i.Id);
                    SPackings.Delete(i.Id);
                    SQCs.Delete(i.Id);
                    SPickups.Delete(i.Id);
                    C2Bcontext.Delete(i.Id);
                    SLaminations.Commit();
                    SCuts.Commit();
                    SEdgebandings.Commit();
                    SDrills.Commit();
                    SPaintings.Commit();
                    SCleanings.Commit();
                    SPackings.Commit();
                    SQCs.Commit();
                    SPickups.Commit();
                    C2Bcontext.Commit();
                }
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}