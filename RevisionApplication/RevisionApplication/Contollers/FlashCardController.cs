using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class FlashCardController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        public const string SessionKeyName = "_Unit";

        public FlashCardController(IQuestionRepository questionRepository, IUnitRepository unitRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            var selectedUnits = HttpContext.Session.GetString(SessionKeyName).Split(',').Select(int.Parse).ToList();
            var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));

            // Get random question that is not the same as the last question 
            Random random = new Random();
            var allValidQuestionIds = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit) && p.Id != record).OrderBy(x => random.Next()).Select(p => p.Id);
            var index = allValidQuestionIds.ElementAt(random.Next(0, allValidQuestionIds.Count() - 1));
            var question = _questionRepository.GetQuestionById(index);

            if (question is null)
            {
                return RedirectToAction("Index", "FlashCard", new { record = 0 });
            }

            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Flash card question",
                Question = question,
                currentRecord = record
            };

            return View(revisionViewModel);
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            model.Title = "Flash card answer"; 
            return View("Answer", model);
        }

    }
}
