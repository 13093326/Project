using Microsoft.EntityFrameworkCore;
using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void GetTestHistoryReport(string name)
        {
            var test = _appDbContext.ReportTestHistory.FromSql($"exec GetTestHistoryReport '{ name }'").ToList();
        }
    }
}
