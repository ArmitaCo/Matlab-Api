using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Package
    {
        public Package()
        {
            if (Boxes==null)
            {
                Boxes=new HashSet<Box>();
            }
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        [JsonIgnore]
        public string CoverUrl { get; set; }
        public int CategoryId { get; set; }
        public virtual Catergory Catergory { get; set; }
        //[JsonIgnore]
        //public virtual ICollection<Article> Articles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserPackage> UserPackages { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [JsonIgnore] public virtual ICollection<Box> Boxes { get; set; }

        [NotMapped]
        public string AbsoluteImageUrl => string.IsNullOrWhiteSpace(ImageUrl) ? null : Defaults.BasePackageImageUrl + ImageUrl;
        [NotMapped]
        public string AbsoluteCoverUrl => string.IsNullOrWhiteSpace(CoverUrl) ? null : Defaults.BasePackageImageUrl + CoverUrl;
    }
}