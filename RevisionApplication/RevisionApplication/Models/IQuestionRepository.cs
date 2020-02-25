using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface IQuestionRepository
    {
        IEnumerable<Question> GetAllQuestions();
        Question GetQuestionById(int questionId);
        bool AddQuestion(Question question, Unit unit);
    }
}
