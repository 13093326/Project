using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class MultipleChoiceController : Controller
    {
        private readonly IMultipleChoiceHelper _multipleChoiceHelper;

        public MultipleChoiceController(IMultipleChoiceHelper multipleChoiceHelper)
        {
            _multipleChoiceHelper = multipleChoiceHelper;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            // Get question based on rating algorithm. 
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
            // Check fields validated. 
            if (ModelState.IsValid && model.ChosenAnswer != null)
            {
                // Check if answer was correct. 
                var isCorrect = (model.ChosenAnswer.Equals(model.Question.CorrectAnswer.ToString())) ? true : false;

                // Update question rating based on answer. 
                _multipleChoiceHelper.UpdateOrInsertRating(User.Identity.Name, model.Question.Id, isCorrect);

                // Set feedback for page. 
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
            else
            {
                // Custom validation. 
                if (model.ChosenAnswer is null)
                {
                    ViewBag.RadioValidation = "The Answer field is required.";
                }
            }

            return View(model);
        }
    }
}
