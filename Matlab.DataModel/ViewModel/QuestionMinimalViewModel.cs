using System.Collections.Generic;
using System.Linq;

namespace Matlab.DataModel
{
    public class QuestionMinimalViewModel
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public ChoiceLabel CorrectAnswer { get; set; }
        public string AnswersText { get; set; }
        public ICollection<AnswerMinimalViewModel> Answers { get; set; }
        public int AnswersCount { get; set; }
        public int? UserAnswerId { get; set; }
        public string Hint { get; set; }

        public QuestionMinimalViewModel()
        {
            
        }

        public QuestionMinimalViewModel(Question model, string userId)
        {
            Id = model.Id;
            QuestionText = model.Title;
            CorrectAnswer = model.CorrectChoiceLabel;
            AnswersText = model.AnswersString;
            AnswersCount = model.AnswersCount;
            Answers = model.Answers.Select(x => new AnswerMinimalViewModel(x)).ToList();
            UserAnswerId = model.UserAnswers.FirstOrDefault(x => x.UserId == userId)?.AnswerId;
            Hint = model.Article.Title;
        }
    }
}