using RevisionApplication.Models;
using RevisionApplication.Repository;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class SettingsHelper : ISettingsHelper
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public SettingsHelper(ICommonHelper commonHelper, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _commonHelper = commonHelper;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        public List<Unit> GetAllUnits()
        {
            return _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList();
        }

        public void UpdateSelectedUnits(string userName, int[] selectedUnits)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);
            currentUserSettings.SelectedUnits = string.Join(",", selectedUnits);
            _userSettingsRepository.UpdateSettings(currentUserSettings);
        }
    }
}
