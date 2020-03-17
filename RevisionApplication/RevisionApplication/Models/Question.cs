using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RevisionApplication.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Question")]
        public string Content { get; set; }
        [Required]
        [DisplayName("Answer 1")]
        public string Answer1 { get; set; }
        [Required]
        [DisplayName("Answer 2")]
        public string Answer2 { get; set; }
        [Required]
        [DisplayName("Answer 3")]
        public string Answer3 { get; set; }
        [Required]
        [DisplayName("Answer 4")]
        public string Answer4 { get; set; }
        [Required]
        [DisplayName("Correct Answer")]
        public int CorrectAnswer { get; set; }
        [Required]
        [DisplayName("Reference Information")]
        public string Reference { get; set; }
        [Required]
        [DisplayName("Unit")]
        public int UnitId { get; set; }
        [DisplayName("Unit")]
        public Unit Unit { get; set; }
    }
}
