using RevisionApplication.Models;
using RevisionApplication.Repository;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class SettingsHelper : ISettingsHelper
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IUnitSelectionRepository _unitSelectionRepository;

        public SettingsHelper(IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository, IUnitSelectionRepository unitSelectionRepository)
        {
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
            _unitSelectionRepository = unitSelectionRepository;
        }

        // Get list of all units. 
        public List<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList();
        }

        // Update the selected units for the current user. 
        public void UpdateSelectedUnits(string userName, List<UnitProperties> units)
        {
            // Get the current user settings. 
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            // Get selected units. 
            var selectedUnits = units.Where(x => x.IsSelected).Select(x => x.Id).ToArray();

            // Update selected units. 
            _unitSelectionRepository.UpdateSelection(currentUserSettings.Id, selectedUnits); 
        }
    }
}
