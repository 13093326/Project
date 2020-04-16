﻿using Microsoft.AspNetCore.Authorization;
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
            // Get random question that is not the same as the last question 
            var question = _flashCardHelper.GetRandomQuestionFromUnits(User.Identity.Name, record);

            if (question is null)
            {
                ViewBag.Message = "No questions found.";
            }

            var revisionViewModel = new RevisionViewModel()
            {
                Title = "Flash Card Question",
                Question = question,
                currentRecord = record
            };

            return View(revisionViewModel);            
        }

        [HttpPost]
        public IActionResult Index(RevisionViewModel model)
        {
            model.Title = "Flash Card Answer"; 
            return View("Answer", model);
        }
    }
}
