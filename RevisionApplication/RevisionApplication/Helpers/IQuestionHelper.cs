using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Helpers
{
    public interface IQuestionHelper
    {
        void AddQuestion(Question question);
        void DeleteQuestion(int Id);
        List<Question> GetAllQuestions(string userName);
        Question GetQuestionById(int Id);
        Unit GetUnitByName(string selectedUnit);
        void UpdateQuestion(Question question);
    }
}
