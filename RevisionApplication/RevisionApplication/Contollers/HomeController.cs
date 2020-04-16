using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;
using System;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICommonHelper _commonHelper;

        public HomeController(ICommonHelper commonHelper)
        {
            _commonHelper = commonHelper;
        }

        public IActionResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            if (ex != null)
            {
                ViewBag.Message = ex.Error.Message;
            }

            return View();
        }

        public IActionResult Index()
        {
            // Get user selected units or create default 
            var units = _commonHelper.GetUserSettingsOrCreate(User.Identity.Name);
            var isUserAdminRole = _commonHelper.isUserRoleAdmin(User.Identity.Name);

            var homeViewModel = new HomeViewModel()
            {
                Title = "Main Menu",
                SelectedUnits = units,
                isAdmin = isUserAdminRole
            };

            return View(homeViewModel);
        }
    }
}
