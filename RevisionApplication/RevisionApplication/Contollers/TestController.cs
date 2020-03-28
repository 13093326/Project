using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly ITestHelper _testHelper;

        public TestController(ITestHelper testHelper)
        {
            _testHelper = testHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Get the next test question 
            var nextTestQuestion = _testHelper.GetNextTestQuestion(User.Identity.Name); 

            if (nextTestQuestion != null)
            {
                var nextQuestion = _testHelper.GetQuestionById(nextTestQuestion.QuestionId);

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
            if (ModelState.IsValid && model.ChosenAnswer != 0)
            {
                // Record results 
                var result = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer)) ? "True" : "False";
                var testQuestion = _testHelper.GetTestQuestionById(model.currentRecord);
                testQuestion.Result = result;
                _testHelper.UpdateTestQuestion(testQuestion);

                // Check for next question 
                var currentTestSet = _testHelper.GetCurentTestSet(User.Identity.Name);
                var nextTestQuestion = _testHelper.GetNextTestQuestion();

                // Display next question 
                return RedirectToAction("Index", "Test");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Result(int Id)
        {
            // Get test results 
            var testSet = _testHelper.GetTestSetById(Id);

            var resultViewModel = new ResultViewModel
            {
                Title = "Test Results",
                TotalCount = testSet.TotalCount, 
                CorrectCount = testSet.CorrectCount
            };

            return View(resultViewModel);
        }
    }
}