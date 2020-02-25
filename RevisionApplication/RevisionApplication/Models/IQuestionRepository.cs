using System.Collections.Generic;

namespace RevisionApplication.Models
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
        bool AddQuestion(Question question, Unit unit);
    }
}
