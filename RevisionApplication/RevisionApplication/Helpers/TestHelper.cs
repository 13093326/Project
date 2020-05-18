using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class TestHelper : ITestHelper
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ITestSetRepository _testSetRepository;

        public TestHelper(ICommonHelper commonHelper, IQuestionRepository questionRepository, ITestQuestionRepository testQuestionRepository, ITestSetRepository testSetRepository)
        {
            _commonHelper = commonHelper;
            _questionRepository = questionRepository;
            _testQuestionRepository = testQuestionRepository;
            _testSetRepository = testSetRepository;
        }

        // Close an open test set for a user. 
        public int CloseCurrentTestSet(string userName)
        {
            // Update current test set with closed properties. 
            var currentTestSet = GetCurrentTestSet(userName);
            currentTestSet.Complete = true;
            currentTestSet = SetTestScore(currentTestSet);
            currentTestSet.Date = DateTime.Now;
            _testSetRepository.UpdateTestSet(currentTestSet);

            return currentTestSet.Id;
        }

        // Get current test set for a user. 
        public TestSet GetCurrentTestSet(string userName)
        {
            return _testSetRepository.GetAllTestSets().Where(t => t.UserName.Equals(userName)).Where(t => t.Complete == false).OrderBy(t => t.Id).FirstOrDefault();
        }

        // Get the next question in the test for a user. 
        public TestQuestion GetNextTestQuestion(string userName)
        {
            var currentTestSet = GetCurrentTestSet(userName); 

            // If no test set then create new test set. 
            if (currentTestSet is null)
            {
                currentTestSet = CreateTestSet(userName);
            }

            return _testQuestionRepository.GetAllTestQuestions().Where(t => t.Result.Equals("None") && t.TestSetId == currentTestSet.Id).OrderBy(t => t.Id).FirstOrDefault();
        }

        // Get a question by question id. 
        public Question GetQuestionById(int Id)
        {
            return _questionRepository.GetQuestionById(Id);
        }

        // Get test question by test question id. 
        public TestQuestion GetTestQuestionById(int Id)
        {
            return _testQuestionRepository.GetTestQuestionById(Id);
        }

        // Get test set by test set id. 
        public TestSet GetTestSetById(int Id)
        {
            return _testSetRepository.GetTestSetById(Id);
        }

        // Update test question. 
        public void UpdateTestQuestion(TestQuestion testQuestion)
        {
            _testQuestionRepository.UpdateTestQuestion(testQuestion);
        }

        // Create new test set. 
        private TestSet CreateTestSet(string userName)
        {
            // Add new test set. 
            var testSet = _testSetRepository.AddTestSet(new TestSet { UserName = userName, Complete = false });

            // Get questions. 
            IEnumerable<Question> testQuestions = GetQuestions(userName);

            List<TestQuestion> testQuestionSet = new List<TestQuestion>();

            // Add questions to test set. 
            foreach (var question in testQuestions)
            {
                testQuestionSet.Add(new TestQuestion { TestSet = testSet, QuestionId = question.Id, Result = "None" });
            }

            _testQuestionRepository.AddTestQuestions(testQuestionSet);

            return testSet;
        }

        // Get all question id for the selected units for a user. 
        private IEnumerable<int> GetAllValidQuestionId(string userName)
        {
            var units = _commonHelper.GetUserSelectedUnits(userName);
            return _questionRepository.GetAllQuestions().Where(q => units.Contains(q.Unit)).Select(q => q.Id);
        }

        // Get questions for the selected units for a user. 
        private IEnumerable<Question> GetQuestions(string userName)
        {
            // Get question ids for the selected units. 
            var allValidQuestionIds = GetAllValidQuestionId(userName);

            // Default to using all the questions in the test. 
            var testQuestionIds = allValidQuestionIds;

            // Check there is enough questions to make a random selection. 
            if (allValidQuestionIds.Count() > 50)
            {
                // Set up for question selection. 
                Random random = new Random();
                HashSet<int> numbersHash = new HashSet<int>(allValidQuestionIds.OrderBy(x => random.Next()));
                HashSet<int> selectedHash = new HashSet<int>();

                // Select 50 questions. 
                for (int i = 0; i < 50; i++)
                {
                    // Get next question. 
                    var index = random.Next(0, numbersHash.Count() - 1);
                    var questionId = numbersHash.ElementAt(index);

                    // Store question and ensure it is not picked again. 
                    selectedHash.Add(questionId);
                    numbersHash.Remove(questionId);
                }

                // Update the list of questions with those selected. 
                testQuestionIds = selectedHash.ToList();
            }

            // Get the selected questions. 
            return _questionRepository.GetAllQuestions().Where(q => testQuestionIds.Contains(q.Id));
        }

        // Set a test score for a set. 
        private TestSet SetTestScore(TestSet testSet)
        {
            var results = _testQuestionRepository.GetAllTestQuestions().Where(t => t.TestSetId == testSet.Id);

            int totalCount = 0;
            int correctCount = 0;

            // Get total counts. 
            foreach (var result in results)
            {
                totalCount++;
                if (result.Result.Equals("True"))
                {
                    correctCount++;
                }
            }

            // Set total counts. 
            testSet.TotalCount = totalCount;
            testSet.CorrectCount = correctCount;

            Decimal percentage = 0;

            // Calculate percentage. 
            if (totalCount == 0)
            {
                percentage = 0;
            }
            else
            { 
                percentage = Math.Round(((decimal)correctCount / (decimal)totalCount) * 100, 2);
            }

            // Set percentage. 
            testSet.Score = percentage;

            return testSet;
        }
    }
}
