using RevisionApplication.Models;
using RevisionApplication.Repository;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class UnitHelper : IUnitHelper
    {
        private readonly IUnitRepository _unitRepository;

        public UnitHelper(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public List<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits().ToList();
        }

        public Unit GetUnitById(int Id)
        {
            return _unitRepository.GetUnitById(Id);
        }

        public Unit UpdateUnit(Unit unit)
        {
            return _unitRepository.UpdateUnit(unit);
        }

        public bool AddUnit(Unit unit)
        {
            return _unitRepository.AddUnit(unit);
        }
    }
}
