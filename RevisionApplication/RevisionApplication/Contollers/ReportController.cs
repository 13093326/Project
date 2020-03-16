using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IReportHelper _reportHelper;

        public ReportController(IReportHelper reportHelper)
        {
            _reportHelper = reportHelper;
        }

        public IActionResult Index()
        {
            ReportViewModel model = new ReportViewModel();

            model.QuestionCoverage.AddRange(_reportHelper.questionCoverageReport(User.Identity.Name));
            model.UnitRating.AddRange(_reportHelper.GetUnitRatingReport(User.Identity.Name));
            model.TestHistory.AddRange(_reportHelper.GetTestHistoryReport(User.Identity.Name));

            return View(model);
        }
    }
}