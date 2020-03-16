using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.Repository;

namespace RevisionApplication.Contollers
{
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
        public IActionResult Edit(Unit model)
        {
            _unitRepository.UpdateUnit(model);

            return RedirectToAction("Index", "Unit");
        }

        [HttpPost]
        public IActionResult Add(Unit unit)
        {
            _unitRepository.AddUnit(unit);

            return RedirectToAction("Index", "Unit");
        }
    }
}