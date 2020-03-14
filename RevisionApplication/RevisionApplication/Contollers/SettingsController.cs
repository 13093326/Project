using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;
using System.Linq;

namespace RevisionApplication.Contollers
{
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
        public IActionResult Index(SettingsViewModel settingsViewModel)
        {

            if (settingsViewModel.SelectedUnitIds != null)
            {
                _commonHelper.UpdateSelectedUnits(User.Identity.Name, settingsViewModel.SelectedUnitIds);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
