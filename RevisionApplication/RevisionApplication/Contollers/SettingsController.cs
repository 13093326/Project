using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ICommonHelper _commonHelper;

        public SettingsController(IUnitRepository unitRepository, ICommonHelper commonHelper)
        {
            _unitRepository = unitRepository;
            _commonHelper = commonHelper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var settingsViewModel = new SettingsViewModel()
            {
                Title = "Settings",
                Units = _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList(),
                SelectedUnitIds = _commonHelper.GetSelectedUnitsIdList(User.Identity.Name)
            };

            return View(settingsViewModel);
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUnitIds != null)
                {
                    _commonHelper.UpdateSelectedUnits(User.Identity.Name, model.SelectedUnitIds);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
