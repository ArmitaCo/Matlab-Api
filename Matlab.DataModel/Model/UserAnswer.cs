using System;

namespace Matlab.DataModel
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
    }
}