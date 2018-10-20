using System;
using System.Collections.Generic;
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
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        [NotMapped]
        public string Text => labelStrings[(int)ChoiceLable]+". "+Title;
        
    }

    public class AvatarImage
    {
        private static string _baseUrl = "http://mohsenmeshkini.ir/Images/Avatars/";
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        [NotMapped]
        public string Image => _baseUrl + Name;
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }

    //public class Factor
    //{
    //    public int Id { get; set; }
    //    public int Amount { get; set; }
    //    public string Mobile { get; set; }
    //    public Guid FactorNumber { get; set; }
    //    public int TransId { get; set; }
    //    public FactorStatus Status { get; set; }
    //    public DateTime CreateDateTime { get; set; }
    //    public DateTime SendDateTime { get; set; }
    //    public DateTime RedirectDateTime{ get; set; }
    //    public DateTime { get; set; }
    //    public DateTime CreateDateTime { get; set; }
    //    public DateTime CreateDateTime { get; set; }

    //}

    public enum FactorStatus
    {
        PreSend,
        Sended,
        SendingError,
        Redirected,
        CallbackRecived,
        CallbackRecivedWithError,
        VerifyError,
        Finished
    }
}