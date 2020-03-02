using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class HomeController : Controller
    {

        private readonly IUnitRepository _unitRepository;
        private readonly IUserSettingsRepository _userSettingsRepository;

        public HomeController(IUnitRepository unitRepository, IUserSettingsRepository userSettingsRepository)
        {
            _unitRepository = unitRepository;
            _userSettingsRepository = userSettingsRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            var currentUserSettings = _userSettingsRepository.GetSettingsByUserName(User.Identity.Name); 

            if (currentUserSettings is null)
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();

                currentUserSettings = _userSettingsRepository.AddSettings(new UserSetting { Username = User.Identity.Name, SelectedUnits = allUnitsIds });
            }

            var units = currentUserSettings.SelectedUnits;

            var homeViewModel = new HomeViewModel()
            {
                Title = "Main menu",
                SelectedUnits = units
            };

            return View(homeViewModel);
        }
    }
}
