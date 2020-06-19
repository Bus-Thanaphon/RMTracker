using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class MachineListController : Controller
    {
        // GET: MachineList
        IRepository<MachineList> MachineLists;
        public MachineListController(IRepository<MachineList> mccon)
        {
            MachineLists = mccon;
        }
        public ActionResult Index()
        {
            List<MachineList> machinlists = MachineLists.Collection().OrderByDescending(o => o.CreateAt).ToList();
            return View(machinlists);
        }
        public ActionResult Create()
        {
            MachineList machinelC = new MachineList();
            return View(machinelC);
        }
        [HttpPost]
        public ActionResult Create(MachineList ml)
        {
            if (!ModelState.IsValid)
            {
                return View(ml);
            }
            else
            {
                MachineLists.Insert(ml);
                MachineLists.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            MachineList userwToEdit = MachineLists.Find(Id);
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
        public ActionResult Edit(MachineList userw, string Id)
        {
            MachineList userwToEdit = MachineLists.Find(Id);

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
                userwToEdit.Name = userw.Name;
                userwToEdit.NickName = userw.NickName;
                userwToEdit.ID_Machine = userw.ID_Machine;
                userwToEdit.Department = userw.Department;

                MachineLists.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Delete(string Id)
        {
            MachineList uToDelete = MachineLists.Find(Id);
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
            MachineList uToDelete = MachineLists.Find(Id);
            if (uToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                MachineLists.Delete(Id);
                MachineLists.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}