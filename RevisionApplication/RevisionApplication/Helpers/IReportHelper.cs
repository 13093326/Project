using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Helpers
{
    public interface IReportHelper
    {
        IOrderedEnumerable<ReportQuestionCoverage> questionCoverageReport(string userName);
        IOrderedEnumerable<ReportUnitRating> GetUnitRatingReport(string userName);
        IOrderedEnumerable<ReportTestHistory> GetTestHistoryReport(string userName);
    }
}
