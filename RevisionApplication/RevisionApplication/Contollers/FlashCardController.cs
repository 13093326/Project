using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RevisionApplication.Contollers
{
    public class FlashCardController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public FlashCardController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {

            var question = _questionRepository.GetAllQuestions().Where(p => p.Id > record).OrderBy(p => p.Id).FirstOrDefault();

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


        [HttpPost]
        public IActionResult Answer(RevisionViewModel model, int record)
        {
            return RedirectToAction("Index", "FlashCard", new { record = model.Question.Id });
        }
    }
}
