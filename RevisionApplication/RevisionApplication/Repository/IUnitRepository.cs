﻿using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface IUnitRepository
    {
        IEnumerable<Unit> GetAllUnits();
        bool AddUnit(Unit unit);
        string GetAllUnitIds();
    }
}