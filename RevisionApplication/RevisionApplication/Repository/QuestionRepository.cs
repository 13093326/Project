using RevisionApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Repository
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
            return _appDbContext.Question;
        }

        public Question GetQuestionById(int questionId)
        {

            return _appDbContext.Question.FirstOrDefault(q => q.Id == questionId);
        }

        public bool AddQuestion(Question question)
        {
 
            _appDbContext.Add(question);
            _appDbContext.SaveChanges();

            return true;
        }

        public Question UpdateQuestion(Question question)
        {
            _appDbContext.Question.Update(question);
            _appDbContext.SaveChanges();

            return question;
        }

        public bool DeleteQuestion(int questionId)
        {
            Question question = GetQuestionById(questionId);
            _appDbContext.Question.Remove(question);
            _appDbContext.SaveChanges();

            return true; 
        }
    }
}
