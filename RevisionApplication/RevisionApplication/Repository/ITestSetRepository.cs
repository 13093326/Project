using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public interface ITestSetRepository
    {
        IEnumerable<TestSet> GetAllTestSets();
        TestSet AddTestSet(TestSet testSet);
        bool UpdateTestSet(TestSet testSet);
    }
}
