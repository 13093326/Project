using RevisionApplication.Models;

namespace RevisionApplication.ViewModels
{
    public class TestViewModel
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public int ChosenAnswer { get; set; }
        public int CurrentRecord { get; set; }
    }
}
