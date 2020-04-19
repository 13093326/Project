using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ICommonHelper
    {
        IEnumerable<Unit> GetUserSelectedUnits(string userName);
        string GetUserSettingsOrCreate(string userName);
        int[] GetSelectedUnitsIdList(string userName);
        List<UnitProperties> GetSelectedUnitsProperteisList(string userName);
        List<string> GetUnitNames();
        bool IsUserRoleAdmin(string userName);
    }
}
