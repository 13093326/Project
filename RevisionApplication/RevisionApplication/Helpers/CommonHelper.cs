using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;

using RevisionApplication.ViewModels;


namespace RevisionApplication.Helpers
{
    public  class CommonHelper : ICommonHelper
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IUnitRepository _unitRepository;

        public CommonHelper(IQuestionRepository questionRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        public string GetUserSettingsOrCreate(string userName)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            if (currentUserSettings is null)
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();

                currentUserSettings = _userSettingsRepository.AddSettings(new UserSetting { Username = userName, SelectedUnits = allUnitsIds });
            }

            return currentUserSettings.SelectedUnits;
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

        public Question GetRandomQuestionFromUnits(string userName, int record)
        {
            // Get user selected units 
            var units = GetUserSelectedUnits(userName);

            // Get questions in random order for units that is not the record provided if it should not match that last retrieved question 
            Random random = new Random();
            var allValidQuestionIds = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit) && p.Id != record).OrderBy(x => random.Next()).Select(p => p.Id);

            // Make a random sleection to prevent bias towards low or high numbers 
            var index = allValidQuestionIds.ElementAt(random.Next(0, allValidQuestionIds.Count() - 1));

            return _questionRepository.GetQuestionById(index);
        }
    }
}
