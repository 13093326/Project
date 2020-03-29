using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;

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

        public IActionResult Index()
        {
            var questions = _questionHelper.GetAllQuestions(User.Identity.Name);

            var homeViewModel = new QuestionListViewModel()
            {
                Title = "Questions",
                Questions = questions
            };

            return View(homeViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            QuestionViewModel model = new QuestionViewModel();

            model.Title = "Add Question";
            model.Units = _commonHelper.GetUnitNames();

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _questionHelper.DeleteQuestion(Id);

            return RedirectToAction("Index", "Question");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var question = _questionHelper.GetQuestionById(Id);
            var unit = _questionHelper.GetUnitById(question.UnitId);
            List<string> units = _commonHelper.GetUnitNames();

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
                Units = units
            };

            model.Title = "Edit Question";

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(QuestionViewModel model)
        {
            var unit = _questionHelper.GetUnitByName(model.SelectedUnit);

            if (ModelState.IsValid && model.CorrectAnswer != 0 && unit != null)
            {
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

                return RedirectToAction("Index", "Home");
            }

            if (model.CorrectAnswer == 0)
            {
                ViewBag.RadioValidation = "The Correct answer field is required.";
            }

            if (unit is null)
            {
                ViewBag.UnitValidation = "The Unit field is required.";
            }

            model.Title = "Edit Question";
            model.Units = _commonHelper.GetUnitNames();

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(QuestionViewModel model)
        {
            var unit = _questionHelper.GetUnitByName(model.SelectedUnit);

            if (ModelState.IsValid && model.CorrectAnswer != 0 && unit != null)
            {
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

                return RedirectToAction("Index", "Home");
            }

            if (model.CorrectAnswer == 0)
            {
                ViewBag.RadioValidation = "The Correct answer field is required.";
            }

            if (unit is null)
            {
                ViewBag.UnitValidation = "The Unit field is required.";
            }

            model.Units = _commonHelper.GetUnitNames();

            return View(model);
        }
    }
}
