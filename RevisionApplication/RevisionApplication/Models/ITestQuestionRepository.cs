using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface ITestQuestionRepository
    {
        IEnumerable<TestQuestion> GetAllTestQuestions();
        bool AddTestQuestion(TestQuestion testQuestion);
        TestQuestion GetTestQuestionById(int Id);
        bool UpdateTestQuestion(TestQuestion testQuestion);
    }
}
