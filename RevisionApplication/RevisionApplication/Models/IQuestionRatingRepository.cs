using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface IQuestionRatingRepository
    {
        IEnumerable<QuestionRating> GetAllQuestions();
        QuestionRating GetQuestionByQuestionId(int questionId);
        QuestionRating AddQuestionRating(QuestionRating questionRating);
    }
}
