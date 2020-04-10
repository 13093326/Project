using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevisionApplication.Models
{
    public class TestSet
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool Complete { get; set; }
        public DateTime Date { get; set; }
        public int TotalCount { get; set; }
        public int CorrectCount { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal Score { get; set; }

        public ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
