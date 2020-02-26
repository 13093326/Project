using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class TestSetRepository : ITestSetRepository
    {
        private readonly AppDbContext _appDbContext;

        public TestSetRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<TestSet> GetAllTestSets()
        {
            return _appDbContext.TestSet;
        }
    }
}
