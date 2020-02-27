using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class TestSet
    {
        public int Id { get; set; }
        public string User { get; set; }
        public bool Complete { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Score { get; set; }
    }
}
