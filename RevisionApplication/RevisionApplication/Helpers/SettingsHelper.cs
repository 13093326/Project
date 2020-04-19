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

        public SettingsHelper(IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        // Get list of all units. 
        public List<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList();
        }

        // Update the selected units for the current user. 
        public void UpdateSelectedUnits(string userName, List<UnitProperties> selectedUnits)
        {
            // Get the current user settings and update. 
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);
            currentUserSettings.SelectedUnits = string.Join(",", selectedUnits.Where(x => x.isSelected).Select(x => x.Id));
            _userSettingsRepository.UpdateSettings(currentUserSettings);
        }
    }
}
