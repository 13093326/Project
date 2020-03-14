namespace RevisionApplication.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public int QuestionId { get; set; }
        public int TestSetId { get; set; }
        public TestSet TestSet { get; set; }
        public Question Questions { get; set; }
    }
}
