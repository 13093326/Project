using System.ComponentModel;

namespace RevisionApplication.Models
{
    public class Question
    {
        public int Id { get; set; }
        [DisplayName("Question")]
        public string Content { get; set; }
        [DisplayName("Answer 1")]
        public string Answer1 { get; set; }
        [DisplayName("Answer 2")]
        public string Answer2 { get; set; }
        [DisplayName("Answer 3")]
        public string Answer3 { get; set; }
        [DisplayName("Answer 4")]
        public string Answer4 { get; set; }
        [DisplayName("Correct Answer")]
        public int CorrectAnswer { get; set; }
        [DisplayName("Reference Information")]
        public string Reference { get; set; }

        
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
    }
}
