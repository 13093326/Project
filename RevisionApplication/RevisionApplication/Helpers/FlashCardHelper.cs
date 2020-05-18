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

        // Get random question for the currently selected units. 
        public Question GetRandomQuestionFromUnits(string userName, int record)
        {
            // Get user selected units. 
            var units = _commonHelper.GetUserSelectedUnits(userName);

            // Get questions in random order for the currently selected units and does not match the record. 
            Random random = new Random();
            var allValidQuestionIds = _questionRepository.GetAllQuestions().Where(q => units.Contains(q.Unit) && q.Id != record).OrderBy(x => random.Next()).Select(q => q.Id);

            // Make a random selection from the randomly ordered list. 
            if (allValidQuestionIds.Count() > 0)
            {
                var index = allValidQuestionIds.ElementAt(random.Next(0, allValidQuestionIds.Count() - 1));

                return _questionRepository.GetQuestionById(index);
            }

            return null;
        }
    }
}
