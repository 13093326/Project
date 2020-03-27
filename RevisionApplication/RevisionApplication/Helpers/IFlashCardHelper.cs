using RevisionApplication.Models;

namespace RevisionApplication.Helpers
{
    public interface IFlashCardHelper
    {
        Question GetRandomQuestionFromUnits(string userName, int record);
    }
}
