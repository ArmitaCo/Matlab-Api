using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AnswersString { get; set; }
        public int ArticleId { get; set; }
        public int AnswersCount { get; set; }
        public virtual Article Article { get; set; }
        //public int CorrectAnswerId { get; set; }
        public ChoiceLable CorrectChoiceLable { get; set; }
        //public virtual Answer CorrectAnswer { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }

    }
}