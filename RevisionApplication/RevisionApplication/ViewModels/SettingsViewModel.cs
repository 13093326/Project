using Microsoft.AspNetCore.Mvc.Rendering;
using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.ViewModels
{
    public class SettingsViewModel
    {
        public string Title { get; set; }
        public List<UnitProperties> Units { get; set; }
    }
}
