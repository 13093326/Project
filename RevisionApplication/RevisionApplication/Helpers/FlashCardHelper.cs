using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class FlashCardHelper : IFlashCardHelper
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IQuestionRepository _questionRepository;

        public FlashCardHelper(ICommonHelper commonHelper, IQuestionRepository questionRepository)
        {
            _commonHelper = commonHelper;
            _questionRepository = questionRepository;
        }

        public Question GetRandomQuestionFromUnits(string userName, int record)
        {
            // Get user selected units 
            var units = _commonHelper.GetUserSelectedUnits(userName);

            // Get questions in random order for units that is not the record provided if it should not match that last retrieved question 
            Random random = new Random();
            var allValidQuestionIds = _questionRepository.GetAllQuestions().Where(p => units.Contains(p.Unit) && p.Id != record).OrderBy(x => random.Next()).Select(p => p.Id);

            // Make a random sleection to prevent bias towards low or high numbers 
            if (allValidQuestionIds.Count() > 0)
            {
                var index = allValidQuestionIds.ElementAt(random.Next(0, allValidQuestionIds.Count() - 1));

                return _questionRepository.GetQuestionById(index);
            }

            return null;
        }
    }
}
