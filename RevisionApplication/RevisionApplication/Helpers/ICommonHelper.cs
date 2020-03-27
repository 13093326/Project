using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ICommonHelper
    {
        IEnumerable<Unit> GetUserSelectedUnits(string userName);
        string GetUserSettingsOrCreate(string userName);
        int[] GetSelectedUnitsIdList(string userName);
        void UpdateSelectedUnits(string userName, int[] selectedUnits);
        List<string> GetUnitNames();
        bool isUserRoleAdmin(string userName);
    }
}
