using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.ViewModels
{
    public class QuestionListViewModel
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public int chosenAnswer { get; set; }
    }
}
