using System.ComponentModel;

namespace RevisionApplication.Models
{
    public class ReportUnitRating
    {
        [DisplayName("Unit")]
        public string UnitName { get; set; }
        [DisplayName("Average Rating")]
        public double AverageRating { get; set; }
    }
}
