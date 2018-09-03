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
        private static string _baseUrl = "http://31.25.130.239/Images/Articles/";
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public int PackageId { get; set; }
        public virtual Package Package { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<ImageSuggest> ImageSuggests { get; set; }


        [NotMapped]
        public string AbsoluteImageUrl => _baseUrl + ImageUrl;

    }

}
