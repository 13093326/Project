using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class TestController : Controller
    {
        private readonly ITestSetRepository _testSetRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        public const string SessionKeyName = "_Unit";

        public TestController(ITestSetRepository testSetRepository, IQuestionRepository questionRepository, IUnitRepository unitRepository, ITestQuestionRepository testQuestionRepository)
        {
            _testSetRepository = testSetRepository;
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _testQuestionRepository = testQuestionRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var currentUser = User.Identity.Name;

            // Find if there is a current test set 
            var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.user.Equals(currentUser)).OrderBy(p => p.Id).FirstOrDefault();

            if (currentTestSet is null)
            {
                // No test set then create one 
                var testSet = _testSetRepository.AddTestSet( new TestSet { user = currentUser, complete = false });

                // Get questions for the selected unit 
                var selectedUnits = HttpContext.Session.GetString(SessionKeyName).Split(',').Select(int.Parse).ToList();
                var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));
                var questions = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit)).OrderBy(p => p.Id).Take(50);

                // Insert questions in to test questions 
                foreach (var question in questions)
                {
                    _testQuestionRepository.AddTestQuestion(new TestQuestion { TestSet = testSet, Question = question, Result = "None" });
                }

                currentTestSet = testSet;
            }

            // Display next question for test set 
            var nextTestQuestion = _testQuestionRepository.GetAllTestQuestions().Where(p => p.Result.Equals("None")).OrderBy(p => p.Id).FirstOrDefault();

            if (nextTestQuestion != null)
            {
                var nextQuestion = _questionRepository.GetQuestionById(nextTestQuestion.QuestionId);

                var testViewModel = new TestViewModel()
                {
                    Title = "Test",
                    Question = nextQuestion,
                    currentRecord = nextTestQuestion.Id
                };

                return View(testViewModel);
            }
            else
            {
                // Close test set 
                currentTestSet.complete = true;
                _testSetRepository.UpdateTestSet(currentTestSet);

                return RedirectToAction("Result", "Test");
            }
        }

        [HttpPost]
        public IActionResult Index(TestViewModel testViewModel)
        {

            // Record results 
            var result = (testViewModel.ChosenAnswer.Equals(testViewModel.Question.CorrectAnswer))? "True" : "False";
            var testQuestion = _testQuestionRepository.GetTestQuestionById(testViewModel.currentRecord);
            testQuestion.Result = result;
            _testQuestionRepository.UpdateTestQuestion(testQuestion);


            // Check for next question 
            var currentUser = User.Identity.Name;
            var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.user.Equals(currentUser)).OrderBy(p => p.Id).FirstOrDefault();
            var nextTestQuestion = _testQuestionRepository.GetAllTestQuestions().Where(p => p.Result.Equals("None")).OrderBy(p => p.Id).FirstOrDefault();

            // No questions dispaly result page 
            return RedirectToAction("Index", "Test");
        }

        [HttpGet]
        public IActionResult Result()
        {

            // Display test results 

            return View();
        }
    }
}