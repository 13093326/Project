using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class FlashCardController : Controller
    {
        private readonly IFlashCardHelper _flashCardHelper;

        public FlashCardController(IFlashCardHelper flashCardHelper)
        {
            _flashCardHelper = flashCardHelper;
        }

        // Flash card question page. 
        [HttpGet] 
        public IActionResult Index(int record)
        {
            // Get random question that is not the same as the last question. 
            var question = _flashCardHelper.GetRandomQuestionFromUnits(User.Identity.Name, record);

            // Handle no questions in database. 
            if (question is null)
            {
                ViewBag.Message = "No questions found.";
            }

            // Create page model. 
            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Flash Card Question",
                Question = question,
                CurrentRecord = record
            };

            return View(revisionViewModel);            
        }

        // Post flash card question page and load answer page. 
        [HttpPost] 
        public IActionResult Index(RevisionViewModel model)
        {
            // Pass model to answer page to avoid database call. 
            model.Title = "Flash Card Answer"; 
            return View("Answer", model);
        }
    }
}
