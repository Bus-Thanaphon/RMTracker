using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class UserListController : Controller
    {
        // GET: UserList
        IRepository<UserList> UserLists;
        IRepository<Departmentuser> Departmentusers;
        public UserListController(IRepository<UserList> userlcon, IRepository<Departmentuser> departmentusercon)
        {
            UserLists = userlcon;
            Departmentusers = departmentusercon;
        }
        public ActionResult Index()
        {
            List<UserList> userlists = UserLists.Collection().OrderByDescending(o => o.CreateAt).ToList();
            return View(userlists);
        }
        public ActionResult Create()
        {
            UserList userlC = new UserList();
            userlC.Department = Departmentusers.Collection();
            return View(userlC);
        }
        [HttpPost]
        public ActionResult Create(UserList userl)
        {
            if (!ModelState.IsValid)
            {
                return View(userl);
            }
            else
            {
                UserLists.Insert(userl);
                UserLists.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            UserList uToDelete = UserLists.Find(Id);
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
            UserList uToDelete = UserLists.Find(Id);
            if (uToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                UserLists.Delete(Id);
                UserLists.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}