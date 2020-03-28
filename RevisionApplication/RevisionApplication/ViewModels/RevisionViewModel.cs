using RevisionApplication.Models;

namespace RevisionApplication.ViewModels
{
    public class RevisionViewModel
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public string ChosenAnswer { get; set; }
        public int currentRecord { get; set; }
    }
}
