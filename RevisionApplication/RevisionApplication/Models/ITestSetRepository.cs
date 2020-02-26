using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface ITestSetRepository
    {
        IEnumerable<TestSet> GetAllTestSets();
        TestSet AddTestSet(TestSet testSet);
        bool UpdateTestSet(TestSet testSet);
    }
}
