using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class Box
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int Code { get; set; }
        public virtual Package Package { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<UserPackageBox> UserPackageBoxes { get; set; }
    }
}