using System.Collections.Generic;
using System.Linq;

namespace Matlab.DataModel
{
    public class QuestionMinimalViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public ChoiceLable CorrectAnswer { get; set; }
        public string AnswersText { get; set; }
        public ICollection<AnswerMinimalViewModel> Answers { get; set; }
        public int AnswersCount { get; set; }

        public QuestionMinimalViewModel()
        {
            
        }

        public QuestionMinimalViewModel(Question model)
        {
            Id = model.Id;
            QuestionText = model.Title;
            CorrectAnswer = model.CorrectChoiceLable;
            AnswersText = model.AnswersString;
            AnswersCount = model.AnswersCount;
            Answers = model.Answers.Select(x => new AnswerMinimalViewModel(x)).ToList();
        }
    }
}