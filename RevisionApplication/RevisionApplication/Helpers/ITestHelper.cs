using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface ITestHelper
    {
        int CloseCurrentTestSet(string userName);
        TestSet GetCurrentTestSet(string userName);
        TestQuestion GetNextTestQuestion(string userName);
        Question GetQuestionById(int Id);
        TestQuestion GetTestQuestionById(int Id);
        TestSet GetTestSetById(int Id);
        void UpdateTestQuestion(TestQuestion testQuestion);
    }
}
