using System.Collections.Generic;

namespace RevisionApplication.Models
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
        Question UpdateQuestion(Question question);
        bool AddQuestion(Question question, Unit unit);
        bool DeleteQuestion(int questionId);
    }
}
