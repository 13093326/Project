using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetAllUnits();
        bool AddUnit(Unit unit);
    }
}
