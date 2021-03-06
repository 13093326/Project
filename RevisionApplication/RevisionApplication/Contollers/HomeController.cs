﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.ViewModels;

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

        // Error page on exception. 
        public IActionResult Error()
        {
            // Get the exception. 
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            
            // Display the exception message on the error page. 
            if (ex != null)
            {
                ViewBag.Message = ex.Error.Message;
            }

            return View();
        }

        // Main menu. 
        public IActionResult Index()
        {
            // Get user selected units or set to default of all units. 
            var units = _commonHelper.GetUserSettingsOrCreate(User.Identity.Name);

            // Check if user has the admin role. 
            var isUserAdminRole = _commonHelper.IsUserRoleAdmin(User.Identity.Name);

            // Create page model. 
            var homeViewModel = new HomeViewModel()
            {
                Title = "Main Menu",
                SelectedUnits = units,
                IsAdmin = isUserAdminRole
            };

            return View(homeViewModel);
        }
    }
}
