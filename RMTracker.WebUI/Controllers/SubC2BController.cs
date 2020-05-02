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

        public SubC2BController(IRepository<User_Works> userwcontext,IRepository<Sub_C2B> SubC2BContext)
        {
            Userworks = userwcontext;
            Subc2bs = SubC2BContext;
        }
        // GET: UserWorks
        public ActionResult Index(string Id)
        {
            List<Sub_C2B> subc2b = Subc2bs.Collection().Where(o => o.SubID == Id).ToList();
            return View(subc2b);
            //Sub_C2B Subindex = Subc2bs.Find(c2bId);
            //var subindex = Subc2bs.Collection().Where(o => o.C2BNo =).Tol
            //if (Subindex == null)
            //{
            //    return HttpNotFound();
            //}
            //else
            //{
            //  var c2bmodel = new C2BC
            //  {
            //      Subc2bs = Subc2bs.Collection().Where(o => o.C2BNo == c2bId).ToList()
            //  };
            //  //userwToEdit = Userworks.Collection().Select(u => u.C2BNo).ToList();
            //  //var userworksId = subc2b.Select(o => o.C2BNo).ToList();

            // // List<Sub_C2B> subc2b = Subc2bs.Collection().Where(o => o.C2BNo == c2bId).ToList();

            ////subc2b[0].Id 
            //  return View(c2bmodel);
            //}
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
                viewModel.Userworks = Userworks.Collection();
                return View(viewModel);
            }
            //Sub_C2B subC2B = new Sub_C2B();
            //return View(subC2B);
        }
        [HttpPost]
        public ActionResult Create(Sub_C2B subc2bs, User_Works userw, string Id)
        {
            User_Works userwToEdit = Userworks.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }
                else
                {
                    Subc2bs.Insert(subc2bs);
                    subc2bs.C2BNo = userwToEdit.C2BNo;
                    subc2bs.SubID = userwToEdit.Id;
                    subc2bs.OrderID_Lamination = "In queue";
                    subc2bs.OrderID_Cut = "Wait 1";
                    subc2bs.OrderID_EdgeBanding = "Wait 2";
                    subc2bs.OrderID_Drill = "Wait 3";
                    subc2bs.OrderID_Packing = "Wait 4";
                    subc2bs.OrderID_Pickup = "Wait 5";
                    Subc2bs.Commit();

                    return RedirectToAction("Index", "UserWorks");
                }
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
        public ActionResult ConfirmDelete(User_Works userw, string Id)
        {
            Sub_C2B subc2bToDelete = Subc2bs.Find(Id);
            if (subc2bToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Subc2bs.Delete(Id);
                Subc2bs.Commit();
                return RedirectToAction("Index", new { id = subc2bToDelete.SubID });
            }
        }
    }
}