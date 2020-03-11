using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.ViewModels
{
    public class ReportViewModel
    {
        public string Title { get; set; }
        public List<ReportQuestionCoverage> QuestionCoverage { get; set; }
        public List<ReportUnitRating> UnitRating { get; set; }
        public List<ReportTestHistory> TestHistory { get; set; }

        public ReportViewModel()
        {
            QuestionCoverage = new List<ReportQuestionCoverage>();
            UnitRating = new List<ReportUnitRating>();
            TestHistory = new List<ReportTestHistory>();
        }
    }
}
