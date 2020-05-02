using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMTracker.Core.Models;
using RMTracker.Core.Contracts;
using RMTracker.Core.ViewModels;
using System.Drawing;

namespace RMTracker.WebUI.Controllers
{
    public class UserWorksController : Controller
    {
        IRepository<User_Works> context;
        IRepository<Sub_C2B> C2Bcontext;

        public UserWorksController(IRepository<User_Works> UserWorksContext, IRepository<Sub_C2B> C2BContext)
        {
            context = UserWorksContext;
            C2Bcontext = C2BContext;
        }
        // GET: UserWorks
        public ActionResult Index()
        {
            List<User_Works> userworks = context.Collection().ToList();
            var userworksId = userworks.Select(o => o.C2BNo).ToList();

            var subindex = C2Bcontext.Collection().Where(o => userworksId.Contains(o.C2BNo)).ToList();

            foreach (var i in userworks) 
            {
                i.subindex = subindex.Where(o => o.C2BNo == i.C2BNo).ToList();
            }
            return View(userworks);
            //var viewmodel = new User_Works();
            //foreach (var item in C2Bcontext.Collection().ToList())
            //{
            //    int id = Int32.Parse(item.C2BNo);

            //    //SystemsCount 
            //    int count = C2Bcontext.Collection().Where(x => Int32.Parse(x.C2BNo) == id).Count();
            //    item.countno = count;
            //}
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
                userwToEdit.SubC2B = userw.SubC2B;
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