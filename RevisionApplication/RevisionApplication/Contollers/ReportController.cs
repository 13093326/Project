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

        // Reports page. 
        public IActionResult Index()
        {
            // Create page model. 
            ReportViewModel model = new ReportViewModel
            {
                Title = "Reports"
            };

            // Set report content. 
            model.QuestionCoverage.AddRange(_reportHelper.questionCoverageReport(User.Identity.Name));
            model.UnitRating.AddRange(_reportHelper.GetUnitRatingReport(User.Identity.Name));
            model.TestHistory.AddRange(_reportHelper.GetTestHistoryReport(User.Identity.Name));

            return View(model);
        }
    }
}