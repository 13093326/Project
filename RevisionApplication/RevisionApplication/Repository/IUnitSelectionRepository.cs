using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface IUnitSelectionRepository
    {
        UnitSelection[] GetSelectionById(int Id);
        void AddSettings(int settingId, int[] selectedUnits);
        void UpdateSelection(int settingId, int[] selectedUnits);
    }
}