using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ISettingsHelper
    {
        List<Unit> GetAllUnits();
        void UpdateSelectedUnits(string userName, List<UnitProperties> selectedUnits);
    }
}
