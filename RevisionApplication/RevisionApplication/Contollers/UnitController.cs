using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Helpers;
using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class UnitController : Controller
    {
        private readonly IUnitHelper _unitHelper;

        public UnitController(IUnitHelper unitHelper)
        {
            _unitHelper = unitHelper;
        }

        // Unit list page. 
        public IActionResult Index()
        {
            // Display all units. 
            List<Unit> model = _unitHelper.GetAllUnits();

            return View(model);
        }

        // Add unit. 
        [HttpGet]
        public IActionResult Add()
        {
            Unit model = new Unit();

            return View(model);
        }

        // Edit unit. 
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Unit model = _unitHelper.GetUnitById(Id);

            return View(model);
        }

        // Post edit unit. 
        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            // Check fields valid. 
            if (ModelState.IsValid)
            {
                // Update selected unit. 
                _unitHelper.UpdateUnit(unit);

                // Load unit list. 
                return RedirectToAction("Index", "Unit");
            }

            // Load original page due to invalid fields. 
            return View(unit);
        }

        // Post add unit. 
        [HttpPost]
        public IActionResult Add(Unit unit)
        {
            // Check fields valid. 
            if (ModelState.IsValid)
            {
                // Add new unit. 
                _unitHelper.AddUnit(unit);

                // Load unit list. 
                return RedirectToAction("Index", "Unit");
            }

            // Load original page due to invalid fields. 
            return View(unit);
        }
    }
}