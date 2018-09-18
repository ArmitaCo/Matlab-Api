using System.Collections.Generic;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class UserPackageBox
    {
        public int Id { get; set; }
        public int UserPackageId { get; set; }
        public int BoxId { get; set; }
        public BoxState State { get; set; }
        public int StateValue { get; set; }
        [JsonIgnore]
        public virtual UserPackage UserPackage { get; set; }
        [JsonIgnore]
        public virtual Box Box { get; set; }

    }
}