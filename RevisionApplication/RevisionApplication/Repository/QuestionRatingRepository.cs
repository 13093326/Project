﻿using RevisionApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Repository
{
    public class QuestionRatingRepository : IQuestionRatingRepository
    {
        private readonly AppDbContext _appDbContext;

        public QuestionRatingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public QuestionRating AddRating(QuestionRating questionRating)
        {
            _appDbContext.Add(questionRating);
            _appDbContext.SaveChanges();

            return questionRating;
        }

        public IEnumerable<QuestionRating> GetAllRatings()
        {
            return _appDbContext.QuestionRating;
        }

        public QuestionRating GetRatingByQuestionId(int questionId)
        {
            return _appDbContext.QuestionRating.FirstOrDefault(q => q.QuestionId == questionId);
        }

        public QuestionRating UpdateRating(QuestionRating questionRating)
        {
            _appDbContext.QuestionRating.Update(questionRating);
            _appDbContext.SaveChanges();

            return questionRating;
        }
    }
}
