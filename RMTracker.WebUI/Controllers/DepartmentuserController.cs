using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class DepartmentuserController : Controller
    {
        // GET: Departmentuser
        IRepository<Departmentuser> Departmentusers;

        public DepartmentuserController(IRepository<Departmentuser> Departmentcontext)
        {
            Departmentusers = Departmentcontext;
        }
        public ActionResult Index()
        {
            List<Departmentuser> department = Departmentusers.Collection().OrderBy(o => o.Number).ToList();
            return View(department);
        }
        public ActionResult Create(int no = 0)
        {
            Departmentuser departmentu = new Departmentuser();
            var lastNo = Departmentusers.Collection().OrderByDescending(c => c.Number).FirstOrDefault();
            if (no != 0)
            {
                departmentu = Departmentusers.Collection().Where(x => x.Number == no).FirstOrDefault<Departmentuser>();
            }
            else if (lastNo == null)
            {
                departmentu.Number = 1;
            }
            else
            {
                departmentu.Number = lastNo.Number + 1;
            }
            return View(departmentu);
        }
        [HttpPost]
        public ActionResult Create(Departmentuser departmentuser)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentuser);
            }
            else
            {
                Departmentusers.Insert(departmentuser);
                Departmentusers.Commit();

                return RedirectToAction("Index");
            }
        }
    }
}