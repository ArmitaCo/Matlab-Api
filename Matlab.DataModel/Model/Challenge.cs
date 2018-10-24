using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Challenge
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string Image { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        [NotMapped] public string ImageUrl => Defaults.BaseChallengeImageUrl + Image;
    }
}