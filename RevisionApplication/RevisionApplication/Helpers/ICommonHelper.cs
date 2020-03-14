using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Helpers
{
    public interface ICommonHelper
    {
        IEnumerable<Unit> GetUserSelectedUnits(string userName);
        string GetUserSettingsOrCreate(string userName);
        Question GetRandomQuestionFromUnits(string userName, int record);
    }
}
