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

        // Get list of all units. 
        public List<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits().ToList();
        }

        // Update unit. 
        public Unit UpdateUnit(Unit unit)
        {
            return _unitRepository.UpdateUnit(unit);
        }

        // Add unit. 
        public bool AddUnit(Unit unit)
        {
            return _unitRepository.AddUnit(unit);
        }
    }
}
