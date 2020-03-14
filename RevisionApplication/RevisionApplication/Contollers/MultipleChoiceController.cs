using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;
using System;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class MultipleChoiceController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IQuestionRatingRepository _questionRatingRepository;


        public MultipleChoiceController(IQuestionRepository questionRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository, IQuestionRatingRepository questionRatingRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
            _questionRatingRepository = questionRatingRepository;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            // Record is last question 

            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(User.Identity.Name);
            var selectedUnits = currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToList();
            var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));

            // Find new question 
            var question = _questionRepository.GetAllQuestions().Where(q => !_questionRatingRepository.GetAllRatings().Where(r => r.UserName == User.Identity.Name).Select(r => r.QuestionId).Contains(q.Id)).FirstOrDefault();

            if (question is null)
            {
                // Find lowest rated question 
                var nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == User.Identity.Name && r.Time < DateTime.Now.AddHours(-1) && r.Rating < 6).OrderBy(r => r.Time).FirstOrDefault();

                if (nextQuestionId is null)
                {
                    // Get oldest rating  
                    nextQuestionId = _questionRatingRepository.GetAllRatings().Where(r => r.UserName == User.Identity.Name).OrderBy(r => r.Time).FirstOrDefault();
                }

                question = _questionRepository.GetQuestionById(nextQuestionId.QuestionId);
            }

            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Multiple choice question",
                Question = question,
                currentRecord = record
            };

            return View(revisionViewModel);
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            // Check if question has rating 
            var rating = _questionRatingRepository.GetAllRatings().Where(x => x.QuestionId == model.Question.Id && x.UserName.Equals(User.Identity.Name)).FirstOrDefault(); 
            var isCorrect = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer.ToString())) ? true : false;
            if (rating is null)
            {
                // Insert rating 
                QuestionRating newRating = new QuestionRating();
                newRating.QuestionId = model.Question.Id;
                newRating.Rating = (isCorrect)? 1 : 0;
                newRating.UserName = User.Identity.Name;
                newRating.Time = DateTime.Now;
                _questionRatingRepository.AddRating(newRating);
            }
            else
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
            }

            if (isCorrect)
            {
                ViewBag.Message = "Correct."; 
                model.Title = "Multiple choice answer";
            }
            else
            {
                ViewBag.Message = $"Answer { model.ChosenAnswer } is incorrect.";
            }
            return View("Answer", model);
        }

    }
}
