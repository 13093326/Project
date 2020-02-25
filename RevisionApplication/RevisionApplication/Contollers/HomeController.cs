using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using RevisionApplication.Models;
using System.Linq;

namespace RevisionApplication.Contollers
{
    public class HomeController : Controller
    {

        private readonly IUnitRepository _unitRepository;
        public const string SessionKeyName = "_Unit";

        public HomeController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        [Authorize]
        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                var allUnitsIds = _unitRepository.GetAllUnitIds();

                HttpContext.Session.SetString(SessionKeyName, allUnitsIds);
            }

            var units = HttpContext.Session.GetString(SessionKeyName);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Main menu",
                SelectedUnits = units
            };

            return View(homeViewModel);
        }
    }
}
