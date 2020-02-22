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
    public class HomeController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public HomeController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {

            var questions = _questionRepository.GetAllQuestions().OrderBy(p => p.Id).ToList();

            var homeViewModel = new HomeViewModel()
            {
                Title = "Questions",
                Questions = questions
            };

            return View(homeViewModel);
        }
    }
}
