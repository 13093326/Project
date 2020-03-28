using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ICommonHelper
    {
        IEnumerable<Unit> GetUserSelectedUnits(string userName);
        string GetUserSettingsOrCreate(string userName);
        int[] GetSelectedUnitsIdList(string userName);
        List<string> GetUnitNames();
        bool isUserRoleAdmin(string userName);
    }
}
