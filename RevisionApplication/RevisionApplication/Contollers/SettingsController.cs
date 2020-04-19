using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ICommonHelper _commonHelper;
        private readonly ISettingsHelper _settingsHelper;

        public SettingsController(ICommonHelper commonHelper, ISettingsHelper settingsHelper)
        {
            _commonHelper = commonHelper;
            _settingsHelper = settingsHelper;
        }

        // Settings page. 
        [HttpGet]
        public IActionResult Index()
        {
            // Create page model. 
            var settingsViewModel = new SettingsViewModel()
            {
                Title = "Settings",
                Units = _commonHelper.GetSelectedUnitsProperteisList(User.Identity.Name)
        };

            return View(settingsViewModel);
        }

        // Post settings page. 
        [HttpPost]
        public IActionResult Index(SettingsViewModel model)
        {
            // Check fields valid. 
            if (model.Units.Count() > 0 && model.Units.Where(x => x.isSelected).Count() > 0)
            {
                // Update unit selection. 
                _settingsHelper.UpdateSelectedUnits(User.Identity.Name, model.Units);

                // Load main menu. 
                return RedirectToAction("Index", "Home");
            }

            // Load original page due to invalid fields. 
            ViewBag.UnitValidation = "Select at least one unit.";
            return View(model);
        }
    }
}
