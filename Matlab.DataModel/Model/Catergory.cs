using System.Collections.Generic;

namespace Matlab.DataModel
{
    public class Catergory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}