using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
        Question UpdateQuestion(Question question);
        bool AddQuestion(Question question);
        bool DeleteQuestion(int questionId);
    }
}
