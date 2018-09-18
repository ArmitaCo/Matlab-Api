using System;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class ExternalArticle
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public int ArticleId { get; set; }
        [JsonIgnore]
        public ExternalArticleState State { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public DateTime CreateDateTime { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser SuggestedByUser { get; set; }
        [JsonIgnore]
        public virtual Article Article { get; set; }

    }
}