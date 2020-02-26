using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.ViewModels
{
    public class TestViewModel
    {
        public string Title { get; set; }
        public Question Question { get; set; }
        public int ChosenAnswer { get; set; }
        public int currentRecord { get; set; }
    }
}
