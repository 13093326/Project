using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.ViewModels;
using Microsoft.AspNetCore.Http;

namespace RevisionApplication.Contollers
{
    public class HomeController : Controller
    {
        public const string SessionKeyName = "_Unit";

        [Authorize]
        public IActionResult Index()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "All");
            }

            var unit = HttpContext.Session.GetString(SessionKeyName);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Main menu",
            };

            return View(homeViewModel);
        }
    }
}
