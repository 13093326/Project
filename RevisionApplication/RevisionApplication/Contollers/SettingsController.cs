using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

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

        [HttpGet]
        public IActionResult Index()
        {
            var settingsViewModel = new SettingsViewModel()
            {
                Title = "Settings",
                Units = _settingsHelper.GetAllUnits(),
                SelectedUnitIds = _commonHelper.GetSelectedUnitsIdList(User.Identity.Name)
            };

            return View(settingsViewModel);
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel model)
        {
            // Check fields valid. 
            if (ModelState.IsValid)
            {
                if (model.SelectedUnitIds != null)
                {
                    // Update unit selection. 
                    _settingsHelper.UpdateSelectedUnits(User.Identity.Name, model.SelectedUnitIds);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
