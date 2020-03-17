using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RevisionApplication.Models
{
    public class Unit
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Unit Name")]
        public string Name { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
