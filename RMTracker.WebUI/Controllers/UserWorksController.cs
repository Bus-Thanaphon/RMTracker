using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMTracker.Core.Models;
using RMTracker.Core.Contracts;

namespace RMTracker.WebUI.Controllers
{
    public class UserWorksController : Controller
    {
        IRepository<User_Works> context;

        public UserWorksController(IRepository<User_Works> UserWorksContext)
        {
            context = UserWorksContext;
        }
        // GET: UserWorks
        public ActionResult Index()
        {
            List<User_Works> userworks = context.Collection().ToList();
            return View(userworks);
        }

        public ActionResult Create()
        {
            User_Works userW = new User_Works();
            return View(userW);
        }
        [HttpPost]
        public ActionResult Create(User_Works userw)
        {
            if (!ModelState.IsValid)
            {
                return View(userw);
            }
            else
            {
                context.Insert(userw);
                context.Commit();

                return RedirectToAction("Index");
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
                if (!ModelState.IsValid)
                {
                    return View(userw);
                }
                userwToEdit.C2BNo = userw.C2BNo;
                userwToEdit.EndDate = userw.EndDate;
                userwToEdit.Current_Station = userw.Current_Station;
                userwToEdit.Job_Status = userw.Job_Status;
                userwToEdit.Order_Status = userw.Order_Status;
                userwToEdit.Comment = userw.Comment;

                context.Commit();

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
        public ActionResult ConfirmDelete(User_Works userw, string Id)
        {
            User_Works userwToDelete = context.Find(Id);
            if (userwToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}