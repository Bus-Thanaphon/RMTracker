using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class ReasonDenineController : Controller
    {
        // GET: ReasonDenine
        IRepository<ReasonDenine> ReasonDenines;
        public ReasonDenineController(IRepository<ReasonDenine> rds)
        {
            ReasonDenines = rds;
        }
        public ActionResult Index()
        {
            List<ReasonDenine> rlists = ReasonDenines.Collection().OrderByDescending(o => o.CreateAt).ToList();
            return View(rlists);
        }
        public ActionResult Create()
        {
            ReasonDenine userlC = new ReasonDenine();
            return View(userlC);
        }
        [HttpPost]
        public ActionResult Create(ReasonDenine userl)
        {
            if (!ModelState.IsValid)
            {
                return View(userl);
            }
            else
            {
                ReasonDenines.Insert(userl);
                ReasonDenines.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ReasonDenine userwToEdit = ReasonDenines.Find(Id);
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
        public ActionResult Edit(ReasonDenine userw, string Id)
        {
            ReasonDenine userwToEdit = ReasonDenines.Find(Id);

            if (userwToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                userwToEdit.No = userw.No;
                userwToEdit.Reason = userw.Reason;
                ReasonDenines.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            ReasonDenine uToDelete = ReasonDenines.Find(Id);
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
            ReasonDenine uToDelete = ReasonDenines.Find(Id);
            if (uToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                ReasonDenines.Delete(Id);
                ReasonDenines.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}