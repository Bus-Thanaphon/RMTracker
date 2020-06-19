using RMTracker.Core.Contracts;
using RMTracker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMTracker.WebUI.Controllers
{
    public class WorksPauseController : Controller
    {
        // GET: WorksPause
        IRepository<s_Lamination> SLaminations;
        IRepository<s_Cut> SCuts;
        IRepository<s_Edgebanding> SEdgebandings;
        IRepository<s_Drill> SDrills;
        IRepository<s_Painting> SPaintings;
        IRepository<s_Cleaning> SCleanings;
        IRepository<s_Packing> SPackings;
        IRepository<s_QC> SQCs;
        IRepository<s_Pickup> SPickups;
        IRepository<WorksPause> WorksPauses;
        public WorksPauseController(IRepository<s_Lamination> laminationcontext, IRepository<Sub_C2B> SubC2Bcontext, IRepository<s_Cut> cutcontext, IRepository<s_Edgebanding> edgebandingcontext,
            IRepository<s_Drill> drillcontext, IRepository<s_Painting> paintingcontext, IRepository<s_Cleaning> cleaningcontext
            , IRepository<s_Packing> packingcontext, IRepository<s_QC> qccontext, IRepository<s_Pickup> pickupcontext, IRepository<UserList> userlcon, IRepository<WorksPause> workpausecontext)
        {
            SLaminations = laminationcontext;
            SCuts = cutcontext;
            SEdgebandings = edgebandingcontext;
            SDrills = drillcontext;
            SPaintings = paintingcontext;
            SCleanings = cleaningcontext;
            SPackings = packingcontext;
            SQCs = qccontext;
            SPickups = pickupcontext;
            WorksPauses = workpausecontext;
        }
        public ActionResult Index(string Id)
        {
            List<WorksPause> workpause = WorksPauses.Collection().Where(o => o.StationID == Id).ToList();
            var workpauseID = workpause.Select(o => o.StationID).ToList();
            return View(workpause);
        }
        public ActionResult Confirm(string Id, WorksPause wpc)
        {
            WorksPause workpauseToEdit = WorksPauses.Find(Id);
            if (workpauseToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(workpauseToEdit);
            }
        }
        [HttpPost]
        public ActionResult Confirm(WorksPause wpc, string Id)
        {
            WorksPause wpToEdit = WorksPauses.Find(Id);
            if (wpToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(wpc);
                }
                wpToEdit.SONO = wpc.SONO;
                wpToEdit.Start_Time = wpc.Start_Time;
                wpToEdit.StationID = wpc.StationID;
                wpToEdit.Station = wpc.Station;
                WorksPauses.Commit();

                return RedirectToAction("Index", wpc.Station);
            }
        }
        
        public ActionResult Back(string Id, WorksPause wpc,string textback,string IDback)
        {
            WorksPause workpauseToBack = WorksPauses.Find(Id);
            if (workpauseToBack == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(wpc);
                }
                textback = workpauseToBack.Station;
                IDback = workpauseToBack.StationID;
                WorksPauses.Delete(Id);
                WorksPauses.Commit();
                return RedirectToAction("Back", textback, new { id = IDback });
            }
        }
    }
}