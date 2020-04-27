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
        IRepository<Sub_C2B> Sub_C2B_V;

        public SubC2BController(IRepository<User_Works> userwcontext,IRepository<Sub_C2B> SubC2BContext)
        {
            uwcontext = userwcontext;
            Sub_C2B_V = SubC2BContext;
        }
        // GET: UserWorks
        public ActionResult Index()
        {
            List<Sub_C2B> subc2b = Sub_C2B_V.Collection().ToList();
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
                Sub_C2B_V.Insert(subc2bs);
                Sub_C2B_V.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string Id)
        {
            Sub_C2B subc2bToView = Sub_C2B_V.Find(Id);
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
            Sub_C2B subc2bToEdit = Sub_C2B_V.Find(Id);
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
        public ActionResult Edit(User_Works userwork,Sub_C2B subc2b, string Id)
        {
            Sub_C2B subc2bToEdit = Sub_C2B_V.Find(Id);

            if (subc2bToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(subc2bToEdit);
                }
                subc2bToEdit.SubC2B = subc2b.SubC2B;
                subc2bToEdit.OrderID_Lamination = subc2b.OrderID_Lamination;

                Sub_C2B_V.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Sub_C2B subc2bToDelete = Sub_C2B_V.Find(Id);
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
            Sub_C2B subc2bToDelete = Sub_C2B_V.Find(Id);
            if (subc2bToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Sub_C2B_V.Delete(Id);
                Sub_C2B_V.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}