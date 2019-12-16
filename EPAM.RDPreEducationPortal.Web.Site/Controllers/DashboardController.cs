using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        // GET: Dashboard
        public ActionResult RecruitmentSummary(int recruitmentId)
        {
            DashboardModel model = new DashboardModel();
            return View("_recruitmentSummary", model.RecruitmentSummaryList(recruitmentId));
        }
        public ActionResult GenderwiseSummary(int recruitmentId)
        {
            DashboardModel model = new DashboardModel();
            return View("_genderwiseSummary", model);
        }
        public ActionResult InterviewPanelwiseSummary(int recruitmentId, string role)
        {
            DashboardModel model = new DashboardModel();
            return View("_interviewPanelwiseSummary", model.InterviewPanelwiseSummaryList(recruitmentId, role));
        }
        public ActionResult RecruitmentSummaryCollegeWise(int recruitmentId)
        {
            DashboardModel model = new DashboardModel();
            return View("_recruitmentSummaryCollegeWise", model.RecruitmentSummaryCollegeWise(recruitmentId));
        }
    }

}