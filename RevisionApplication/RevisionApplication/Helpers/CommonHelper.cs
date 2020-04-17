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

        // Get a list of selected unit id's for the currently logged in user. 
        public int[] GetSelectedUnitsIdList(string userName)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            if (currentUserSettings is null)
            {
                return null;
            }

            return currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToArray();
        }

        // Return true if the currently logged in user is an admin. 
        public bool IsUserRoleAdmin(string userName)
        {
            var role = _roleRepository.GetAdminRoleForUser(userName);

            if (role != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Get the user unit settings or return default if not set yet. 
        public string GetUserSettingsOrCreate(string userName)
        {
            var currentUserSettings = GetSelectedUnitsIdList(userName);

            // If no settings found update user to default settings. 
            if (currentUserSettings is null)
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();

                _userSettingsRepository.AddSettings(new UserSetting { UserName = userName, SelectedUnits = allUnitsIds });

                currentUserSettings = GetSelectedUnitsIdList(userName);
            }

            // Return as a comma seperated list of unit names. 
            return string.Join(", ", _unitRepository.GetAllUnits().Where(u => currentUserSettings.Contains(u.Id)).Select(u => u.Name));
        }

        // Get a list of unit names. 
        public List<string> GetUnitNames()
        {
            return _unitRepository.GetAllUnits().Select(u => u.Name).ToList();
        }

        // Get a list of selected units for the currently logged in user. 
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
