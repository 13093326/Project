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
        private readonly IQuestionRepository _questionRepository;
        private readonly ICommonHelper _commonHelper;
        private readonly IUnitRepository _unitRepository;

        public QuestionController(IQuestionRepository questionRepository, ICommonHelper commonHelper, IUnitRepository unitRepository)
        {
            _questionRepository = questionRepository;
            _commonHelper = commonHelper;
            _unitRepository = unitRepository;
        }

        public IActionResult Index()
        {
            var units = _commonHelper.GetSelectedUnitsIdList(User.Identity.Name);

            var questions = _questionRepository.GetAllQuestions()
                .Join(_unitRepository.GetAllUnits().Where(u => units.Contains(u.Id)), q => q.UnitId, u => u.Id,
                (q, u) => new Question {
                    Answer1 = q.Answer1, Answer2 = q.Answer2, Answer3 = q.Answer3, Answer4 = q.Answer4, Content = q.Content, CorrectAnswer = q.CorrectAnswer, Id = q.Id, Reference = q.Reference, UnitId = q.UnitId, Unit = u
                }).OrderBy(q => q.Id).ToList();

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

            model.Units = _commonHelper.GetUnitNames();

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _questionRepository.DeleteQuestion(Id);

            return RedirectToAction("Index", "Question");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Question question = _questionRepository.GetQuestionById(Id);

            List<string> units = _commonHelper.GetUnitNames();
            Unit unit = _unitRepository.GetUnitById(question.UnitId);

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

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = _unitRepository.GetUnitByName(model.SelectedUnit);

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

                _questionRepository.UpdateQuestion(question);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Unit unit = _unitRepository.GetUnitByName(model.SelectedUnit);

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

                _questionRepository.AddQuestion(question);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
