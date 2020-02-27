﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevisionApplication.Models;
using RevisionApplication.ViewModels;

namespace RevisionApplication.Contollers
{
    public class TestController : Controller
    {
        private readonly ITestSetRepository _testSetRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        public const string SessionKeyName = "_Unit";

        public TestController(ITestSetRepository testSetRepository, IQuestionRepository questionRepository, IUnitRepository unitRepository, ITestQuestionRepository testQuestionRepository)
        {
            _testSetRepository = testSetRepository;
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
            _testQuestionRepository = testQuestionRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var currentUser = User.Identity.Name;

            // Find if there is a current test set 
            var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.User.Equals(currentUser)).Where(p => p.Complete == false).OrderBy(p => p.Id).FirstOrDefault();

            if (currentTestSet is null)
            {
                // No test set then create one 
                var testSet = _testSetRepository.AddTestSet( new TestSet { User = currentUser, Complete = false });

                // Get questions for the selected unit 
                var selectedUnits = HttpContext.Session.GetString(SessionKeyName).Split(',').Select(int.Parse).ToList();
                var units = _unitRepository.GetAllUnits().Where(p => selectedUnits.Contains(p.Id));
                var questions = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit)).OrderBy(p => p.Id).Take(50);

                // Insert questions in to test questions 
                foreach (var question in questions)
                {
                    _testQuestionRepository.AddTestQuestion(new TestQuestion { TestSet = testSet, QuestionId = question.Id, Result = "None" });
                }

                currentTestSet = testSet;
            }

            // Display next question for test set 
            var nextTestQuestion = _testQuestionRepository.GetAllTestQuestions().Where(p => p.Result.Equals("None")).OrderBy(p => p.Id).FirstOrDefault();

            if (nextTestQuestion != null)
            {
                var nextQuestion = _questionRepository.GetQuestionById(nextTestQuestion.QuestionId);

                var testViewModel = new TestViewModel()
                {
                    Title = "Test",
                    Question = nextQuestion,
                    currentRecord = nextTestQuestion.Id
                };

                return View(testViewModel);
            }
            else
            {
                // Close test set 
                currentTestSet.Complete = true;
                _testSetRepository.UpdateTestSet(currentTestSet);

                return RedirectToAction("Result", "Test", new { currentTestSet.Id } );
            }
        }

        [HttpPost]
        public IActionResult Index(TestViewModel testViewModel)
        {

            // Record results 
            var result = (testViewModel.ChosenAnswer.Equals(testViewModel.Question.CorrectAnswer))? "True" : "False";
            var testQuestion = _testQuestionRepository.GetTestQuestionById(testViewModel.currentRecord);
            testQuestion.Result = result;
            _testQuestionRepository.UpdateTestQuestion(testQuestion);


            // Check for next question 
            var currentUser = User.Identity.Name;
            var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.User.Equals(currentUser)).OrderBy(p => p.Id).FirstOrDefault();
            var nextTestQuestion = _testQuestionRepository.GetAllTestQuestions().Where(p => p.Result.Equals("None")).OrderBy(p => p.Id).FirstOrDefault();

            // Display next question 
            return RedirectToAction("Index", "Test" );
        }

        [HttpGet]
        public IActionResult Result(int Id)
        {
            // Get test results 
            var results = _testQuestionRepository.GetAllTestQuestions().Where(p => p.TestSetId == Id);

            int totalCount = 0;
            int correctCount = 0;

            foreach (var result in results)
            {
                totalCount++; 
                if (result.Result.Equals("True"))
                {
                    correctCount++;
                }
            }

            var resultViewModel = new ResultViewModel
            {
                TotalCount = totalCount, 
                CorrectCount = correctCount
            };

            // Record score 
            var percentage = Math.Round((decimal)correctCount / (decimal)totalCount, 2);
            var currentTestSet = _testSetRepository.GetAllTestSets().Where(p => p.Id == Id).OrderBy(p => p.Id).FirstOrDefault();
            currentTestSet.Score = percentage;
            _testSetRepository.UpdateTestSet(currentTestSet);

            return View(resultViewModel);
        }
    }
}