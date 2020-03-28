using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public  class CommonHelper : ICommonHelper
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public CommonHelper(IRoleRepository roleRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _roleRepository = roleRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        public int[] GetSelectedUnitsIdList(string userName)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            if (currentUserSettings is null)
            {
                return null;
            }

            return currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToArray();
        }

        public bool isUserRoleAdmin(string userName)
        {
            return _roleRepository.isUserAdmin(userName);
        }

        public string GetUserSettingsOrCreate(string userName)
        {
            var currentUserSettings = GetSelectedUnitsIdList(userName);

            if (currentUserSettings is null)
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();

                _userSettingsRepository.AddSettings(new UserSetting { Username = userName, SelectedUnits = allUnitsIds });

                currentUserSettings = GetSelectedUnitsIdList(userName);
            }

            return string.Join(", ", _unitRepository.GetAllUnits().Where(u => currentUserSettings.Contains(u.Id)).Select(u => u.Name));
        }

        public List<string> GetUnitNames()
        {
            return _unitRepository.GetAllUnits().Select(u => u.Name).ToList();
        }

        public IEnumerable<Unit> GetUserSelectedUnits(string userName)
        {
            // Get the current user settings 
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);
            var selectedUnits = currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToList();

            // Get the id of the units 
            var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));

            return units;
        }
    }
}
