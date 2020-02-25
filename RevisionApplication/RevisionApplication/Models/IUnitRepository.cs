using System.Collections.Generic;

namespace RevisionApplication.Models
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetAllUnits();
        bool AddUnit(Unit unit);
        string GetAllUnitIds();
    }
}
