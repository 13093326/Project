using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public  class CommonHelper : ICommonHelper
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionRatingRepository _questionRatingRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IRoleRepository _roleRepository;

        public CommonHelper(IQuestionRepository questionRepository, IQuestionRatingRepository questionRatingRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository, IRoleRepository roleRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
            _questionRatingRepository = questionRatingRepository;
            _roleRepository = roleRepository;
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

        public void UpdateSelectedUnits(string userName, int[] selectedUnits)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);
            currentUserSettings.SelectedUnits = string.Join(",", selectedUnits);
            _userSettingsRepository.UpdateSettings(currentUserSettings);
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
