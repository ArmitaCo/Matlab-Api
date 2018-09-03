using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class ImageSuggest
    {
        private static string _baseUrl = "http://31.25.130.239/Images/Articles/";
        public int Id { get; set; }
        public int ArticleId { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public virtual Article Article { get; set; }

        [NotMapped]
        public string AbsoluteImageUrl => _baseUrl + ImageUrl;
    }
}