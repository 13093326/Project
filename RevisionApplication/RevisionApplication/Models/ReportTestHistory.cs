using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionApplication.Models
{
    public class ReportTestHistory
    {
        public int Id { get; set; }
        public DateTime DateTaken { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Score { get; set; }
    }
}
