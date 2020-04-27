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
    public class SubC2BController : Controller
    {
        IRepository<User_Works> uwcontext;
        IRepository<Sub_C2B> c2bcontext;

        public SubC2BController(IRepository<User_Works> userwcontext,IRepository<Sub_C2B> SubC2BContext)
        {
            uwcontext = userwcontext;
            c2bcontext = SubC2BContext;
        }
        // GET: UserWorks
        public ActionResult Index()
        {
            List<Sub_C2B> subc2b = c2bcontext.Collection().ToList();
            return View(subc2b);
        }

        public ActionResult Create()
        {
            RMC2BView viewModel = new RMC2BView();

            viewModel.Sub_C2B_V = new Sub_C2B();
            viewModel.User_Works_V = uwcontext.Collection();
            return View(viewModel);

            //Sub_C2B subC2B = new Sub_C2B();
            //return View(subC2B);
        }
        [HttpPost]
        public ActionResult Create(Sub_C2B subc2bs)
        {
            if (!ModelState.IsValid)
            {
                return View(subc2bs);
            }
            else
            {
                c2bcontext.Insert(subc2bs);
                c2bcontext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string Id)
        {
            Sub_C2B subc2bToView = c2bcontext.Find(Id);
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
            Sub_C2B subc2bToEdit = c2bcontext.Find(Id);
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
            Sub_C2B subc2bToEdit = c2bcontext.Find(Id);

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

                c2bcontext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Sub_C2B subc2bToDelete = c2bcontext.Find(Id);
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
            Sub_C2B subc2bToDelete = c2bcontext.Find(Id);
            if (subc2bToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                c2bcontext.Delete(Id);
                c2bcontext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}