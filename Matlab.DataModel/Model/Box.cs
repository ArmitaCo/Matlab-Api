using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Box
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int Code { get; set; }
        [JsonIgnore]
        public virtual Package Package { get; set; }
        [JsonIgnore]
        public virtual ICollection<Article> Articles { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserPackageBox> UserPackageBoxes { get; set; }

        [NotMapped] public string Title => $"از {Code * 5 + 1} تا {(Code + 1) * 5}";
    }
}