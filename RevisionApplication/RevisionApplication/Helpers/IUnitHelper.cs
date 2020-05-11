using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface IUnitHelper
    {
        List<Unit> GetAllUnits();
        Unit UpdateUnit(Unit unit);
        bool AddUnit(Unit unit);
    }
}
