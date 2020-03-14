using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public interface ITestQuestionRepository
    {
        IEnumerable<TestQuestion> GetAllTestQuestions();
        bool AddTestQuestion(TestQuestion testQuestion);
        bool AddTestQuestions(List<TestQuestion> testQuestions);
        TestQuestion GetTestQuestionById(int Id);
        bool UpdateTestQuestion(TestQuestion testQuestion);
    }
}
