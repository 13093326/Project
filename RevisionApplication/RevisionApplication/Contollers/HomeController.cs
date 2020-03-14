using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.Repository;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class HomeController : Controller
    {

        private readonly ICommonHelper _commonHelper;

        public HomeController(ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;

        }

        [Authorize]
        public IActionResult Index()
        {
            // Get user selected units or create default 
            var units = _commonHelper.GetUserSettingsOrCreate(User.Identity.Name);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Main Menu",
                SelectedUnits = units
            };

            return View(homeViewModel);
        }
    }
}
