using System.Collections.Generic;
using Newtonsoft.Json;

namespace Matlab.DataModel
{
    public class UserPackage
    {
        public UserPackage()
        {
            if (UserPackageBoxes==null)
            {
                UserPackageBoxes=new List<UserPackageBox>();
            }
        }
        public int Id { get; set; }
        public int PackageId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        public UserPackageState UserPackageState { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Package Package { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserPackageBox> UserPackageBoxes { get; set; }
    }
}