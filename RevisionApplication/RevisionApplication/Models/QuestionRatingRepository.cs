using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevisionApplication.Models
{
    public class QuestionRatingRepository : IQuestionRatingRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuestionRatingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public QuestionRating AddQuestionRating(QuestionRating questionRating)
        {
            _appDbContext.Add(questionRating);
            _appDbContext.SaveChanges();

            return questionRating;
        }

        public IEnumerable<QuestionRating> GetAllQuestions()
        {
            return _appDbContext.QuestionRatings;
        }

        public QuestionRating GetQuestionByQuestionId(int questionId)
        {
            return _appDbContext.QuestionRatings.FirstOrDefault(q => q.QuestionId == questionId);
        }
    }
}
