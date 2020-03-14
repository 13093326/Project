using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ICommonHelper
    {
        IEnumerable<Unit> GetUserSelectedUnits(string userName);
        string GetUserSettingsOrCreate(string userName);
        int[] GetSelectedUnitsIdList(string userName);
        void UpdateSelectedUnits(string userName, int[] selectedUnits);
        void UpdateOrInsertRating(string userName, int questionId, bool isCorrect);
        Question GetMultipleChoiceQuestionBasedOnRating(string userName);
        Question GetRandomQuestionFromUnits(string userName, int record);
    }
}
