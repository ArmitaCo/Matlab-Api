using System;

namespace Matlab.DataModel
{
    public class ExternalArticle
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public int ArticleId { get; set; }
        public ExternalArticleState State { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public virtual ApplicationUser SuggestedByUser { get; set; }
        public virtual Article Article { get; set; }

    }
}