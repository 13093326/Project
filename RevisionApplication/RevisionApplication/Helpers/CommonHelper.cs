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
            // Get the user settings. 
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            // Handle no settings. 
            if (currentUserSettings is null)
            {
                return null;
            }

            // Return unit settings split in to an array. 
            return currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToArray();
        }

        // Get a list of unit properties for the user. 
        public List<UnitProperties> GetSelectedUnitsProperteisList(string userName)
        {
            // Get all units 
            var allUnits = _unitRepository.GetAllUnits(); 

            // Get the user settings. 
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            // Handle no settings. 
            if (currentUserSettings is null)
            {
                return null;
            }

            // Split user settings in to list 
            var currentUserSettingIds = currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToList();

            List<UnitProperties> properties = new List<UnitProperties>();

            // Generate list of properties and update with user selection 
            foreach (var unit in allUnits)
            {
                properties.Add(new UnitProperties { Id = unit.Id, Name = unit.Name, isSelected = (currentUserSettingIds.Contains(unit.Id)? true : false) });
            }

            // Return unit settings split in to an array. 
            return properties; 
        }

        // Return true if the currently logged in user is an admin. 
        public bool IsUserRoleAdmin(string userName)
        {
            // Get user role. 
            var role = _roleRepository.GetAdminRoleForUser(userName);

            // Check and return result. 
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
