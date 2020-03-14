using RevisionApplication.Models;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public interface IReportHelper
    {
        IOrderedEnumerable<ReportQuestionCoverage> questionCoverageReport(string userName);
        IOrderedEnumerable<ReportUnitRating> GetUnitRatingReport(string userName);
        IOrderedEnumerable<ReportTestHistory> GetTestHistoryReport(string userName);
    }
}
