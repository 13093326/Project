using RevisionApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Repository
{
    public class TestQuestionRepository : ITestQuestionRepository
    {
        private readonly AppDbContext _appDbContext;

        public TestQuestionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TestQuestion> GetAllTestQuestions()
        {
            return _appDbContext.TestQuestion;
        }

        public bool AddTestQuestion(TestQuestion testQuestion)
        {
            _appDbContext.Add(testQuestion);
            _appDbContext.SaveChanges();

            return true;
        }

        public bool AddTestQuestions(List<TestQuestion> testQuestions)
        {
            _appDbContext.AddRange(testQuestions);
            _appDbContext.SaveChanges();

            return true;
        }

        public TestQuestion GetTestQuestionById(int testQuestionId)
        {
            return _appDbContext.TestQuestion.FirstOrDefault(q => q.Id == testQuestionId);
        }

        public bool UpdateTestQuestion(TestQuestion testQuestion)
        {
            _appDbContext.TestQuestion.Update(testQuestion);
            _appDbContext.SaveChanges();

            return true;
        }
    }
}
