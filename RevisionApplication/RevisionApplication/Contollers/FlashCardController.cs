using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class FlashCardController : Controller
    {
        private readonly ICommonHelper _commonHelper;

        public FlashCardController(ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            // Get random question that is not the same as the last question 
            var question = _commonHelper.GetRandomQuestionFromUnits(User.Identity.Name, record);

            if (question is null)
            {
                return RedirectToAction("Index", "FlashCard", new { record = 0 });
            }

            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Flash card question",
                Question = question,
                currentRecord = record
            };

            return View(revisionViewModel);
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            model.Title = "Flash card answer"; 
            return View("Answer", model);
        }
    }
}
