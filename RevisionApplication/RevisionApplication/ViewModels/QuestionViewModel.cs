using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.ViewModels
{
    public class QuestionViewModel
    {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public int chosenAnswer { get; set; }
    }
}
