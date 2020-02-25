using Microsoft.AspNetCore.Mvc.Rendering;
using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.ViewModels
{
    public class SettingsViewModel
    {
        public string Title { get; set; }
        public List<Unit> Units { get; set; }
        public int[] SelectedUnitIds { get; set; }

        public IEnumerable<SelectListItem> GetUnitsAsSelectListItems()
        {
            return new SelectList(Units, "Id", "Name");
        }
    }
}
