using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;
using System;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class FlashCardController : Controller
    {
        private readonly IFlashCardHelper _flashCardHelper;
        private readonly ICommonHelper _commonHelper;

        public FlashCardController(IFlashCardHelper flashCardHelper, ICommonHelper commonHelper)
        {
            _flashCardHelper = flashCardHelper;
            _commonHelper = commonHelper;
        }

        [HttpGet]
        public IActionResult Index(int record)
        {
            try
            {
                // Get random question that is not the same as the last question 
                var question = _flashCardHelper.GetRandomQuestionFromUnits(User.Identity.Name, record);

                if (question is null)
                {
                    ViewBag.Message = "No questions found.";
                }

                var revisionViewModel = new RevisionViewModel()
                {
                    Title = "Flash card question",
                    Question = question,
                    currentRecord = record
                };

                return View(revisionViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("../Error/Index");
            }            
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            model.Title = "Flash card answer"; 
            return View("Answer", model);
        }
    }
}
