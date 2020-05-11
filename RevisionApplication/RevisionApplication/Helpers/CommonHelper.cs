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
        private readonly IUnitSelectionRepository _unitSelectionRepository;

        public CommonHelper(IRoleRepository roleRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository, IUnitSelectionRepository unitSelectionRepository)
        {
            _roleRepository = roleRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
            _unitSelectionRepository = unitSelectionRepository; 
        }

        // Get a list of selected unit for the currently logged in user. 
        public List<UnitSelection> GetSelectedUnitsList(string userName)
        {
            // Get the user settings. 
            var settings = _userSettingsRepository.GetSettingsByUserName(userName); 

            // Handle no settings. 
            if (settings is null)
            {
                return null;
            }

            // Return unit selections for the setting id. 
            return _unitSelectionRepository.GetSelectionById(settings.Id).ToList();
        }

        // Get a list of units with selection properties for the user. 
        public List<UnitProperties> GetSelectedUnitsProperteisList(string userName)
        {
            // Get all units 
            var allUnits = _unitRepository.GetAllUnits(); 

            // Get list of selected unit ids. 
            var currentUnitSelection = GetSelectedUnitsList(userName).Select(u => u.SelectedUnitId).ToList();

            List<UnitProperties> properties = new List<UnitProperties>();

            // Generate list of properties and update with user selection 
            foreach (var unit in allUnits)
            {
                // Set the selected units in the list of units. 
                properties.Add(new UnitProperties { Id = unit.Id, Name = unit.Name, isSelected = (currentUnitSelection.Contains(unit.Id)? true : false) });
            }

            // Return list of units with current user selection. 
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

        // Get comma seperated list of the user unit settings or return default if not set yet. 
        public string GetUserSettingsOrCreate(string userName)
        {
            // Get settings for Id. 
            var currentUserSettings = GetSelectedUnitsList(userName);

            // If no settings found update user to default settings. 
            if (currentUserSettings is null)
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();
                var userSettings = _userSettingsRepository.AddSettings(new UserSetting { UserName = userName });
                _unitSelectionRepository.AddSettings(userSettings.Id, allUnitsIds);
                currentUserSettings = GetSelectedUnitsList(userName);
            }

            // Get unit selection for setting id 
            var unitIdList = currentUserSettings.Select(u => u.SelectedUnitId).ToArray();

            // Return as a comma seperated list of unit names. 
            return string.Join(", ", _unitRepository.GetAllUnits().Where(u => unitIdList.Contains(u.Id)).Select(u => u.Name));
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
            var selectedUnits = _unitSelectionRepository.GetSelectionById(currentUserSettings.Id).Select(u => u.SelectedUnitId).ToList();

            // Get the id of the units 
            var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));

            return units;
        }
    }
}
