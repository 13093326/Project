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

        // Multiple choice question page. 
        [HttpGet]
        public IActionResult Index()
        {
            // Get question based on rating algorithm. 
            var question = _multipleChoiceHelper.GetMultipleChoiceQuestionBasedOnRating(User.Identity.Name); 

            // Create page model. 
            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Multiple Choice Question",
                Question = question
            };

            return View(revisionViewModel);
        }

        // Post multiple choice page and load answer page. 
        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            // Check fields valid. 
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

                // Load answer page. 
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

            // Load question page due to invalid fields. 
            return View(model);
        }
    }
}
