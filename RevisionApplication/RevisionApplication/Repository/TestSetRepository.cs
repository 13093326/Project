﻿using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
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

        public TestSet GetTestSetById(int testSetId)
        {
            return _appDbContext.TestSet.FirstOrDefault(q => q.Id == testSetId);
        }

        public TestSet AddTestSet(TestSet testSet)
        {
            _appDbContext.Add(testSet);
            _appDbContext.SaveChanges();

            return testSet;
        }

        public bool UpdateTestSet(TestSet testSet)
        {
            _appDbContext.TestSet.Update(testSet);
            _appDbContext.SaveChanges();

            return true;
        }
    }
}
