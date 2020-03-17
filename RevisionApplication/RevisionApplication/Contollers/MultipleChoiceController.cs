using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class MultipleChoiceController : Controller
    {
        private readonly ICommonHelper _commonHelper;

        public MultipleChoiceController(ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper; 
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            // Get question based on rating 
            var question = _commonHelper.GetMultipleChoiceQuestionBasedOnRating(User.Identity.Name); 

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
            if (ModelState.IsValid)
            {
                // Check if answer was correct 
                var isCorrect = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer.ToString())) ? true : false;

                // Update question rating 
                _commonHelper.UpdateOrInsertRating(User.Identity.Name, model.Question.Id, isCorrect);

                if (isCorrect)
                {
                    ViewBag.Message = "Correct.";
                    model.Title = "Multiple choice answer";
                }
                else
                {
                    ViewBag.Message = $"Answer { model.ChosenAnswer } is incorrect.";
                }
                return View("Answer", model);
            }

            return View(model);
        }
    }
}
