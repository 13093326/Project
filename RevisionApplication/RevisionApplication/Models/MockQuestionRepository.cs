using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class MockQuestionRepository : IQuestionRepository
    {
        private List<Question> _questions;

        public MockQuestionRepository()
        {
            if (_questions == null)
            {
                InitialiseQuestions();
            }
        }

        private void InitialiseQuestions()
        {
            _questions = new List<Question>
            {
                new Question { Id = 1, Content = "Question 1" },
                new Question { Id = 2, Content = "Question 2" }
            };
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _questions;
        }

        public Question GetQuestionById(int questionId)
        {
            return _questions.FirstOrDefault(p => p.Id == questionId);
        }
    }
}
