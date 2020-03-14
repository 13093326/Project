using RevisionApplication.Models;

namespace RevisionApplication.Helpers
{
    public interface ITestHelper
    {
        TestQuestion GetNextTestQuestion(string userName);
        int CloseCurrentTestSet(string userName);
    }
}
