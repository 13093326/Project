using RevisionApplication.Models;
using System.Collections.Generic;

namespace RevisionApplication.Repository
{
    public interface IQuestionRatingRepository
    {
        IEnumerable<QuestionRating> GetAllRatings();
        QuestionRating GetRatingByQuestionId(int questionId);
        QuestionRating AddRating(QuestionRating questionRating);
        QuestionRating UpdateRating(QuestionRating questionRating);
    }
}
