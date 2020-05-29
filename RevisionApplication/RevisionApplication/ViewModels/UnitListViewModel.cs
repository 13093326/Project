using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.ViewModels
{
    public class UnitListViewModel
    {
        public string Title { get; set; }
        public List<Unit> Units { get; set; }
    }
}
