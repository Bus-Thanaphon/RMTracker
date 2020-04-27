using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class OnholdController : Controller
    {
        IRepository<On_Hold> OHcontext;

        public OnholdController(IRepository<On_Hold> OnHoldContext)
        {
            OHcontext = OnHoldContext;
        }
        // GET: UserWorks
        public ActionResult Index()
        {
            List<On_Hold> onholds = OHcontext.Collection().ToList();
            return View(onholds);
        }

        public ActionResult Create()
        {
            On_Hold onhold = new On_Hold();
            return View(onhold);
        }
        [HttpPost]
        public ActionResult Create(On_Hold onholds)
        {
            if (!ModelState.IsValid)
            {
                return View(onholds);
            }
            else
            {
                OHcontext.Insert(onholds);
                OHcontext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(string Id)
        {
            On_Hold ohToView = OHcontext.Find(Id);
            if (ohToView == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ohToView);
            }

        }

        public ActionResult Edit(string Id)
        {
            On_Hold ohToEdit = OHcontext.Find(Id);
            if (ohToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ohToEdit);
            }
        }
        [HttpPost]
        public ActionResult Edit(On_Hold onhold, string Id)
        {
            On_Hold ohToEdit = OHcontext.Find(Id);

            if (ohToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(ohToEdit);
                }
                ohToEdit.OnholdNo = onhold.OnholdNo;
                ohToEdit.Onhold_Start = onhold.Onhold_Start;
                ohToEdit.Onhold_End = onhold.Onhold_End;
                ohToEdit.Onhold_Reason = onhold.Onhold_Reason;

                OHcontext.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            On_Hold ohToDelete = OHcontext.Find(Id);
            if (ohToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ohToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(On_Hold onhold, string Id)
        {
            On_Hold ohToDelete = OHcontext.Find(Id);
            if (ohToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                OHcontext.Delete(Id);
                OHcontext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}