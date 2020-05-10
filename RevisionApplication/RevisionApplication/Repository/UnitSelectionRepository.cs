using System;
using System.Collections.Generic;
using RevisionApplication.Models;
using System.Linq;

namespace RevisionApplication.Repository
{
    public class UnitSelectionRepository : IUnitSelectionRepository
    {
        private readonly AppDbContext _appDbContext;

        public UnitSelectionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public UnitSelection[] GetSelectionById(int Id)
        {
            return _appDbContext.UnitSelection.Where(u => u.UserSettingId == Id).ToArray();
        }

        public void AddSettings(int settingId, int[] selectedUnits)
        {
            AddUnits(settingId, selectedUnits);
            _appDbContext.SaveChanges();
        }

        public void UpdateSelection(int settingId, int[] selectedUnits)
        {
            // Remove current selection.  
            var removeUnits = _appDbContext.UnitSelection.Where(u => u.UserSettingId == settingId).ToList();
            _appDbContext.UnitSelection.RemoveRange(removeUnits);

            // Add selection. 
            AddUnits(settingId, selectedUnits);

            _appDbContext.SaveChanges(); 
        }

        private void AddUnits(int settingId, int[] selectedUnits)
        {
            List<UnitSelection> selection = new List<UnitSelection>();

            foreach (var item in selectedUnits)
            {
                selection.Add(new UnitSelection { SelectedUnitId = item, UserSettingId = settingId });
            }

            _appDbContext.UnitSelection.AddRange(selection);
        }
    }
}