using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class ReasonPauseController : Controller
    {
        // GET: ReasonPause
        IRepository<ReasonPause> ReasonPauses;
        public ReasonPauseController(IRepository<ReasonPause> rps)
        {
            ReasonPauses = rps;
        }
        public ActionResult Index()
        {
            List<ReasonPause> rlists = ReasonPauses.Collection().OrderByDescending(o => o.CreateAt).ToList();
            return View(rlists);
        }
        public ActionResult Create()
        {
            ReasonPause userlC = new ReasonPause();
            return View(userlC);
        }
        [HttpPost]
        public ActionResult Create(ReasonPause userl)
        {
            if (!ModelState.IsValid)
            {
                return View(userl);
            }
            else
            {
                ReasonPauses.Insert(userl);
                ReasonPauses.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ReasonPause userwToEdit = ReasonPauses.Find(Id);
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
        public ActionResult Edit(ReasonPause userw, string Id)
        {
            ReasonPause userwToEdit = ReasonPauses.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userwToEdit.No = userw.No;
                userwToEdit.Reason = userw.Reason;
                ReasonPauses.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ReasonPause uToDelete = ReasonPauses.Find(Id);
            if (uToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(uToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ReasonPause uToDelete = ReasonPauses.Find(Id);
            if (uToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                ReasonPauses.Delete(Id);
                ReasonPauses.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
