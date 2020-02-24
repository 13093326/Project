using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                Title = "Main menu",
            };

            return View(homeViewModel);
        }
    }
}
