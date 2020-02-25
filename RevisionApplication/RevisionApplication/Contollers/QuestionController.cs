using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public IActionResult Index()
        {

            var questions = _questionRepository.GetAllQuestions().OrderBy(p => p.Id).ToList();

            var homeViewModel = new QuestionViewModel()
            {
                Title = "Questions",
                Questions = questions
            };

            return View(homeViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Question question, Unit unit)
        {
            _questionRepository.AddQuestion(question, unit);

            return RedirectToAction("Index", "Home");
        }
    }
}
