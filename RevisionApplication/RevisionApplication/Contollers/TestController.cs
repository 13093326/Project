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

        // Test question page. 
        [HttpGet]
        public IActionResult Index()
        {
            // Get the next test question. 
            var nextTestQuestion = _testHelper.GetNextTestQuestion(User.Identity.Name);

            // Check there is a question to display. 
            if (nextTestQuestion != null)
            {
                // Get the question. 
                var nextQuestion = _testHelper.GetQuestionById(nextTestQuestion.QuestionId);
                
                // Create page model. 
                var testViewModel = new TestViewModel()
                {
                    Title = "Test",
                    Question = nextQuestion,
                    CurrentRecord = nextTestQuestion.Id
                };

                return View(testViewModel);
            }
            else
            {
                // No further questions found so close test set. 
                var testSetId = _testHelper.CloseCurrentTestSet(User.Identity.Name);

                // Display results page. 
                return RedirectToAction("Result", "Test", new { Id = testSetId } );
            }
        }

        // Post test question page. 
        [HttpPost]
        public IActionResult Index(TestViewModel model)
        {
            // Check fields valid. 
            if (ModelState.IsValid && model.ChosenAnswer != 0)
            {
                // Record answer result. 
                var result = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer)) ? "True" : "False";
                var testQuestion = _testHelper.GetTestQuestionById(model.CurrentRecord);
                testQuestion.Result = result;
                _testHelper.UpdateTestQuestion(testQuestion);

                // Display next question.  
                return RedirectToAction("Index", "Test");
            }

            // Custom validation. 
            if (model.ChosenAnswer == 0)
            {
                ViewBag.RadioValidation = "The Answer field is required.";
            }

            // Load original page due to invalid fields. 
            return View(model);
        }

        // Results page. 
        [HttpGet]
        public IActionResult Result(int Id)
        {
            // Get test results. 
            var testSet = _testHelper.GetTestSetById(Id);

            // Create page model. 
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