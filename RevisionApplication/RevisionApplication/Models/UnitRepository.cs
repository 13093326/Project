using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class UnitRepository : IUnitRepository
    {

        private readonly AppDbContext _appDbContext;

        public UnitRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            return _appDbContext.Units;
        }

        public bool AddUnit(Unit unit)
        {
            _appDbContext.Add
                   (
                       new Unit
                       {
                           Name = unit.Name
                       }
                   );

            _appDbContext.SaveChanges();

            return true;
        }
    }
}

