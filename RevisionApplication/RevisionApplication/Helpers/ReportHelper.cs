using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class ReportHelper : IReportHelper
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionRatingRepository _questionRatingRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ITestSetRepository _testSetRepository;

        public ReportHelper(IQuestionRepository questionRepository, IQuestionRatingRepository questionRatingRepository, IUnitRepository unitRepository, ITestSetRepository testSetRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _questionRatingRepository = questionRatingRepository;
            _testSetRepository = testSetRepository; 
        }

        // Get all data for the question coverage report for the current logged in user. 
        public IOrderedEnumerable<ReportQuestionCoverage> questionCoverageReport(string userName)
        {
            // Get number of questions for each unit. 
            var questionCoverageCounts = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitId = u.Id, UnitName = u.Name, QuestionId = q.Id })
                .GroupBy(t => new { t.UnitId, t.UnitName })
                .Select(group => new { questionCount = group.Count(), unitId = group.Key.UnitId, unitName = group.Key.UnitName });

            // Get number of revised questions for each unit. 
            var revisionCoverageCounts = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitId = u.Id, UnitName = u.Name, QuestionId = q.Id })
                .Join(_questionRatingRepository.GetAllRatings().Where(qr => qr.UserName == userName), q => q.QuestionId, r => r.QuestionId, (q, r) => new { q.UnitId, q.UnitName, r.Id })
                .GroupBy(t => new { t.UnitId, t.UnitName })
                .Select(group => new { revisionCount = group.Count(), unitId = group.Key.UnitId, unitName = group.Key.UnitName });

            // Get the union of the question and revision counts. 
            var questionCoverageQuery = questionCoverageCounts.SelectMany
                (q => revisionCoverageCounts.Where(r => q.unitName == r.unitName).DefaultIfEmpty(),
                (q, r) => new ReportQuestionCoverage { UnitName = q.unitName, TotalQuestionCount = q.questionCount, RatedQuestionCount = (r != null) ? r.revisionCount : 0, Percentage = (r != null) ? Math.Round(((double)r.revisionCount / (double)q.questionCount) * 100, 2) : 0 })
                .ToList().OrderByDescending(r => r.Percentage);

            return questionCoverageQuery;
        }

        // Get all data for the unit rating report for the current logged in user. 
        public IOrderedEnumerable<ReportUnitRating> GetUnitRatingReport(string userName)
        {
            // Get the average rating score for each unit. 
            var unitRatingQuery = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitName = u.Name, QuestionId = q.Id })
                .Join(_questionRatingRepository.GetAllRatings().Where(qr => qr.UserName == userName), q => q.QuestionId, qr => qr.QuestionId, (q, qr) => new { q.UnitName, q.QuestionId, qr.Rating })
                .GroupBy(t => new { t.UnitName })
                .Select(group => new ReportUnitRating { AverageRating = Math.Round(group.Average(item => item.Rating), 2), UnitName = group.Key.UnitName })
                .ToList().OrderByDescending(r => r.AverageRating);

            return unitRatingQuery;
        }

        // Get all data for the test history report for the current logged in user. 
        public IOrderedEnumerable<ReportTestHistory> GetTestHistoryReport(string userName)
        {
            // Get the test scores. 
            var testHistoryQuery = _testSetRepository.GetAllTestSets().Where(t => t.UserName == userName)
                .Select(r => new ReportTestHistory { DateTaken = r.Date, Score = r.Score })
                .ToList().OrderByDescending(r => r.DateTaken);

            return testHistoryQuery;
        }
    }
}
