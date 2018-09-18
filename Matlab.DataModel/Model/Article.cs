using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Article
    {
        public Article()
        {
            if (ImageSuggests == null) ImageSuggests = new HashSet<ImageSuggest>();
            if (Favorites == null) Favorites = new HashSet<Favorite>();
        }
        
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        //public int PackageId { get; set; }
        public int BoxId { get; set; }
        //public virtual Package Package { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Box Box { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ImageSuggest> ImageSuggests { get; set; }
        public virtual ICollection<ExternalArticle> ExternalArticles { get; set; }
        
        [NotMapped]
        public string AbsoluteImageUrl => string.IsNullOrWhiteSpace(ImageUrl) ? null : Defaults.BaseImageUrl + ImageUrl;

    }
}
