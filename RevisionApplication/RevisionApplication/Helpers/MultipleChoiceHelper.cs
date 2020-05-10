using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class MultipleChoiceHelper : IMultipleChoiceHelper
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IQuestionRatingRepository _questionRatingRepository;
        private readonly IQuestionRepository _questionRepository;

        public MultipleChoiceHelper(ICommonHelper commonHelper, IQuestionRatingRepository questionRatingRepository, IQuestionRepository questionRepository)
        {
            _commonHelper = commonHelper;
            _questionRatingRepository = questionRatingRepository;
            _questionRepository = questionRepository;
        }
        
        // Get multiple choice question for the currently selected units based on question selection algorithm. 
        public Question GetMultipleChoiceQuestionBasedOnRating(string userName)
        {
            // Get the currently selected units. 
            var units = _commonHelper.GetSelectedUnitsList(userName).Select(u => u.SelectedUnitId).ToList();

            // Find new question.  
            var question = _questionRepository.GetAllQuestions().Where(q => !_questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName).Select(r => r.QuestionId).Contains(q.Id) && units.Contains(q.UnitId)).FirstOrDefault();

            // If no new questions found look for question on different criteria. 
            if (question is null)
            {
                // Find lowest rated question id. 
                var nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.UnitId) && r.Time < DateTime.Now.AddHours(-1) && r.Rating < 6).OrderBy(r => r.Time).FirstOrDefault();

                // If no low rated questions due to be shown look for oldest question id. 
                if (nextQuestionId is null)
                {
                    // Get oldest rated question id. 
                    nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.UnitId)).OrderBy(r => r.Time).FirstOrDefault();
                }

                if (nextQuestionId != null)
                {
                    // Get the question from the selected id. 
                    question = _questionRepository.GetQuestionById(nextQuestionId.QuestionId);
                }
            }

            return question;
        }

        // Updated rating for question or if not rated yet insert a new rating. 
        public void UpdateOrInsertRating(string userName, int questionId, bool isCorrect)
        {
            // Update rating. 
            var ratingUpdated = UpdateRating(userName, questionId, isCorrect);

            // Check if there was a rating to update. 
            if (ratingUpdated == false)
            {
                // Insert rating. 
                AddRating(userName, questionId, isCorrect);
            }
        }

        // Add a new rating. 
        private void AddRating(string userName, int questionId, bool isCorrect)
        {
            // Create new rating. 
            QuestionRating newRating = new QuestionRating()
            {
                QuestionId = questionId,
                Rating = (isCorrect) ? 1 : 0,
                UserName = userName,
                Time = DateTime.Now
            };

            _questionRatingRepository.AddRating(newRating);
        }

        // Update rating. 
        private bool UpdateRating(string userName, int questionId, bool isCorrect)
        {
            // Get the rating. 
            var rating = _questionRatingRepository.GetAllRatings().Where(x => x.QuestionId == questionId && x.UserName.Equals(userName)).FirstOrDefault();

            // Check if rating found. 
            if (rating != null)
            {
                // Set score. 
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

                // Set access time and update the rating. 
                rating.Time = DateTime.Now;
                _questionRatingRepository.UpdateRating(rating);

                return true;
            }

            return false;
        }
    }
}
