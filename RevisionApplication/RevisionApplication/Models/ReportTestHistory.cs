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
        [DisplayName("Score")]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Score { get; set; }
    }
}
