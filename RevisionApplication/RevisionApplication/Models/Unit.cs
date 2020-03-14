using System.Collections.Generic;

namespace RevisionApplication.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
