namespace RevisionApplication.Models
{
    public class ReportQuestionCoverage
    {
        public string UnitName { get; set; }
        public int TotalQuestionCount { get; set; }
        public int RatedQuestionCount { get; set; }
        public double Percentage { get; set; }
    }
}
