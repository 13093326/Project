using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
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