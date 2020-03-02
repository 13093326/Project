using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class SettingsController : Controller
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public SettingsController(IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var units = _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList();

            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(User.Identity.Name);
            var selectedUnits = currentUserSettings.SelectedUnits.Split(',').Select(int.Parse).ToArray();

            var settingsViewModel = new SettingsViewModel()
            {
                Title = "Questions",
                Units = units,
                SelectedUnitIds = selectedUnits
            };

            return View(settingsViewModel);
        }

        [HttpPost]
        public IActionResult Index(SettingsViewModel settingsViewModel)
        {

            if (settingsViewModel.SelectedUnitIds != null)
            {
                var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(User.Identity.Name);
                currentUserSettings.SelectedUnits = string.Join(",", settingsViewModel.SelectedUnitIds);
                _userSettingsRepository.UpdateSettings(currentUserSettings);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
