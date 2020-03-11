using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class ReportController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly IQuestionRatingRepository _questionRatingRepository;
        private readonly ITestSetRepository _testSetRepository;


        public ReportController(IQuestionRepository questionRepository, IUnitRepository unitRepository, IQuestionRatingRepository questionRatingRepository, ITestSetRepository testSetRepository)
        {
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _questionRatingRepository = questionRatingRepository;
            _testSetRepository = testSetRepository;
        }

        public IActionResult Index()
        {
            ReportViewModel model = new ReportViewModel();

            // Get number of questions for each unit 
            var questionCoverageCounts = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitName = u.Name, QuestionId = q.Id })
                .GroupBy(t => new { t.UnitName })
                .Select(group => new { questionCount = group.Count(), unitName = group.Key.UnitName });

            // Get number of revised questions for each unit 
            var revisionCoverageCounts = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitName = u.Name, QuestionId = q.Id })
                .Join(_questionRatingRepository.GetAllRatings().Where(qr => qr.UserName == User.Identity.Name), q => q.QuestionId, r => r.QuestionId, (q, r) => new { q.UnitName, r.Id })
                .GroupBy(t => new { t.UnitName })
                .Select(group => new { revisionCount = group.Count(), unitName = group.Key.UnitName });

            // Get the union of the question and revision counts 
            var questionCoverageQuery = questionCoverageCounts.SelectMany
                (q => revisionCoverageCounts.Where(r => q.unitName == r.unitName).DefaultIfEmpty(),
                (q, r) => new ReportQuestionCoverage { UnitName = q.unitName, TotalQuestionCount = q.questionCount, RatedQuestionCount = (r != null)? r.revisionCount : 0, Percentage = (r != null) ? Math.Round(((double)r.revisionCount / (double)q.questionCount)*100,2) : 0 })
                .ToList().OrderByDescending(r => r.Percentage);

            // Get the average rating score for each unit 
            var unitRatingQuery = _unitRepository.GetAllUnits()
                .Join(_questionRepository.GetAllQuestions(), u => u.Id, q => q.UnitId, (u, q) => new { UnitName = u.Name, QuestionId = q.Id })
                .Join(_questionRatingRepository.GetAllRatings().Where(qr => qr.UserName == User.Identity.Name), q => q.QuestionId, qr => qr.QuestionId, (q, qr) => new { q.UnitName, q.QuestionId, qr.Rating })
                .GroupBy(t => new { t.UnitName })
                .Select(group => new ReportUnitRating { AverageRating = Math.Round(group.Average(item => item.Rating), 2), UnitName = group.Key.UnitName })
                .ToList().OrderByDescending(r => r.AverageRating);

            var testHistoryQuery = _testSetRepository.GetAllTestSets().Where(t => t.User == User.Identity.Name)
                .Select(r => new ReportTestHistory { DateTaken = r.Date, Score = r.Score })
                .ToList().OrderByDescending(r => r.DateTaken);

            model.QuestionCoverage.AddRange(questionCoverageQuery);
            model.UnitRating.AddRange(unitRatingQuery);
            model.TestHistory.AddRange(testHistoryQuery);

            return View(model);
        }
    }
}