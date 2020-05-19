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
            return _appDbContext.Unit;
        }

        public Unit UpdateUnit(Unit unit)
        {
            _appDbContext.Unit.Update(unit);
            _appDbContext.SaveChanges();

            return unit;
        }

        public Unit GetUnitByName(string name)
        {
            return _appDbContext.Unit.FirstOrDefault(u => u.Name.Equals(name));
        }

        public Unit GetUnitById(int Id)
        {
            return _appDbContext.Unit.FirstOrDefault(u => u.Id == Id);
        }

        public bool AddUnit(Unit unit)
        {
            _appDbContext.Add(unit);
            _appDbContext.SaveChanges();

            return true;
        }

        public int[] GetAllUnitIds()
        {
            return GetAllUnits().Select(u => u.Id).ToArray();
        }
    }
}

