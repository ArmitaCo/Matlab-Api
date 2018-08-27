using System.Collections.Generic;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class Catergory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public virtual ICollection<Package> Packages { get; set; }
    }
}