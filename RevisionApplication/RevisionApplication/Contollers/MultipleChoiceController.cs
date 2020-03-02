using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class MultipleChoiceController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public MultipleChoiceController(IQuestionRepository questionRepository, IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(User.Identity.Name);
            var selectedUnits = currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToList();
            var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));
            var question = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit)).Where(p => p.Id > record).OrderBy(p => p.Id).FirstOrDefault();

            if (question is null)
            {
                return RedirectToAction("Index", "MultipleChoice", new { record = 0 });
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
            if (model.ChosenAnswer.Equals(model.Question.CorrectAnswer.ToString()))
            {
                ViewBag.Message = "Correct."; 
                model.Title = "Multiple choice answer";
                return View("Answer", model);
            }
            else
            {
                ViewBag.Message = $"Answer { model.ChosenAnswer } is incorrect.";
                return View("Index", model);
            }
        }

    }
}
