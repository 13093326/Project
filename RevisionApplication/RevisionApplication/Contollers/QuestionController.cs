using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Collections.Generic;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IQuestionHelper _questionHelper;

        public QuestionController(ICommonHelper commonHelper, IQuestionHelper questionHelper)
        {
            _commonHelper = commonHelper;
            _questionHelper = questionHelper;
        }

        // Display list of questions page. 
        public IActionResult Index()
        {
            // Get all questions. 
            var questions = _questionHelper.GetAllQuestions(User.Identity.Name);

            // Create page model. 
            var homeViewModel = new QuestionListViewModel()
            {
                Title = "Questions",
                Questions = questions
            };

            return View(homeViewModel);
        }

        // Add question page. 
        [HttpGet]
        public IActionResult Add()
        {
            // Create page model. 
            QuestionViewModel model = new QuestionViewModel()
            {
                Title = "Add Question",
                Units = _commonHelper.GetUnitNames()
            };

            return View(model);
        }

        // Delete question. 
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            // Delete selected question. 
            _questionHelper.DeleteQuestion(Id);

            return RedirectToAction("Index", "Question");
        }

        // Edit question. 
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            // Get selected question. 
            var question = _questionHelper.GetQuestionById(Id);
            var unit = _commonHelper.GetUnitById(question.UnitId);
            List<string> units = _commonHelper.GetUnitNames();

            // Create page model. 
            QuestionViewModel model = new QuestionViewModel
            {
                Content = question.Content, 
                Answer1 = question.Answer1, 
                Answer2 = question.Answer2, 
                Answer3 = question.Answer3, 
                Answer4 = question.Answer4, 
                CorrectAnswer = question.CorrectAnswer, 
                Reference = question.Reference,
                SelectedUnit = unit.Name,
                Units = units,
                Title = "Edit Question"
        }; 

            return View(model);
        }

        // Post edit question. 
        [HttpPost]
        public IActionResult Edit(QuestionViewModel model)
        {
            // Get selected unit. 
            var unit = _questionHelper.GetUnitByName(model.SelectedUnit);

            // Check fields valid. 
            if (ModelState.IsValid && model.CorrectAnswer != 0 && unit != null)
            {
                // Save question. 
                Question question = new Question
                {
                    Id = model.Id,
                    Answer1 = model.Answer1,
                    Answer2 = model.Answer2,
                    Answer3 = model.Answer3,
                    Answer4 = model.Answer4,
                    Content = model.Content,
                    CorrectAnswer = model.CorrectAnswer,
                    Reference = model.Reference,
                    Unit = unit,
                    UnitId = unit.Id
                };

                _questionHelper.UpdateQuestion(question);

                // Load main menu. 
                return RedirectToAction("Index", "Question");
            }
            else
            {
                // Custom validation. 
                if (model.CorrectAnswer == 0)
                {
                    ViewBag.RadioValidation = "The Correct answer field is required.";
                }

                if (unit is null)
                {
                    ViewBag.UnitValidation = "The Unit field is required.";
                }
            }

            // Set title and unit list names for page. 
            model.Title = "Edit Question";
            model.Units = _commonHelper.GetUnitNames();

            // Load original page due to invalid fields. 
            return View(model);
        }

        // Post add question. 
        [HttpPost]
        public IActionResult Add(QuestionViewModel model)
        {
            // Get the selected unit. 
            var unit = _questionHelper.GetUnitByName(model.SelectedUnit);

            // Check fields valid. 
            if (ModelState.IsValid && model.CorrectAnswer != 0 && unit != null)
            {
                // Add question. 
                Question question = new Question
                {
                    Answer1 = model.Answer1,
                    Answer2 = model.Answer2,
                    Answer3 = model.Answer3,
                    Answer4 = model.Answer4,
                    Content = model.Content,
                    CorrectAnswer = model.CorrectAnswer,
                    Reference = model.Reference,
                    UnitId = unit.Id
                };

                _questionHelper.AddQuestion(question);

                // Load main menu. 
                return RedirectToAction("Index", "Home");
            }
            else
           {
                // Custom validation. 
                if (model.CorrectAnswer == 0)
                {
                    ViewBag.RadioValidation = "The Correct answer field is required.";
                }

                if (unit is null)
                {
                    ViewBag.UnitValidation = "The Unit field is required.";
                }
            }

            // Set unit list names for page. 
            model.Units = _commonHelper.GetUnitNames();

            // Load original page due to invalid fields. 
            return View(model);
        }
    }
}
