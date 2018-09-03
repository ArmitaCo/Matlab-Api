using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Answer
    {
        private static string[] labelStrings = new[] {"الف", "ب", "ج", "د"};
        public int Id { get; set; }
        [JsonIgnore]
        public string Title { get; set; }
        public ChoiceLable ChoiceLable { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        [NotMapped]
        public string Text => labelStrings[(int)ChoiceLable]+". "+Title;
        
    }
}