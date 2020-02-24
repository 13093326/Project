using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class MultipleChoiceController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public MultipleChoiceController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            var question = _questionRepository.GetAllQuestions().Where(p => p.Id > record).OrderBy(p => p.Id).FirstOrDefault();

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
