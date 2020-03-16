using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
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

        public Unit GetUnitByName(string name)
        {
            return _appDbContext.Units.FirstOrDefault(u => u.Name.Equals(name));
        }

        public Unit GetUnitById(int Id)
        {
            return _appDbContext.Units.FirstOrDefault(u => u.Id == Id);
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

        public string GetAllUnitIds()
        {
            return string.Join(",", GetAllUnits().Select(item => item.Id));
        }
    }
}

