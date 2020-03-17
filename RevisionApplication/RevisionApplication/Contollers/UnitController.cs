using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.Repository;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Contollers
{
    [Authorize]
    public class UnitController : Controller
    {
        private readonly IUnitRepository _unitRepository;

        public UnitController(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public IActionResult Index()
        {
            List<Unit> model = _unitRepository.GetAllUnits().ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            Unit model = new Unit();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Unit model = _unitRepository.GetUnitById(Id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitRepository.UpdateUnit(unit);

                return RedirectToAction("Index", "Unit");
            }

            return View(unit);
        }

        [HttpPost]
        public IActionResult Add(Unit unit)
        {
            if (ModelState.IsValid)
            {
                _unitRepository.AddUnit(unit);

                return RedirectToAction("Index", "Unit");
            }

            return View(unit);
        }
    }
}