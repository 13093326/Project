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
        
        public Question GetMultipleChoiceQuestionBasedOnRating(string userName)
        {
            var units = _commonHelper.GetSelectedUnitsIdList(userName);

            // Find new question 
            var question = _questionRepository.GetAllQuestions().Where(q => !_questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName).Select(r => r.QuestionId).Contains(q.Id) && units.Contains(q.UnitId)).FirstOrDefault();

            if (question is null)
            {
                // Find lowest rated question id 
                var nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.UnitId) && r.Time < DateTime.Now.AddHours(-1) && r.Rating < 6).OrderBy(r => r.Time).FirstOrDefault();

                if (nextQuestionId is null)
                {
                    // Get oldest rated question id  
                    nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == userName && units.Contains(r.Question.UnitId)).OrderBy(r => r.Time).FirstOrDefault();
                }

                // Get the question 
                question = _questionRepository.GetQuestionById(nextQuestionId.QuestionId);
            }

            return question;
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
    }
}
