using RevisionApplication.Models;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public interface IReportHelper
    {
        IOrderedEnumerable<ReportQuestionCoverage> QuestionCoverageReport(string userName);
        IOrderedEnumerable<ReportUnitRating> GetUnitRatingReport(string userName);
        IOrderedEnumerable<ReportTestHistory> GetTestHistoryReport(string userName);
    }
}
