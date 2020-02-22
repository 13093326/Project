using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class QuestionRepository : IQuestionRepository
    {

        private readonly AppDbContext _appDbContext;

        public QuestionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _appDbContext.Questions;
        }

        public Question GetQuestionById(int questionId)
        {
            return _appDbContext.Questions.FirstOrDefault(q => q.Id == questionId);
        }

        public bool AddQuestion(Question question)
        {
            _appDbContext.Add
                   (
                       new Question { Content = question.Content }
                   );

            _appDbContext.SaveChanges();

            return true;
        }
    }
}
