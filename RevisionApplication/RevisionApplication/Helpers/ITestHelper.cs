using RevisionApplication.Models;

namespace RevisionApplication.Helpers
{
    public interface ITestHelper
    {
        int CloseCurrentTestSet(string userName);
        TestSet GetCurentTestSet(string currentUser);
        TestQuestion GetNextTestQuestion(string userName);
        TestQuestion GetNextTestQuestion();
        Question GetQuestionById(int Id);
        TestQuestion GetTestQuestionById(int Id);
        TestSet GetTestSetById(int Id);
        void UpdateTestQuestion(TestQuestion testQuestion);
    }
}
