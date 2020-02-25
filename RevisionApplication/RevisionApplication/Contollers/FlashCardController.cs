using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
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

            var question = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit)).Where(p => p.Id > record).OrderBy(p => p.Id).FirstOrDefault();

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
