using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class UserPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public UserPackageState UserPackageState { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Package Package { get; set; }
    }
}