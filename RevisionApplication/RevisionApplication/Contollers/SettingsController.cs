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
        public const string SessionKeyName = "_Unit";

        public SettingsController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var units = _unitRepository.GetAllUnits().OrderBy(p => p.Id).ToList();
            
            var selectedUnits = HttpContext.Session.GetString(SessionKeyName).Split(',').Select(int.Parse).ToArray();

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
                HttpContext.Session.SetString(SessionKeyName, string.Join(",", settingsViewModel.SelectedUnitIds));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
