using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
