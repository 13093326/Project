using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual TestSet TestSet { get; set; }
    }
}
