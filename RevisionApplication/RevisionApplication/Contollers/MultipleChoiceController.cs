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
        private readonly IMultipleChoiceHelper _multipleChoiceHelper;

        public MultipleChoiceController(ICommonHelper commonHelper, IMultipleChoiceHelper multipleChoiceHelper)
        {
            _commonHelper = commonHelper;
            _multipleChoiceHelper = multipleChoiceHelper;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            // Get question based on rating 
            var question = _multipleChoiceHelper.GetMultipleChoiceQuestionBasedOnRating(User.Identity.Name); 

            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Multiple Choice Question",
                Question = question,
                currentRecord = record
            };

            return View(revisionViewModel);
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            if (ModelState.IsValid && model.ChosenAnswer != null)
            {
                // Check if answer was correct 
                var isCorrect = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer.ToString())) ? true : false;

                // Update question rating 
                _multipleChoiceHelper.UpdateOrInsertRating(User.Identity.Name, model.Question.Id, isCorrect);

                if (isCorrect)
                {
                    ViewBag.Message = "Correct.";
                    ViewBag.Colour = "Green";
                }
                else
                {
                    ViewBag.Message = $"Answer { model.ChosenAnswer } is incorrect.";
                    ViewBag.Colour = "Red";
                }

                model.Title = "Multiple Choice Answer";

                return View("Answer", model);
            }

            return View(model);
        }
    }
}
