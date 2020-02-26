using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
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
            return _appDbContext.TestQuestions;
        }
    }
}
