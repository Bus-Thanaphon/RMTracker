using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMTracker.Core.Models;
using RMTracker.Core.Contracts;
using RMTracker.Core.ViewModels;

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

        public ActionResult Createsubc2b(string Id)
        {
            User_Works viewc2bedit = context.Find(Id);
            if (viewc2bedit == null)
            {
                return HttpNotFound();
            }
            else
            {
                RMC2BView viewModel = new RMC2BView();
                viewModel.Sub_C2B_V = new Sub_C2B();
                return View(viewModel);
            }
            //RMC2BView viewModel = new RMC2BView();
            //viewModel.Sub_C2B_V = new Sub_C2B();
            //return View(viewModel);
        }
        [HttpPost]
        public ActionResult Createsubc2b(Sub_C2B subc2bs)
        {
            
                if (!ModelState.IsValid)
                {
                    return View(subc2bs);
                }

                C2Bcontext.Insert(subc2bs);
                TempData["dataformuser"] = subc2bs; 
                //C2Bcontext.Commit();
                
                return RedirectToAction("Index", "SubC2B");
            
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