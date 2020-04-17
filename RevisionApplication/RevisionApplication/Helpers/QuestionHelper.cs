using RevisionApplication.Models;
using RevisionApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RevisionApplication.Helpers
{
    public class QuestionHelper : IQuestionHelper
    {
        private readonly ICommonHelper _commonHelper;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitRepository _unitRepository;

        public QuestionHelper(ICommonHelper commonHelper, IQuestionRepository questionRepository, IUnitRepository unitRepository)
        {
            _commonHelper = commonHelper;
            _questionRepository = questionRepository;
            _unitRepository = unitRepository;
        }

        // Add new question. 
        public void AddQuestion(Question question)
        {
            _questionRepository.AddQuestion(question);
        }

        // Delete selected quesiton. 
        public void DeleteQuestion(int Id)
        {
            _questionRepository.DeleteQuestion(Id);
        }

        // Get list of all questions for the selected units. 
        public List<Question> GetAllQuestions(string userName)
        {
            // Get user selected units. 
            var units = _commonHelper.GetSelectedUnitsIdList(userName);

            // Get all questions where the unit has been selected. 
            var questions = _questionRepository.GetAllQuestions()
                .Join(_unitRepository.GetAllUnits().Where(u => units.Contains(u.Id)), q => q.UnitId, u => u.Id,
                (q, u) => new Question
                {
                    Answer1 = q.Answer1,
                    Answer2 = q.Answer2,
                    Answer3 = q.Answer3,
                    Answer4 = q.Answer4,
                    Content = q.Content,
                    CorrectAnswer = q.CorrectAnswer,
                    Id = q.Id,
                    Reference = q.Reference,
                    UnitId = q.UnitId,
                    Unit = u
                }).OrderBy(q => q.Id).ToList();

            return questions;
        }

        // Get question by question id. 
        public Question GetQuestionById(int Id)
        {
            return _questionRepository.GetQuestionById(Id);
        }

        // Get unit by unit id. 
        public Unit GetUnitById(int unitId)
        {
            return _unitRepository.GetUnitById(unitId);
        }

        // Get unit by unit name. 
        public Unit GetUnitByName(string selectedUnit)
        {
            return _unitRepository.GetUnitByName(selectedUnit);
        }

        // Update question. 
        public void UpdateQuestion(Question question)
        {
            _questionRepository.UpdateQuestion(question);
        }
    }
}
