using RevisionApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
