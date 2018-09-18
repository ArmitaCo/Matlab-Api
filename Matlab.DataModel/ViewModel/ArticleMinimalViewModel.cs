using System.Collections.Generic;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class ArticleMinimalViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string CoverUrl { get; set; }
        public int Order { get; set; }
        public ICollection<ExternalArticle> ExternalArticles { get; set; }

        public ArticleMinimalViewModel()
        {
            
        }

        public ArticleMinimalViewModel(Article article)
        {
            Id = article.Id;
            ImageUrl = article.AbsoluteImageUrl;
            Order = article.Order;
            Title = article.Title;
            ExternalArticles = article.ExternalArticles;
        }
    }
}