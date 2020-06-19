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
    public class WorksDenineController : Controller
    {
        // GET: WorksDenine
        IRepository<Sub_C2B> Subc2bs;
        IRepository<s_Lamination> SLaminations;
        IRepository<s_Cut> SCuts;
        IRepository<s_Edgebanding> SEdgebandings;
        IRepository<s_Drill> SDrills;
        IRepository<s_Painting> SPaintings;
        IRepository<s_Cleaning> SCleanings;
        IRepository<s_Packing> SPackings;
        IRepository<s_QC> SQCs;
        IRepository<s_Pickup> SPickups;
        IRepository<WorksDenine> WorksDenines;
        public WorksDenineController(IRepository<s_Lamination> laminationcontext, IRepository<Sub_C2B> SubC2Bcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<UserList> userlcon, IRepository<WorksDenine> workdeninecontext)
        {
            Subc2bs = SubC2Bcontext;
            SLaminations = laminationcontext;
            SCuts = cutcontext;
            SEdgebandings = edgebandingcontext;
            SDrills = drillcontext;
            SPaintings = paintingcontext;
            SCleanings = cleaningcontext;
            SPackings = packingcontext;
            SQCs = qccontext;
            SPickups = pickupcontext;
            WorksDenines = workdeninecontext;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Denine(string Id)
        {
            WorksDenine workD = WorksDenines.Find(Id);
            if (workD == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(workD);
            }
        }
        [HttpPost]
        public ActionResult Denine(string Id,WorksDenine workd)
        {
            WorksDenine workD = WorksDenines.Find(Id);
            if (workD == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(workd);
                }
                workD.Reason = workd.Reason;
                workD.CheckBoxDetail = workd.CheckBoxDetail;
                WorksDenines.Commit();

                return RedirectToAction("DetailCheck", workD.Station, new { id = workD.StationID });
            }
        }

        public ActionResult Back(string Id, WorksDenine wdu, string textback, string IDback)
        {
            WorksDenine workdToBack = WorksDenines.Find(Id);
            if (WorksDenines == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(wdu);
                }
                textback = workdToBack.Station;
                IDback = workdToBack.StationID;
                WorksDenines.Delete(Id);
                WorksDenines.Commit();
                return RedirectToAction("DenineCheck", textback, new { Id = IDback });
            }
        }
    }
}