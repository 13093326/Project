using RevisionApplication.Models;
using System.Collections.Generic;
using System.Linq;

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

        public Unit UpdateUnit(Unit unit)
        {
            _appDbContext.Units.Update(unit);

            _appDbContext.SaveChanges();

            return unit;
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
            _appDbContext.Add(unit);

            _appDbContext.SaveChanges();

            return true;
        }

        public string GetAllUnitIds()
        {
            return string.Join(",", GetAllUnits().Select(item => item.Id));
        }
    }
}

