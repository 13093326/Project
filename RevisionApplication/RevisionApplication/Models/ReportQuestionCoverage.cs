using System.ComponentModel;

namespace RevisionApplication.Models
{
    public class ReportQuestionCoverage
    {
        [DisplayName("Unit")]
        public string UnitName { get; set; }
        [DisplayName("Total Questions")]
        public int TotalQuestionCount { get; set; }
        [DisplayName("Revised Questions")]
        public int RatedQuestionCount { get; set; }
        [DisplayName("Percentage")]
        public double Percentage { get; set; }
    }
}
