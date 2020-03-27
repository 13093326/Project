using RevisionApplication.Models;

namespace RevisionApplication.Helpers
{
    public interface IMultipleChoiceHelper
    {
        Question GetMultipleChoiceQuestionBasedOnRating(string userName);
        void UpdateOrInsertRating(string userName, int questionId, bool isCorrect);
    }
}
