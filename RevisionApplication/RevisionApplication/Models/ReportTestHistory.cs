using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionApplication.Models
{
    public class ReportTestHistory
    {
        public int Id { get; set; }
        [DisplayName("Date Taken")]
        public DateTime DateTaken { get; set; }
        [DisplayName("Correct Count")]
        public int Correct { get; set; }
        [DisplayName("Total Count")]
        public int Total { get; set; }
        [DisplayName("Percentage")]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Score { get; set; }
    }
}
