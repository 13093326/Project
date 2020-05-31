using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class UnitController : Controller
    {
        private readonly IUnitHelper _unitHelper;
        private readonly ICommonHelper _commonHelper;

        public UnitController(IUnitHelper unitHelper, ICommonHelper commonHelper)
        {
            _unitHelper = unitHelper;
            _commonHelper = commonHelper;
        }

        // Unit list page. 
        public IActionResult Index()
        {
            // Display all units. 
            UnitListViewModel model = new UnitListViewModel
            {
                Title = "Units",
                Units = _unitHelper.GetAllUnits()
            };

            return View(model);
        }

        // Add unit. 
        [HttpGet]
        public IActionResult Add()
        {
            UnitViewModel model = new UnitViewModel
            {
                Title = "Add Unit",
                Unit = new Unit()
            };

            return View(model);
        }

        // Edit unit. 
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            UnitViewModel model = new UnitViewModel
            {
                Title = "Edit Unit",
                Unit = _commonHelper.GetUnitById(Id)
            };

            return View(model);
        }

        // Post edit unit. 
        [HttpPost]
        public IActionResult Edit(UnitViewModel model)
        {
            // Check fields valid. 
            if (ModelState.IsValid)
            {
                // Update selected unit. 
                _unitHelper.UpdateUnit(model.Unit);

                // Load unit list. 
                return RedirectToAction("Index", "Unit");
            }

            // Set title. 
            model.Title = "Edit Unit";

            // Load original page due to invalid fields. 
            return View(model);
        }

        // Post add unit. 
        [HttpPost]
        public IActionResult Add(UnitViewModel model)
        {
            // Check fields valid. 
            if (ModelState.IsValid)
            {
                // Add new unit. 
                _unitHelper.AddUnit(model.Unit);

                // Load unit list. 
                return RedirectToAction("Index", "Unit");
            }

            // Set title. 
            model.Title = "Add Unit";

            // Load original page due to invalid fields. 
            return View(model);
        }
    }
}