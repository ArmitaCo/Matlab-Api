namespace Matlab.DataModel
{
    public class AnswerMinimalViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public ChoiceLable ChoiceLable { get; set; }

        public AnswerMinimalViewModel()
        {

        }

        public AnswerMinimalViewModel(Answer model)
        {
            Id = model.Id;
            Text = model.Title;
            IsCorrect = model.IsCorrect;
            ChoiceLable = model.ChoiceLable;
        }
    }
}