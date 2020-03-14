using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface ITestSetRepository
    {
        IEnumerable<TestSet> GetAllTestSets();
        TestSet AddTestSet(TestSet testSet);
        bool UpdateTestSet(TestSet testSet);
        TestSet GetTestSetById(int testSetId);
    }
}
