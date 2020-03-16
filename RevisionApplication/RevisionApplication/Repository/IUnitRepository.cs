using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetAllUnits();
        bool AddUnit(Unit unit);
        string GetAllUnitIds();
        Unit GetUnitByName(string name);
        Unit GetUnitById(int Id);
        Unit UpdateUnit(Unit unit);
    }
}
