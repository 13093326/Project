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

        public CommonHelper(IQuestionRepository questionRepository, IQuestionRatingRepository questionRatingRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
            _questionRatingRepository = questionRatingRepository;
        }

        public int[] GetSelectedUnitsIdList(string userName)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);

            return currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToArray();
        }

        public void UpdateSelectedUnits(string userName, int[] selectedUnits)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(userName);
            currentUserSettings.SelectedUnits = string.Join(",", selectedUnits);
            _userSettingsRepository.UpdateSettings(currentUserSettings);
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

        public void UpdateOrInsertRating(string userName, int questionId, bool isCorrect)
        {
            // Update rating 
            var ratingUpdated = UpdateRating(userName, questionId, isCorrect);
            
            // Check if there was a rating to update 
            if (ratingUpdated == false)
            {
                // Insert rating 
                AddRating(userName, questionId, isCorrect);
            }
        }

        private void AddRating(string userName, int questionId, bool isCorrect)
        {
            QuestionRating newRating = new QuestionRating()
            { 
                QuestionId = questionId,
                Rating = (isCorrect) ? 1 : 0,
                UserName = userName,
                Time = DateTime.Now
            };

            _questionRatingRepository.AddRating(newRating);
        }

        private bool UpdateRating(string userName, int questionId, bool isCorrect)
        {
            var rating = _questionRatingRepository.GetAllRatings().Where(x => x.QuestionId == questionId && x.UserName.Equals(userName)).FirstOrDefault();

            if (rating != null)
            {
                if (isCorrect)
                {
                    if (rating.Rating < 10)
                    {
                        rating.Rating++;
                    }
                }
                else
                {
                    if (rating.Rating > 1)
                    {
                        rating.Rating--;
                    }
                }

                rating.Time = DateTime.Now;
                _questionRatingRepository.UpdateRating(rating);

                return true; 
            }

            return false; 
        }

        public Question GetMultipleChoiceQuestionBasedOnRating(string userName)
        {
            var units = GetUserSelectedUnits(userName);

            // Find new question 
            var question = _questionRepository.GetAllQuestions().Where(q => !_questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.Unit)).Select(r => r.QuestionId).Contains(q.Id)).FirstOrDefault();

            if (question is null)
            {
                // Find lowest rated question id 
                var nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.Unit) && r.Time < DateTime.Now.AddHours(-1) && r.Rating < 6).OrderBy(r => r.Time).FirstOrDefault();

                if (nextQuestionId is null)
                {
                    // Get oldest rated question id  
                    nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.Unit)).OrderBy(r => r.Time).FirstOrDefault();
                }

                // Get the question 
                question = _questionRepository.GetQuestionById(nextQuestionId.QuestionId);
            }

            return question;
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
