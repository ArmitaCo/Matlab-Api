using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Catergory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Package> Packages { get; set; }
        [NotMapped]
        public string AbsoluteImageUrl => string.IsNullOrWhiteSpace(ImageUrl) ? null : Defaults.BasePackageImageUrl + ImageUrl;
    }
}