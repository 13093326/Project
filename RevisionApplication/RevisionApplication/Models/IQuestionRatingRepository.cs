using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public interface IQuestionRatingRepository
    {
        IEnumerable<QuestionRating> GetAllRatings();
        QuestionRating GetRatingByQuestionId(int questionId);
        QuestionRating AddRating(QuestionRating questionRating);
        QuestionRating UpdateRating(QuestionRating questionRating);
    }
}
