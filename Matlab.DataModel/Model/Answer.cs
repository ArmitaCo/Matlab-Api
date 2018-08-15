namespace Matlab.DataModel
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ChoiceLable ChoiceLable { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}