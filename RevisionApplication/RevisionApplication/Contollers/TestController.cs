using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestSetRepository _testSetRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly ITestHelper _testHelper;

        public TestController(ITestSetRepository testSetRepository, IQuestionRepository questionRepository, IUnitRepository unitRepository, ITestQuestionRepository testQuestionRepository, IUserSettingsRepository userSettingsRepository, ITestHelper testHelper)
        {
            _testSetRepository = testSetRepository;
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _testQuestionRepository = testQuestionRepository;
            _userSettingsRepository = userSettingsRepository;
            _testHelper = testHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Get the next test question 
            var nextTestQuestion = _testHelper.GetNextTestQuestion(User.Identity.Name); 

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
                var testSetId = _testHelper.CloseCurrentTestSet(User.Identity.Name);

                return RedirectToAction("Result", "Test", new { Id = testSetId } );
            }
        }

        [HttpPost]
        public IActionResult Index(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Record results 
                var result = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer)) ? "True" : "False";
                var testQuestion = _testQuestionRepository.GetTestQuestionById(model.currentRecord);
                testQuestion.Result = result;
                _testQuestionRepository.UpdateTestQuestion(testQuestion);


                // Check for next question 
                var currentUser = User.Identity.Name;
                var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.User.Equals(currentUser)).OrderBy(p => p.Id).FirstOrDefault();
                var nextTestQuestion = _testQuestionRepository.GetAllTestQuestions().Where(p => p.Result.Equals("None")).OrderBy(p => p.Id).FirstOrDefault();

                // Display next question 
                return RedirectToAction("Index", "Test");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Result(int Id)
        {
            // Get test results 
            var testSet = _testSetRepository.GetTestSetById(Id);

            var resultViewModel = new ResultViewModel
            {
                TotalCount = testSet.TotalCount, 
                CorrectCount = testSet.CorrectCount
            };

            return View(resultViewModel);
        }
    }
}