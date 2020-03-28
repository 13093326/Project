using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface IUnitHelper
    {
        List<Unit> GetAllUnits();
        Unit GetUnitById(int Id);
        Unit UpdateUnit(Unit unit);
        bool AddUnit(Unit unit);
    }
}
